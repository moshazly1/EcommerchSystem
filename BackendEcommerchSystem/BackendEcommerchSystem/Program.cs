
using BackendEcommerchSystem.Data;
using BackendEcommerchSystem.Interfaces.Repositories;
using BackendEcommerchSystem.Interfaces.Services;
using BackendEcommerchSystem.Repositorie;
using BackendEcommerchSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.Text;
using Hangfire;
using Hangfire.SqlServer; 
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
            builder.Services.AddScoped<IEmailService , EmailService>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IWhiteListReposatory, whiteListReposatory>();
            builder.Services.AddScoped<IWhiteListServices, WhiteListService>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderServices, OrderServices>();
            builder.Services.AddScoped<IProductFilterReposatory, ProductFilterReposatory>();
            builder.Services.AddScoped<INotificationService , NotificationService>();
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
            builder.Services.AddHangfire( config =>  {
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("Connection")); 
            }) ;
            builder.Services.AddHangfireServer(); 

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy => policy
                            .WithOrigins("http://localhost:3000", "http://localhost:5173")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                              .AllowCredentials());
            });

            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
             
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            app.UseHangfireDashboard(); 
            //}
            app.UseCors("AllowFrontend");
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            RecurringJob.AddOrUpdate<INotificationService>(
                "WeeklyProducts",
                x => x.SendEmailDigestAsync(),
            
                Cron.Weekly(DayOfWeek.Friday , 9)
                ); 
            app.MapControllers();

            app.Run();
        }
    }
}
