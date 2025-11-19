using Microsoft.Extensions.DependencyInjection;
using ServiceContracts;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using WarehouseApp.ViewModels;
using WarehouseApp.ViewModels.CategoriesViewModels;
using WarehouseApp.ViewModels.ManufacturersViewModels;
using WarehouseApp.ViewModels.ProductsViewModels;
using WarehouseApp.ViewModels.WarehouseProductsViewModel;

namespace WarehouseApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Сервіси
            services.AddSingleton<INavigationService, WarehouseApp.Services.NavigationService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddTransient<CategoriesViewModel>();
            services.AddTransient<ProductsViewModel>();
            services.AddTransient<ManufacturersViewModel>();
            services.AddTransient<WarehouseProductsViewModel>();

            // Вікна
            services.AddSingleton<Views.MainWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<Views.MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }
    }

}
