using BackendEcommerchSystem.DTOs.AcountDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Repositorie;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public string GenerateRefrshToken()
        {
            var RandomeNumber = new byte[64]; 
            using var rng = RandomNumberGenerator.Create(); 
          rng.GetBytes(RandomeNumber);  
            return Convert.ToBase64String(RandomeNumber);   
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if(user == null  || !BCrypt.Net.BCrypt.Verify(model.Password , user.PasswordHash) )
            {
                return new AuthResponseDTO { Mesage = "Inveld Email or Password!" }; 
            }
             var token =  CreateToken(user); 
             var refreshToken = GenerateRefrshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
             await _userRepository.UpdateAsync(user);   
            await _userRepository.SaveChangesAsync();
            return new AuthResponseDTO
            {
                IsAuthentication = true,
                Mesage = "Login successful!",
                Username = user.FullName,
                Email = user.Email,
                Token = token,
                ExpiresOn = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"])) , 
            AccessToken = token,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime.Value,
            };  
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(string Token)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(Token);

            // 1. لو التوكن مش موجود أو منتهي
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    Mesage = "Invalid or expired refresh token!",
                };
            } 

          
            var newAccessToken = CreateToken(user);
            var newRefreshToken = GenerateRefrshToken();

          
            user.RefreshToken = newRefreshToken; 
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new AuthResponseDTO
            {
                IsAuthentication = true,
                Mesage = "Token refreshed successfully!",
                Token = newAccessToken,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Username = user.FullName,
                Email = user.Email,
                ExpiresOn = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"]))
            };
        }
        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO model)
        {

            var errors = new List<string>();

           
            var email = model.Email?.Trim();
            var name = model.Name?.Trim();
            var password = model.Password;

       
            if (string.IsNullOrEmpty(email))
                errors.Add("Email is required.");
            else if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                errors.Add("Invalid email format.");
            else if (await _userRepository.GetByEmailAsync(email) != null)
                errors.Add("Email is already registered.");

        
            if (string.IsNullOrEmpty(name))
                errors.Add("Name is required.");
            else if (name.Length < 2)
                errors.Add("Name must be at least 2 characters long.");

     
            if (string.IsNullOrEmpty(password))
                errors.Add("Password is required.");
            else
            {
                if (password.Length < 8)
                    errors.Add("Password must be at least 8 characters long.");

                if (!password.Any(char.IsUpper))
                    errors.Add("Password must contain at least one uppercase letter.");

                if (!password.Any(char.IsLower))
                    errors.Add("Password must contain at least one lowercase letter.");

                if (!password.Any(char.IsDigit))
                    errors.Add("Password must contain at least one number.");

                if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*()]"))
                    errors.Add("Password must contain at least one special character (!@#$%^&*).");
            }

            if (errors.Any())
            {
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    Mesage = string.Join(" \n ", errors)
                };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            
            var user = new User
            {
                FullName = name,
                Email = email,
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
                Username = name,   
                Email = email,   
               Token = token,
               ExpiresOn = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"]))
            }; 
           
        }

    } 
 
}
