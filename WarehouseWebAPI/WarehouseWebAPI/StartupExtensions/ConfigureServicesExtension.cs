using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.ManufacturersServiceContracts;
using ServiceContracts.ProductsServiceContracts;
using ServiceContracts.WarehousesServiceContracts;
using Services.CategoriesServices;
using Services.ManufacturersService;
using Services.ProductsServices;
using Services.WarehousesServices;

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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            //Services
            services.AddScoped<ICategoriesAdderService, CategoriesAdderService>();
            services.AddScoped<ICategoriesDeleterService, CategoriesDeleterService>();
            services.AddScoped<ICategoriesGetterService, CategoriesGetterService>();
            services.AddScoped<ICategoriesUpdaterService, CategoriesUpdaterService>();

            services.AddScoped<IManufacturersAdderService, ManufacturersAdderService>();
            services.AddScoped<IManufacturersDeleterService, ManufacturersDeleterService>();
            services.AddScoped<IManufacturersGetterService, ManufacturersGetterService>();
            services.AddScoped<IManufacturersUpdaterService, ManufacturersUpdaterService>();

            services.AddScoped<IProductsAdderService, ProductsAdderService>();
            services.AddScoped<IProductsDeleterService, ProductsDeleterService>();
            services.AddScoped<IProductsGetterService, ProductsGetterService>();
            services.AddScoped<IProductsUpdaterService, ProductsUpdaterService>();

            services.AddScoped<IWarehousesAdderService, WarehousesAdderService>();
            services.AddScoped<IWarehousesDeleterService, WarehousesDeleterService>();
            services.AddScoped<IWarehousesGetterService, WarehousesGetterService>();
            services.AddScoped<IWarehousesUpdaterService, WarehousesUpdaterService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Entities")));

            return services;
        }
    }
}
