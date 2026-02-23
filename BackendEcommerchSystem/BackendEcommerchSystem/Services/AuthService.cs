using BackendEcommerchSystem.DTOs.AcountDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendEcommerchSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository; 
        private readonly IConfiguration _configuration; 
        public AuthService(IUserRepository userRepository , IConfiguration configuration)
        {
            _userRepository = userRepository;     
            _configuration = configuration;  
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()) , 
                new Claim(ClaimTypes.Name , user.FullName)  , 
                new Claim(ClaimTypes.Email, user.Email)    ,
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"] , 
                audience: _configuration["JWT:Audience"] , 
                claims:claims ,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"])),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor); 
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if(user == null  || !BCrypt.Net.BCrypt.Verify(model.Password , user.PasswordHash) )
            {
                return new AuthResponseDTO { Mesage = "Inveld Email or Password!" }; 
            }
             var token =  CreateToken(user); 
            return new AuthResponseDTO
            {
                IsAuthentication = true,
                Mesage = "Login successful!",
                Username = user.FullName,
                Email = user.Email,
                Token = token , 
                ExpiresOn = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"]))
            }; 
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO model)
        {
           if( await _userRepository.GetByEmailAsync(model.Email) != null)
           {
                return new AuthResponseDTO { Mesage = "Email is already registered!" }; 
           }

            if (model.Name.Length <2 )
            {
                return new AuthResponseDTO
                {
                    Mesage = "Password must be at least 2 character long."
                };
               }  
            if(model.Password.Length < 8 ) {
                return new AuthResponseDTO { Mesage = "Password must be at least 8 characters long."
                };
            }
            if(!System.Text.RegularExpressions.Regex.IsMatch(model.Password , @"[!@#$%^&*()]") )
            {
                return new AuthResponseDTO { Mesage = "Password must be at least 8 characters long." };
            }
            if(!model.Password.Any(char.IsUpper))
            {
                return new AuthResponseDTO { Mesage = "Password must contain at least one uppercase letter." };
            }
            if(!model.Password.Any(char.IsDigit))
            {
                return new AuthResponseDTO { Mesage = "Password must contain at least one Number letter." };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            
            var user = new User
            {
                FullName = model.Name,
                Email = model.Email,
                PasswordHash = hashedPassword,
                Role = Enums.UserRole.Customer,
                 CreatedAt = DateTime.UtcNow,       
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = CreateToken(user); 
            return new AuthResponseDTO
            {
                IsAuthentication = true,    
                Mesage = "User registered successfully!" , 
                Username = model.Name,   
                Email = model.Email,   
               Token = token,
               ExpiresOn = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"]))
            }; 
           
        }

    } 
 
}
