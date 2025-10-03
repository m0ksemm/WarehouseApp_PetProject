using GalaSoft.MvvmLight.Command;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WarehouseApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public BaseViewModel CurrentViewModel => _navigationService.CurrentViewModel;

        public ICommand NavigateToCategoriesCommand { get; }
        public ICommand NavigateToProductsCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToCategoriesCommand = new RelayCommand(_ => _navigationService.NavigateTo<CategoriesViewModel>());
            NavigateToProductsCommand = new RelayCommand(_ => _navigationService.NavigateTo<ProductsViewModel>());

            _navigationService.CurrentViewModelChanged += () =>
            {
                OnPropertyChanged(nameof(CurrentViewModel));
            };

            // Початковий екран
            _navigationService.NavigateTo<CategoriesViewModel>();
        }
    }
}
