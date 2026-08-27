using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUserAsync () ; 
        Task<User> GetByEmailAsync(string email); 
        Task<User> GetByIDAsync(int id);
        Task AddAsync (User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Task UpdateEmailDigestAsync(int userId, bool emailDigest);
        Task SaveChangesAsync();
         Task<User> GetByRefreshTokenAsync(string token);
        Task UpdatAaccountActivity(int  userId ,  bool accountActivity);  
    
    }
}
