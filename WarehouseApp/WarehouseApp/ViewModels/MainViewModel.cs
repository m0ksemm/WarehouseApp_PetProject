using GalaSoft.MvvmLight.Command;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand NavigateToManufacturersCommand { get; }
        public ICommand ToggleWarehousesMenuCommand { get; }
        public ICommand SelectWarehouseCommand { get; }

        private bool _isWarehousesExpanded;
        public bool IsWarehousesExpanded
        {
            get => _isWarehousesExpanded;
            set => SetProperty(ref _isWarehousesExpanded, value);
        }

        public ObservableCollection<string> Warehouses { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateToCategoriesCommand = new RelayCommand(_ => _navigationService.NavigateTo<CategoriesViewModel>());
            NavigateToProductsCommand = new RelayCommand(_ => _navigationService.NavigateTo<ProductsViewModel>());
            NavigateToManufacturersCommand = new RelayCommand(_ => _navigationService.NavigateTo<ManufacturersViewModel>());

            ToggleWarehousesMenuCommand = new RelayCommand(_ =>
                IsWarehousesExpanded = !IsWarehousesExpanded);

            SelectWarehouseCommand = new RelayCommand<string>(OnWarehouseSelected);
            _navigationService.CurrentViewModelChanged += () =>
            {
                OnPropertyChanged(nameof(CurrentViewModel));
            };

            Warehouses = new ObservableCollection<string>
            {
                "Main Warehouse",
                "Backup Storage",
                "Downtown Depot"
            };

            // Початковий екран
            _navigationService.NavigateTo<CategoriesViewModel>();
        }

        private void OnWarehouseSelected(string warehouseName)
        {
            // Навігація до продуктів конкретного складу
            // Поки просто ProductsViewModel, далі можна передати warehouseName як параметр
            _navigationService.NavigateTo<ProductsViewModel>();
        }
    }
}
