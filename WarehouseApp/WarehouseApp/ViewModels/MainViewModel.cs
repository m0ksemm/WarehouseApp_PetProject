using Entities;
using GalaSoft.MvvmLight.Command;
using ServiceContracts;
using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.ServiceContracts;
using Services;
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
using WarehouseApp.ViewModels.ProductssViewModels;
using WarehouseApp.ViewModels.ProductsViewModels;
using WarehouseApp.ViewModels.WarehousesViewModel;
using WarehouseApp.Views.ProductsViews;
using WarehouseApp.Views.WarehousesViews;

namespace WarehouseApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IWarehousesService _warehousesService;

        public BaseViewModel CurrentViewModel => _navigationService.CurrentViewModel;

        private ObservableCollection<WarehouseResponse> _warehouses = new();
        public ObservableCollection<WarehouseResponse> Warehouses
        {
            get => _warehouses;
            set => SetProperty(ref _warehouses, value);
        }

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

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _warehousesService = new WarehousesService();

            NavigateToCategoriesCommand = new RelayCommand(_ => _navigationService.NavigateTo<CategoriesViewModel>());
            NavigateToProductsCommand = new RelayCommand(_ => _navigationService.NavigateTo<ProductsViewModel>());
            NavigateToManufacturersCommand = new RelayCommand(_ => _navigationService.NavigateTo<ManufacturersViewModel>());

            ToggleWarehousesMenuCommand = new RelayCommand(_ =>
                IsWarehousesExpanded = !IsWarehousesExpanded);

            AddWarehouseCommand = new RelayCommand(_ => AddWarehouse());
            EditWarehouseCommand = new RelayCommand(_ => EditWarehouse());
            DeleteWarehouseCommand = new RelayCommand(_ => DeleteWarehouse());

            SelectWarehouseCommand = new RelayCommand<WarehouseResponse>(OnWarehouseSelected);

            _navigationService.CurrentViewModelChanged += () =>
            {
                OnPropertyChanged(nameof(CurrentViewModel));
            };

            _navigationService.NavigateTo<CategoriesViewModel>();

            _ = LoadWarehouses();
        }

        private async Task LoadWarehouses()
        {
            try
            {
                var loaded = await _warehousesService.GetAllWarehouses();

                if (loaded != null)
                {
                    for (int i = 0; i < loaded.Count; i++)
                        loaded[i].RowNumber = i + 1;

                    Warehouses = new ObservableCollection<WarehouseResponse>(loaded);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading warehouses: {ex.Message}");
            }
        }

        private void OnWarehouseSelected(WarehouseResponse warehouse)
        {
            if (warehouse == null)
                return;

            _navigationService.NavigateTo<ProductsViewModel>();
        }

        private void AddWarehouse()
        {
            var window = new WarehouseAddEditView();
            var vm = new WarehouseAddEditViewModel(
                window,
                async result =>
                {
                    if (result != null)
                    {
                        var req = new WarehouseAddRequest
                        {
                            WarehouseName = result.WarehouseName,
                            Address = result.Address,
                            SquareArea = result.SquareArea
                        };

                        try
                        {
                            await _warehousesService.AddWarehouse(req);
                            await LoadWarehouses();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error adding warehouse: {ex.Message}");
                        }
                    }
                }
            );

            window.DataContext = vm;
            window.ShowDialog();
        }

        private void EditWarehouse()
        {

            // TODO: реалізація редагування
        }

        private void DeleteWarehouse()
        {

            // TODO: реалізація видалення
        }
    }
}
