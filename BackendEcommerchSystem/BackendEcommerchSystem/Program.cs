
using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Repositorie;
using BackendEcommerchSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BackendEcommerchSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {

          
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
            );
            // Add services to the container.
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryServise>();
            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductServices>(); 
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
