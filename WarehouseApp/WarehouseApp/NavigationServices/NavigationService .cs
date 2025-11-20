using Entities;
using Microsoft.Extensions.DependencyInjection;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.ViewModels;
using WarehouseApp.ViewModels.WarehouseProductsViewModel;


namespace WarehouseApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public event Action CurrentViewModelChanged;

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // 1️⃣ Навігація через DI
        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            CurrentViewModel = viewModel;
        }

        // 2️⃣ Навігація переданим ViewModel
        public void NavigateTo(BaseViewModel viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
