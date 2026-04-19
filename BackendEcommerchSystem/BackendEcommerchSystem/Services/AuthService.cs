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

        public async Task<bool> ForgotPasswordAsync(string emailUser)
        {
            //var email = await _userRepository.GetByEmailAsync(emailUser); 
            return true; ;

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
                Role = user.Role
            };  
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);
            if (user == null) {
                return;
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();


        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(string Token)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(Token);


            if (user == null ||
      user.RefreshToken != Token ||
      user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    Mesage = "Invalid or expired refresh token!"
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
                RefreshTokenExpiration = user.RefreshTokenExpiryTime.Value,
                RefreshToken = newRefreshToken,
                Username = user.FullName,
                Email = user.Email,
                ExpiresOn = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:DurationInDays"])) , 
             Role = user.Role
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

            if (string.IsNullOrEmpty(name))
                errors.Add("Name is required.");

            if (string.IsNullOrEmpty(password))
                errors.Add("Password is required.");

            if (errors.Any())
            {
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    Mesage = string.Join("\n", errors)
                };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                FullName = name,
                Email = email,
                PasswordHash = hashedPassword,
                Role = Enums.UserRole.Customer,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var accessToken = CreateToken(user);
            var refreshToken = GenerateRefrshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new AuthResponseDTO
            {
                IsAuthentication = true,
                Mesage = "User registered successfully!",
                Username = user.FullName,
                Email = user.Email,
                AccessToken = accessToken,
                Token = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime.Value,
                ExpiresOn = DateTime.Now.AddDays(
                    Convert.ToDouble(_configuration["JWT:DurationInDays"])
                ),
                Role = user.Role
            };
        }

    } 
 
}
