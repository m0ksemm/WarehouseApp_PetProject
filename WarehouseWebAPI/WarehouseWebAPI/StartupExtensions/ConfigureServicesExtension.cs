using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.ManufacturersServiceContracts;
using Services.CategoriesServices;
using Services.ManufacturersService;

namespace WarehouseWebAPI.StartupExtensions
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            IConfiguration configuration) 
        {
            //Repositories
            services.AddScoped<ICategoryRepository, CategoriesRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

            //Services
            services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();
            services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
            services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
            services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();

            services.AddScoped<IManufacturersAdderService, ManufacturersAdderService>();
            services.AddScoped<IManufacturersDeleterService, ManufacturersDeleterService>();
            services.AddScoped<IManufacturersGetterService, ManufacturersGetterService>();
            services.AddScoped<IManufacturersUpdaterService, ManufacturersUpdaterService>();
            services.AddScoped<IManufacturersDeliveriesUpdaterService, ManufacturersDeliveriesUpdaterService>();

            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Entities")));

            return services;
        }
    }
}
