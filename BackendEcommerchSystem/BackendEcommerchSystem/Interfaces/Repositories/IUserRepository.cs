using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync () ; 
        Task<User> GetByEmailAsync(string email); 
        Task<User> GetByIDAsync(int id);
        Task UpdateAsync(User user);
        Task AddAsync (User user);
        Task DeleteUserAsync(int id);  
        Task SaveChangesAsync();
         Task<User> GetByRefreshTokenAsync(string token); 
    }
}
