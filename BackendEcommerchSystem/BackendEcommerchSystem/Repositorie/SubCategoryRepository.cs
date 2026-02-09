using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AppDbContext _appContext; 
        public SubCategoryRepository(AppDbContext appContext)
        {
            _appContext = appContext;   
        }

        public async Task AddAsync(SubCategory subCategory)
        {
        await _appContext.subcategories
                .AddAsync(subCategory);
           
        }
        public async Task<IEnumerable<SubCategory>> GetAllAsync()
        {
            var SC = await _appContext.subcategories
                .Include(c => c.Category)
                .ToListAsync();
                  return SC;
        }
        public async Task<SubCategory> GetSubCategoryById(int id)
        {
            var SC = await _appContext
                .subcategories
                .Include(c => c.Category)
                .FirstOrDefaultAsync(x => x.SubCategoryId == id);
                return SC;
        }


        public Task UpdateAsync(SubCategory subCategory)
        {
            _appContext.subcategories.Update(subCategory);
            return Task.CompletedTask;
        }
        public async Task DeleteAsync(int id)
        {
            var SC = await GetSubCategoryById(id);
            if (SC != null)
            {
                _appContext.subcategories.Remove(SC);
            }

        }
        public async Task SaveChangeAsync()
        {
           await _appContext.SaveChangesAsync();
        }

      
    }
}
