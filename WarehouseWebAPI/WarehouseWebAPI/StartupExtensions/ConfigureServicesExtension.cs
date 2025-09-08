using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using Services.CategoriesServices;

namespace WarehouseWebAPI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration) 
        {
            //Repositories
            services.AddScoped<ICategoryRepository, CategoriesRepository>();

            //Services
            services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();
            services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
            services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
            services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Entities")));

            return services;
        }
    }
}
