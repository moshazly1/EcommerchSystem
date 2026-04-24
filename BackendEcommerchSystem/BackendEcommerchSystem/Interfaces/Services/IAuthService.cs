using BackendEcommerchSystem.DTOs.AcountDTO;

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
    }
}
