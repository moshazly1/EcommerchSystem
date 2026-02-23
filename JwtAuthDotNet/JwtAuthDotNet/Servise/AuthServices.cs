using JwtAuthDotNet.Data;
using JwtAuthDotNet.DTO;
using JwtAuthDotNet.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthDotNet.Servise
{
    public class AuthServices : IAuthService
    {
        private readonly AppDbcontext _appDbcontext;
        private readonly IConfiguration _configuration;

        public AuthServices(AppDbcontext appDbcontext, IConfiguration configuration)
        {
            _appDbcontext = appDbcontext;
            _configuration = configuration;
        }

        // ===================== LOGIN =====================
        public async Task<string?> LoginAysnc(UserDTO request)
        {
            var user = await _appDbcontext.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
                return null;

            var passwordResult = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
                return null;

            return CreateToken(user);
        }

        // ===================== REGISTER =====================
        public async Task<User?> RegisterAsync(UserDTO request)
        {
            if (await _appDbcontext.Users
                .AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            var user = new User();

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;

            _appDbcontext.Users.Add(user);
            await _appDbcontext.SaveChangesAsync();

            return user;
        }

        // ===================== CREATE JWT TOKEN =====================
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Role , user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"])
            );

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
