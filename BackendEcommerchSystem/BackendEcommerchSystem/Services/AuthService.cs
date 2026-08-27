using BackendEcommerchSystem.DTOs.AcountDTO;
using BackendEcommerchSystem.DTOs.UserDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Repositorie;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Tls;
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
        private readonly IEmailService _emailService; 
        
        public AuthService(IUserRepository userRepository , IConfiguration configuration , IEmailService emailService)
        {
            _userRepository = userRepository;     
            _configuration = configuration;
            _emailService = emailService;  
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
            var user = await _userRepository.GetByEmailAsync(emailUser);
          if(user == null)
            {
                return false;
            }
          var resetToken = Guid.NewGuid().ToString();
            user.ResetPasswordToken = resetToken;
            user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddMinutes(15); 
             _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
            var resetLink = $"http://localhost:3000/reset-password?token={resetToken}&email={emailUser}";
            await _emailService.SendEmailAsync(emailUser , "Reset Password", $@"
        <h2>Password Reset Request</h2>
        <p>You requested to reset your password.</p>
        <a href='{resetLink}' 
           style='padding:10px 20px;background:#007bff;color:white;text-decoration:none;border-radius:5px'>
           Reset Password
        </a>
        <p>This link will expire in 15 minutes.</p>
    ");  
            return true;    
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
                return new AuthResponseDTO { Mesage = "Invalid Email or Password!" }; 
            }

            if (user.TwoFactorEnabled)
            {
                var code = Random.Shared.Next(100000 , 1000000).ToString();
                user.TowFactorCode = code;      
                user.TowFactorCodeExpiresAt = DateTime.Now.AddMinutes(10);
                _userRepository.UpdateUser(user);
                await _userRepository.SaveChangesAsync();

                string twoFactorBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Login Verification</title>
</head>

<body style='margin:0; padding:0; background-color:#f3f4f6; font-family:Arial, Helvetica, sans-serif;'>

    <div style='max-width:600px; margin:40px auto; background:#ffffff;
                border-radius:14px; overflow:hidden;
                box-shadow:0 4px 20px rgba(0,0,0,0.08);'>

        <!-- Header -->
        <div style='background:#2563eb; padding:30px; text-align:center;'>
            <h1 style='color:#ffffff; margin:0; font-size:26px;'>
                🔐 Login Verification
            </h1>

            <p style='color:#dbeafe; margin:8px 0 0; font-size:14px;'>
                TekStore Account Security
            </p>
        </div>

        <!-- Content -->
        <div style='padding:35px;'>

            <h2 style='color:#111827; margin-top:0;'>
                Hello {user.FullName},
            </h2>

            <p style='color:#4b5563; font-size:15px; line-height:1.7;'>
                We received a request to sign in to your TekStore account.
                To complete your login, please enter the verification code below.
            </p>

            <!-- Verification Code -->
            <div style='text-align:center; margin:30px 0;'>

                <p style='color:#6b7280; font-size:14px; margin-bottom:10px;'>
                    Your verification code
                </p>

                <div style='display:inline-block;
                            background:#eff6ff;
                            border:2px solid #2563eb;
                            border-radius:12px;
                            padding:18px 35px;'>

                    <span style='font-size:32px;
                                 font-weight:bold;
                                 letter-spacing:8px;
                                 color:#1d4ed8;'>
                        {code}
                    </span>

                </div>

            </div>

            <!-- Expiration -->
            <div style='background:#f8fafc;
                        border:1px solid #e5e7eb;
                        border-radius:10px;
                        padding:18px;
                        margin:25px 0;'>

                <p style='margin:0;
                          color:#374151;
                          font-size:14px;
                          line-height:1.6;'>

                    ⏱️ <strong>This code will expire in 10 minutes.</strong>
                    <br>
                    For your security, please do not share this code with anyone.
                </p>

            </div>

            <!-- Security Warning -->
            <div style='background:#fff7ed;
                        border-left:4px solid #f97316;
                        padding:15px;
                        margin:25px 0;'>

                <p style='margin:0;
                          color:#9a3412;
                          font-size:14px;
                          line-height:1.6;'>

                    <strong>Didn't try to sign in?</strong>
                    <br>

                    If you did not request this verification code,
                    please change your password immediately and secure your account.
                </p>

            </div>

            <p style='color:#6b7280;
                      font-size:14px;
                      line-height:1.6;'>

                Your account security is important to us.
                Never share your verification code with anyone,
                including TekStore support.
            </p>

        </div>

        <!-- Footer -->
        <div style='background:#f9fafb;
                    padding:20px;
                    text-align:center;
                    border-top:1px solid #e5e7eb;'>

            <p style='margin:0;
                      color:#9ca3af;
                      font-size:12px;'>

                This is an automated security notification from TekStore.
            </p>

            <p style='margin:8px 0 0;
                      color:#9ca3af;
                      font-size:12px;'>

                © {DateTime.Now.Year} TekStore. All rights reserved.
            </p>

        </div>

    </div>

</body>
</html>";

                await _emailService.SendEmailAsync(
         user.Email,
         "🔐 Your TekStore Verification Code",
         twoFactorBody
     );
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    RequiresTwoFactor = true,
                    Mesage = "A verification code has been sent to your email.",
                    TwoFactorCodeExpiresAt = user.TowFactorCodeExpiresAt , 
                }; 
            }

         
             var token =  CreateToken(user); 
             var refreshToken = GenerateRefrshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
              _userRepository.UpdateUser(user);   
            await _userRepository.SaveChangesAsync();
            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>

<body style='margin:0; padding:0; background-color:#f3f4f6; font-family:Arial, Helvetica, sans-serif;'>

    <div style='max-width:600px; margin:40px auto; background:#ffffff;
                border-radius:12px; overflow:hidden;
                box-shadow:0 4px 15px rgba(0,0,0,0.08);'>

        <!-- Header -->
        <div style='background:#2563eb; padding:28px; text-align:center;'>
            <h1 style='color:#ffffff; margin:0; font-size:26px;'>
                🔐 New Login Detected
            </h1>

            <p style='color:#dbeafe; margin:8px 0 0; font-size:14px;'>
                TekStore Account Security
            </p>
        </div>

        <!-- Content -->
        <div style='padding:30px;'>

            <h2 style='color:#111827; margin-top:0;'>
                Hello {user.FullName},
            </h2>

            <p style='color:#4b5563; font-size:15px; line-height:1.7;'>
                We detected a successful login to your TekStore account.
                If this was you, you don't need to take any action.
            </p>

            <!-- Login Details -->
            <div style='background:#f8fafc; border:1px solid #e5e7eb;
                        border-radius:8px; padding:20px; margin:25px 0;'>

                <h3 style='color:#2563eb; margin-top:0;'>
                    Login Details
                </h3>

                <p style='margin:10px 0; color:#374151;'>
                    <strong>Date & Time:</strong>
                    {DateTime.Now:dddd, dd MMMM yyyy - hh:mm tt}
                </p>

                <p style='margin:10px 0; color:#374151;'>
                    <strong>Account:</strong>
                    {user.Email}
                </p>

            </div>

            <!-- Security Warning -->
            <div style='background:#fff7ed; border-left:4px solid #f97316;
                        padding:15px; margin:25px 0;'>

                <p style='margin:0; color:#9a3412; font-size:14px; line-height:1.6;'>
                    <strong>Didn't log in?</strong><br>
                    If you don't recognize this activity, please change your
                    password immediately and secure your account.
                </p>

            </div>

            <p style='color:#6b7280; font-size:14px; line-height:1.6;'>
                Your account security is important to us. We recommend using
                a strong, unique password for your TekStore account.
            </p>

        </div>

        <!-- Footer -->
        <div style='background:#f9fafb; padding:20px; text-align:center;
                    border-top:1px solid #e5e7eb;'>

            <p style='margin:0; color:#9ca3af; font-size:12px;'>
                This is an automated security notification from TekStore.
            </p>

            <p style='margin:8px 0 0; color:#9ca3af; font-size:12px;'>
                © {DateTime.Now.Year} TekStore. All rights reserved.
            </p>

        </div>

    </div>

</body>
</html>";
            if (user.AccountActivity) {
                await _emailService.SendEmailAsync(
    user.Email,
    "🔐 New Login Detected - TekStore",
    body
);
            }
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
             _userRepository.UpdateUser(user);
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

             _userRepository.UpdateUser(user);
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

             _userRepository.UpdateUser(user);
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

        public async Task<AuthResponseDTO> ResendTwoFactorCodeAsync(string email)
        {
         
            if (string.IsNullOrEmpty(email))
            {
               
                return  new AuthResponseDTO
                {
                    Mesage = "Email is required "
                };  
            }
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) {
                return new AuthResponseDTO
                {
                    Mesage = "Invalid request." , 
                };  

            }
          
                if (!user.TwoFactorEnabled)
                {
                    return new AuthResponseDTO { Mesage = "Invalid request." };
                }

            var code = Random.Shared.Next(100000, 1000000).ToString();
            user.TowFactorCode = code;
            user.TowFactorCodeExpiresAt = DateTime.Now.AddMinutes(10);

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();

            string twoFactorBody = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Login Verification</title>
</head>
<body style='margin:0; padding:0; background-color:#f3f4f6; font-family:Arial, Helvetica, sans-serif;'>
    <div style='max-width:600px; margin:40px auto; background:#ffffff;
                border-radius:14px; overflow:hidden;
                box-shadow:0 4px 20px rgba(0,0,0,0.08);'>
        <div style='background:#2563eb; padding:30px; text-align:center;'>
            <h1 style='color:#ffffff; margin:0; font-size:26px;'>🔐 Login Verification</h1>
            <p style='color:#dbeafe; margin:8px 0 0; font-size:14px;'>TekStore Account Security</p>
        </div>
        <div style='padding:35px;'>
            <h2 style='color:#111827; margin-top:0;'>Hello {user.FullName},</h2>
            <p style='color:#4b5563; font-size:15px; line-height:1.7;'>
                We received a request to sign in to your TekStore account.
                To complete your login, please enter the verification code below.
            </p>
            <div style='text-align:center; margin:30px 0;'>
                <p style='color:#6b7280; font-size:14px; margin-bottom:10px;'>Your verification code</p>
                <div style='display:inline-block; background:#eff6ff; border:2px solid #2563eb; border-radius:12px; padding:18px 35px;'>
                    <span style='font-size:32px; font-weight:bold; letter-spacing:8px; color:#1d4ed8;'>{code}</span>
                </div>
            </div>
            <div style='background:#f8fafc; border:1px solid #e5e7eb; border-radius:10px; padding:18px; margin:25px 0;'>
                <p style='margin:0; color:#374151; font-size:14px; line-height:1.6;'>
                    ⏱️ <strong>This code will expire in 10 minutes.</strong><br>
                    For your security, please do not share this code with anyone.
                </p>
            </div>
            <div style='background:#fff7ed; border-left:4px solid #f97316; padding:15px; margin:25px 0;'>
                <p style='margin:0; color:#9a3412; font-size:14px; line-height:1.6;'>
                    <strong>Didn't try to sign in?</strong><br>
                    If you did not request this verification code, please change your password immediately and secure your account.
                </p>
            </div>
            <p style='color:#6b7280; font-size:14px; line-height:1.6;'>
                Your account security is important to us.
                Never share your verification code with anyone, including TekStore support.
            </p>
        </div>
        <div style='background:#f9fafb; padding:20px; text-align:center; border-top:1px solid #e5e7eb;'>
            <p style='margin:0; color:#9ca3af; font-size:12px;'>This is an automated security notification from TekStore.</p>
            <p style='margin:8px 0 0; color:#9ca3af; font-size:12px;'>© {DateTime.Now.Year} TekStore. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

            await _emailService.SendEmailAsync(
                user.Email,
                "🔐 Your TekStore Verification Code",
                twoFactorBody
            );

            return new AuthResponseDTO
            {
                RequiresTwoFactor = true,
                Mesage = "A new verification code has been sent to your email.",
                TwoFactorCodeExpiresAt = user.TowFactorCodeExpiresAt,
            };
        }

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null ||
        user.ResetPasswordToken != token ||
        user.ResetPasswordTokenExpiry < DateTime.UtcNow)
            {
                return false;   
            }
            if (newPassword.Length < 8 ||
     !newPassword.Any(char.IsUpper) ||
     !newPassword.Any(char.IsLower) ||
     !newPassword.Any(char.IsDigit))
            {
                return false;
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiry = null;
            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();
            string body = $@"
<div style='font-family:Arial,sans-serif;max-width:600px;margin:auto;border:1px solid #e5e7eb;border-radius:8px;padding:30px'>
    <h2 style='color:#2563eb;'>🔒 Password Changed Successfully</h2>

    <p>Hello <strong>{user.FullName}</strong>,</p>

    <p>This is a confirmation that the password for your <strong>TekStore</strong> account has been changed successfully.</p>

    <div style='background:#f8fafc;padding:15px;border-left:4px solid #2563eb;margin:20px 0'>
        <strong>Time:</strong> {DateTime.Now:dddd, dd MMMM yyyy HH:mm}
    </div>

    <p>If you made this change, no further action is required.</p>

    <p style='color:#dc2626;'>
        <strong>If you did not change your password, please secure your account immediately and contact our support team.</strong>
    </p>

    <hr style='margin:25px 0'>

    <p style='font-size:13px;color:gray'>
        This is an automated security notification from TekStore.
    </p>
</div>";
            if (user.AccountActivity)
            {
                await _emailService.SendEmailAsync(
                    user.Email,
                     "🔒 Password Changed",
                    

     body
                    );
                
                   
            }
            
            return true;

          
        }

        public async Task<AuthResponseDTO> VerifyTwoFactorAsync(VerifyTwoeFactorDTO model)
        {
            var user = await _userRepository.GetByEmailAsync(model.Email);
            if (user == null)
            {
                return new AuthResponseDTO()
                {
                    IsAuthentication = false,
                    Mesage = "Invalid verification request."
                };
            }
            if (string.IsNullOrEmpty(user.TowFactorCode) ||
                user.TowFactorCode != model.Code)
            {
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    Mesage = "Invalid verification code."
                };

            }
            if (user.TowFactorCodeExpiresAt == null || user.TowFactorCodeExpiresAt < DateTime.UtcNow)
            {
                return new AuthResponseDTO
                {
                    IsAuthentication = false,
                    Mesage = "Verification code has expired."
                };
            }
            user.TowFactorCode = null;
            user.TowFactorCodeExpiresAt = null;

            var token = CreateToken(user);
            var refreshToken = GenerateRefrshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            _userRepository.UpdateUser(user);
            await _userRepository.SaveChangesAsync();

            return new AuthResponseDTO
            {
                IsAuthentication = true,
                RequiresTwoFactor = false,
                Mesage = "Login successful!",
                Username = user.FullName,
                Email = user.Email,
                Token = token,
                AccessToken = token,
                ExpiresOn = DateTime.UtcNow.AddDays(
                    Convert.ToDouble(_configuration["JWT:DurationInDays"])
                ),
                RefreshToken = refreshToken,
                RefreshTokenExpiration = user.RefreshTokenExpiryTime.Value,
                Role = user.Role

            };
        }
    } 
 
}
