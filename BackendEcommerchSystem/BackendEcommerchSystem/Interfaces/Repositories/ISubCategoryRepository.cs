using BackendEcommerchSystem.Entities;

namespace BackendEcommerchSystem.Interfaces.Repositories
{
    public interface ISubCategoryRepository
    {
          Task<IEnumerable<SubCategory>> GetAllAsync();
         Task<SubCategory> GetSubCategoryById(int id);

        Task AddAsync (SubCategory subCategory);    
         Task UpdateAsync (SubCategory subCategory);  


         Task DeleteAsync(int id );
        Task SaveChangeAsync(); 
  
        

    }
}
