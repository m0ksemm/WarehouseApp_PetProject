using Microsoft.Extensions.DependencyInjection;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.ViewModels;


namespace WarehouseApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

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

        public event Action CurrentViewModelChanged;

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();
            CurrentViewModel = viewModel;
        }
    }
}
