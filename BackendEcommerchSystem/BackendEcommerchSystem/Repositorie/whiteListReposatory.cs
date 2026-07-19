using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Entities;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class whiteListReposatory  :IWhiteListReposatory 
    {
        private readonly AppDbContext _appDbContext;    
        public whiteListReposatory(AppDbContext appDbContext) { 
         _appDbContext = appDbContext;  
        }

        public async Task AddWhiteListAsync(WhiteList whiteList)
        {
            await _appDbContext.WhiteLists.AddAsync(whiteList);
        }

        public async Task<WhiteList?> GetByUserAndProductAsync(int userId, int productId)
        {
        return await   _appDbContext.WhiteLists.FirstOrDefaultAsync(w => w.UserId == userId && w.ProductId == productId);
        }

        public async Task<IEnumerable<WhiteList>> GetWhiteListsByUserID(int userId)
        {
            return await _appDbContext.WhiteLists.Include(w=>w.Product).ThenInclude(p=>p.ProductImages).Where(w=>w.UserId == userId).ToListAsync();  
        }

        public async Task RemoveAsync(WhiteList whiteList)
        {
            _appDbContext.WhiteLists.Remove(whiteList);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync(); 
        }
    }
}
