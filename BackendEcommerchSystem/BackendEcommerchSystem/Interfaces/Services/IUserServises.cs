using BackendEcommerchSystem.DTOs.UserDTO;
using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Services
{
    public interface IUserServises
    {
        Task<IEnumerable<UserDTO>> GetAllUser();
        Task<UserDTO> GetByIDAsync(int id);
        Task<UserDTO> GetByEmailAsync(   string email);
        Task AddUserAsync(CreateUserDTO userDTO);
        Task UpdateUser(int id, UpdateUserDTO update);
        Task DeleteUser(int id);

        Task UpdateEmailDigestAsync(int userId , bool emailDigest); 
    }
}
