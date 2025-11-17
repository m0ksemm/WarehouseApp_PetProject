using GalaSoft.MvvmLight.Command;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WarehouseApp.ViewModels.CategoriesViewModels;
using WarehouseApp.ViewModels.ManufacturersViewModels;
using WarehouseApp.ViewModels.ProductsViewModels;
using WarehouseApp.ViewModels.WarehousesViewModel;
using WarehouseApp.Views.WarehousesViews;

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

        public ICommand AddWarehouseCommand { get; }
        public ICommand EditWarehouseCommand { get; }
        public ICommand DeleteWarehouseCommand { get; }

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

            AddWarehouseCommand = new RelayCommand(_ => AddWarehouse());
            EditWarehouseCommand = new RelayCommand<string>(EditWarehouse);
            DeleteWarehouseCommand = new RelayCommand<string>(DeleteWarehouse);

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

        private void AddWarehouse()
        {
            var dialog = new WarehouseAddEditView();               // Створюємо вікно
            var vm = new WarehouseAddEditViewModel(name =>
            {
                if (!string.IsNullOrWhiteSpace(name))
                    Warehouses.Add(name);
            });
            dialog.DataContext = vm;

            dialog.ShowDialog();
        }

        private void EditWarehouse(string warehouse)
        {
            if (warehouse == null) return;

            var dialog = new WarehouseAddEditView();
            var vm = new WarehouseAddEditViewModel(newName =>
            {
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    int index = Warehouses.IndexOf(warehouse);
                    Warehouses[index] = newName;
                }
            }, warehouse);

            dialog.DataContext = vm;
            dialog.ShowDialog();
        }

        private void DeleteWarehouse(string warehouse)
        {
            if (warehouse == null) return;

            if (MessageBox.Show($"Delete warehouse '{warehouse}'?",
                                "Confirm",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Warehouses.Remove(warehouse);
            }
        }
    }
}
