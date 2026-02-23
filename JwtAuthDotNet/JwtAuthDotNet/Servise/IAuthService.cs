using JwtAuthDotNet.DTO;
using JwtAuthDotNet.Entities;

namespace JwtAuthDotNet.Servise
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDTO request);
        Task<string?> LoginAysnc(UserDTO request); 
        
    }
}
