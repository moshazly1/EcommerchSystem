using BackendEcommerchSystem.DTOs.AcountDTO;
using BackendEcommerchSystem.DTOs.UserDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO model);
        Task<AuthResponseDTO> LoginAsync(LoginDTO model);
        Task<AuthResponseDTO> RefreshTokenAsync(string token);
        Task LogoutAsync(string refreshToken);
        Task<bool> ForgotPasswordAsync( string email);
        Task<bool> ResetPasswordAsync(string email , string token , string newPassword );
        Task<AuthResponseDTO> VerifyTwoFactorAsync(VerifyTwoeFactorDTO model);
        Task<AuthResponseDTO> ResendTwoFactorCodeAsync(string  email); 

    }
}
