using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appContext; 
        public CategoryRepository(AppDbContext appContext) {
         _appContext = appContext;  
        } 
        public async Task AddAsync(Category category)
        {
            await _appContext.categories.AddAsync(category); 
           
        }

        public async Task DeleteAsync(int id)
        {
           var cat = await _appContext.categories.FindAsync(id);
            if (cat != null) {
             _appContext.categories.Remove(cat);    
            }
        }

        public  async  Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _appContext.categories.ToListAsync();  
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            var cat = await _appContext.categories.FindAsync( id);
         
                return  cat; 
         
        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync();
        }


        public Task UpdateAsync(Category category)
        {
            _appContext.categories.Update(category);
           return Task.CompletedTask;
         }
    }
}
