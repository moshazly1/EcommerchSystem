
using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Repositorie;
using BackendEcommerchSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
          
            builder.Services.AddScoped<IUserRepository, UserReposutory>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryServise>();
            builder.Services.AddScoped<IProductRepository,ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductServices>();
            builder.Services.AddScoped<IProductImageServices, ProductImageService>(); 
            builder.Services.AddScoped<IProductImageRepository, ProductImageReposatory>();
            builder.Services.AddScoped<IUserServises, UserService>();
            builder.Services.AddControllers();
            builder.Services.AddAuthentication(opthion =>
            {
                opthion.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opthion.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,        
                    ValidateIssuer = true , 
                    ValidateAudience = true ,
                    ValidateLifetime = true ,       
                    ValidIssuer = builder.Configuration["JWT:Issuer"] ,
                    ValidAudience = builder.Configuration["JWT:Audience"] , 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                }; 

            }
         

            );
            
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
