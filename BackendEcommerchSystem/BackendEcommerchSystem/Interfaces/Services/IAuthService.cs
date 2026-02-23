using BackendEcommerchSystem.DTOs.AcountDTO;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO model);
        Task<AuthResponseDTO> LoginAsync(LoginDTO model);
        
    }
}
