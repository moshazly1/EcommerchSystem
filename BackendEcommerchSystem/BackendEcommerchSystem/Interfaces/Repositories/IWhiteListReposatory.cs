using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface IWhiteListReposatory
    {
        Task<IEnumerable<WhiteList>> GetWhiteListsByUserID(int userId);
        Task<WhiteList?> GetByUserAndProductAsync(int userId, int productId);
        Task AddWhiteListAsync(WhiteList whiteList);
        Task RemoveAsync(WhiteList whiteList);
        Task SaveChangesAsync(); 
    }
}
