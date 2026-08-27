using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.DTOs.FiltrationDTO;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem.Repositorie
{
    public class ProductFilterReposatory : IProductFilterReposatory
    {
        private readonly AppDbContext appContext;
       public   ProductFilterReposatory(AppDbContext _appContext)
        {
            appContext = _appContext;   
        }
        public async Task<List<Product>> GetFilteredProductsAsync(ProductFilterDTO dto)
        {
            IQueryable<Product> query = appContext.products;
            if (dto.MinPrice.HasValue)
            {
                query =  query.Where(p => p.Price >= dto.MinPrice); 
            }
            if (dto.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= dto.MaxPrice);   // ✅ كده
            }

            if (dto.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == dto.CategoryId);
            }
            if(dto.SubCategoryId.HasValue)
            {
                query = query.Where(p => p.SubCategoryId == dto.SubCategoryId); 
            }
            if (dto.BrandID.HasValue)
            {
                query = query.Where(p => p.BrandId == dto.BrandID);
            }
            return await query.ToListAsync();      

        }
    }
}
