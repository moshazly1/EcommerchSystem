using BackendEcommerchSystem.DTOs.AcountDTO;
using BackendEcommerchSystem.DTOs.UserDTO;
using BackendEcommerchSystem.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace BackendEcommerchSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthentication)
            {
                return BadRequest(result.Mesage);
            }

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = result.RefreshTokenExpiration
            });

            return Ok(new
            {
                isAuthentication = true,
                mesage = result.Mesage,
                accessToken = result.AccessToken,
                username = result.Username,
                email = result.Email,
                role = result.Role
            });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.LoginAsync(model);

            // 2FA required
            if (result.RequiresTwoFactor)
            {
                return Ok(new
                {
                    isAuthentication = false,
                    requiresTwoFactor = true,
                    mesage = result.Mesage , 
                   email = model.Email,
                    twoFactorCodeExpiresAt = result.TwoFactorCodeExpiresAt
                });
            }

            if (!result.IsAuthentication)
            {
                return Unauthorized(result.Mesage);
            }

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = result.RefreshTokenExpiration
            });

            return Ok(new
            {
                isAuthentication = true,
                requiresTwoFactor = false,
                mesage = result.Mesage,
                accessToken = result.AccessToken,
                username = result.Username,
                email = result.Email,
                role = result.Role
            });
        }
        [HttpPost("refreshToken")]

        public async Task<IActionResult> RefreshToken()
        {
            var token = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return Unauthorized("Refresh token missing");

            var result = await _authService.RefreshTokenAsync(token);
            if (!result.IsAuthentication)
                return Unauthorized(result.Mesage);

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,  
                Expires = result.RefreshTokenExpiration
            });

            return Ok(new
            {
                accessToken = result.AccessToken,
                role = result.Role,
                username = result.Username,
                email = result.Email
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(token))
            {
                await _authService.LogoutAsync(token);
            }
            Response.Cookies.Delete("refreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            });
            return Ok("Logged Out Successfully");
        }


        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid request data."
                });
            }

            var result = await _authService.ForgotPasswordAsync(model.Email);

            if (!result)
            {

                return Ok(new
                {
                    message = "If the email exists, a reset link has been sent."
                });
            }

            return Ok(new
            {
                message = "If the email exists, a reset link has been sent."
            });
        }
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPaswordDTO model)
        {
            var resalt = await _authService.ResetPasswordAsync(model.Email, model.Token, model.NewPassword);
            if (!resalt)
            {
                return BadRequest("Invalid or expired token!");
            }
            return Ok("Password reset successfully!");
        }

        [HttpPost("verfy-2fa")]
        public async Task<IActionResult> VerfiyTwoFactor([FromBody] VerifyTwoeFactorDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var result = await _authService.VerifyTwoFactorAsync(model); 
            if(!result.IsAuthentication)
            {
                return Unauthorized(result.Mesage); 
            }
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = result.RefreshTokenExpiration
            });
            return Ok(new
            {
                isAuthentication = true,
                message = result.Mesage,
                accessToken = result.AccessToken,
                username = result.Username,
                email = result.Email,
                role = result.Role
            });

        }

        [HttpPost("Resend-2fa")]
        public async Task<IActionResult> ResendTwoFactor([FromBody]   ResendTowFactorDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);      
            }
            var resalt = await _authService.ResendTwoFactorCodeAsync(model.Email); 
            if(!resalt.RequiresTwoFactor)
            {
                return BadRequest(resalt.Mesage);
            }
            return Ok(new
            {
                mesage = resalt.Mesage,
                twoFactorCodeExpiresAt = resalt.TwoFactorCodeExpiresAt,
            });
        }
    
    }
}
