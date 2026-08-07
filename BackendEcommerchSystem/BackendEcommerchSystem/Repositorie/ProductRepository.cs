using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Entities;
using BackendEcommerchSystem.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BackendEcommerchSystem.Repositorie
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appContext;    
        public ProductRepository(AppDbContext appContext) {
             _appContext = appContext;       
        }


        public  async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _appContext.products
                .Include(p => p.Brand)
                .Include(p => p.SubCategory).Include(p=>p.ProductImages)
                .Where(p => p.IsActive).ToListAsync();
        }

        public async Task<Product> GetByIdProductAsync(int id)
        {
            return await _appContext.products.Include(p => p.Brand)
            .Include(p => p.SubCategory).Include(p => p.ProductImages)
           .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }
        public async Task AddProductAsync(Product product)
        {
            await _appContext.products.AddAsync(product);
            await _appContext.SaveChangesAsync();   
        }

        public async Task UpdateProduct(int id, Product product)
        {
            _appContext.products.Update(product);
            await _appContext.SaveChangesAsync();


        }

        public async Task DeleteProduct(int id)
        {
            var p = await  _appContext.products.FirstOrDefaultAsync(p => p.Id == id);
            if (p == null)
            {
                throw new Exception("Product not found");
            }
           _appContext.products.Remove(p); 
            await _appContext.SaveChangesAsync();   

        }

        public async Task SaveChangesAsync()
        {
            await _appContext.SaveChangesAsync(); 
        }
        public async Task AddPrductAsync(Product product)
        {
            await _appContext.products.AddAsync(product);
            await _appContext.SaveChangesAsync();

        }
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllLaptopAsync(int page, int pageSize)
        {
            var query = _appContext.products
                .Include(p => p.Brand)
                .Include(p => p.SubCategory)
                .Include(p => p.ProductImages)
                .Where(p => p.CategoryId == 1);

            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllPCsAsync(int page  ,  int pageSize)
        {
                var query = _appContext.products.Include(p => p.Brand)
               .Include(p => p.SubCategory)
               .Include(p => p.ProductImages)
               .Where(p => p.CategoryId == 2);
               var totalCount = await query.CountAsync();
               var Products = await query.Skip((page - 1) * pageSize)
               .Take(pageSize).ToListAsync();
                return (Products , totalCount);
        }


        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllMiceAsync(int page, int pageSize)
        {
            var query = _appContext.products.Include(p => p.Brand)
              .Include(p => p.SubCategory)
              .Include(p => p.ProductImages)
              .Where(p => p.CategoryId == 3);
            var totalCount = await query.CountAsync();
            var Products = await query.Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            return (Products, totalCount);
        }

      

        async Task<(IEnumerable<Product> Products, int TotalCount)> IProductRepository.GetAllAccessoriesAsync(int page, int pageSize)
        {
            var query = _appContext.products.Include(p => p.Brand)
              .Include(p => p.SubCategory)
              .Include(p => p.ProductImages)
              .Where(p => p.CategoryId == 2);
            var totalCount = await query.CountAsync();
            var Products = await query.Skip((page - 1) * pageSize)
            .Take(pageSize).ToListAsync();
            return (Products, totalCount);
        }

        public async Task<IEnumerable<Product>> GetProductsAddedLastWeekAsync()
        {
            var lastWeek = DateTime.Now.AddDays(-7);
            var lastProduct = await _appContext.products.Where(p => p.CreatedAt > lastWeek).ToListAsync();
            return lastProduct;     
        }
    }
}
