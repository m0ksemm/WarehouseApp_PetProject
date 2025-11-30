using Entities;
using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
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
using WarehouseApp.ViewModels.ProductssViewModels;
using WarehouseApp.Views.ProductsViews;
using WarehouseApp.Views.WarehouseProductsViews;

namespace WarehouseApp.ViewModels.WarehouseProductsViewModel
{
    public class WarehouseProductsViewModel : BaseViewModel
    {
        private readonly WarehouseResponse _warehouse;

        private readonly IWarehouseProductsService _warehouseProductsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IManufacturersService _manufacturersService;
        private readonly IProductsService _productsService;

        public string WarehouseTitle => $"Products in '{_warehouse.WarehouseName}'";

        private ObservableCollection<WarehouseProductResponse> _warehouseProducts = new();
        public ObservableCollection<WarehouseProductResponse> WarehouseProducts
        {
            get => _warehouseProducts;
            set => SetProperty(ref _warehouseProducts, value);
        }

        public ObservableCollection<CategoryResponse> Categories { get; set; }
        public ObservableCollection<ManufacturerResponse> Manufacturers { get; set; }


        // Categories filters
        private ObservableCollection<CategoryResponse> _categoriesFilter = new();
        private ObservableCollection<CategoryResponse> _allCategories = new();
        public ObservableCollection<CategoryResponse> CategoriesFilter
        {
            get => _categoriesFilter;
            set => SetProperty(ref _categoriesFilter, value);
        }

        // Manufacturers filters
        private ObservableCollection<ManufacturerResponse> _manufacturersFilter = new();
        private ObservableCollection<ManufacturerResponse> _allManufacturers = new();
        public ObservableCollection<ManufacturerResponse> ManufacturersFilter
        {
            get => _manufacturersFilter;
            set => SetProperty(ref _manufacturersFilter, value);
        }

        // Selected filters
        private CategoryResponse _selectedCategoryFilter;
        public CategoryResponse SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                if (SetProperty(ref _selectedCategoryFilter, value))
                {
                    //if (value != null && value.CategoryName != "All Categories")
                    //    CategorySearchText = value.CategoryName!;
                    //else
                    //    CategorySearchText = string.Empty;

                    ApplyFilter();
                }
            }
        }

        private ManufacturerResponse _selectedManufacturerFilter;
        public ManufacturerResponse SelectedManufacturerFilter
        {
            get => _selectedManufacturerFilter;
            set
            {
                if (SetProperty(ref _selectedManufacturerFilter, value)) 
                {

                    ApplyFilter();
                }                    
            }
        }


        private WarehouseProductResponse _selectedWarehouseProduct;

        public WarehouseProductResponse SelectedWarehouseProduct 
        {
            get => _selectedWarehouseProduct;
            set => SetProperty(ref _selectedWarehouseProduct, value);
        }

        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set { if (SetProperty(ref _searchText, value)) ApplyFilter(); }
        }


        public CategoryResponse SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; ApplyFilter(); OnPropertyChanged(); }
        }
        private CategoryResponse _selectedCategory;

        public ManufacturerResponse SelectedManufacturer
        {
            get => _selectedManufacturer;
            set { _selectedManufacturer = value; ApplyFilter(); OnPropertyChanged(); }
        }
        private ManufacturerResponse _selectedManufacturer;

        private List<WarehouseProductResponse> _allWarehouseProducts;

        private ObservableCollection<ProductResponse> _allProducts;


        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public WarehouseProductsViewModel(WarehouseResponse warehouse)
        {
            _warehouse = warehouse;
            _warehouseProductsService = new WarehouseProductsService();
            _categoriesService = new CategoriesService();
            _manufacturersService = new ManufacturersService();
            _productsService = new ProductsService();

            AddCommand = new RelayCommand(async _ => await AddWarehouseProduct());
            UpdateCommand = new RelayCommand(async _ => await UpdateProduct(), _ => SelectedWarehouseProduct != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteProduct(), _ => SelectedWarehouseProduct != null);

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadFilters();
            await LoadData();
        }

        private async Task LoadData()
        {
            // Завантаження
            try
            {
                var warehouseProducts = await _warehouseProductsService.GetWarehouseProductsByWarehouseId(_warehouse.WarehouseID);
                var allProducts = await _productsService.GetAllProducts();
                if (warehouseProducts != null)
                {
                    for (int i = 0; i < warehouseProducts.Count; i++)
                        warehouseProducts[i].RowNumber = i + 1;

                    _allWarehouseProducts = warehouseProducts.ToList(); // не забуваємо ||||||||||\ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!this is  not all
                    ApplyFilter();

                    WarehouseProducts = new ObservableCollection<WarehouseProductResponse>(warehouseProducts);
                    _allProducts = new ObservableCollection<ProductResponse>(allProducts);
                    OnPropertyChanged(nameof(WarehouseProducts)); // MUST HAVE
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private async Task LoadFilters()
        {
            try
            {
                var categories = await _categoriesService.GetAllCategories();
                var manufacturers = await _manufacturersService.GetAllManufacturers();

                _allCategories = new ObservableCollection<CategoryResponse>(categories ?? Enumerable.Empty<CategoryResponse>());
                _allManufacturers = new ObservableCollection<ManufacturerResponse>(manufacturers ?? Enumerable.Empty<ManufacturerResponse>());

                var allCat = new CategoryResponse { CategoryID = Guid.Empty, CategoryName = "All Categories" };
                var allMan = new ManufacturerResponse { ManufacturerID = Guid.Empty, ManufacturerName = "All Manufacturers" };

                var cats = new ObservableCollection<CategoryResponse> { allCat };
                foreach (var c in _allCategories) cats.Add(c);
                CategoriesFilter = cats;

                var mans = new ObservableCollection<ManufacturerResponse> { allMan };
                foreach (var m in _allManufacturers) mans.Add(m);
                ManufacturersFilter = mans;

                SelectedCategoryFilter = allCat;
                SelectedManufacturerFilter = allMan;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading filters: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            var filtered = _allWarehouseProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => !string.IsNullOrEmpty(p.Product.ProductName) && p.Product.ProductName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Product.BarCode.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Product.Category.CategoryName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Product.Manufacturer.ManufacturerName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Product.Price.ToString().IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Product.BarCode.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

            if (SelectedCategoryFilter != null && SelectedCategoryFilter.CategoryID != Guid.Empty)
                filtered = filtered.Where(p => p.Product.Category != null && p.Product.Category.CategoryID == SelectedCategoryFilter.CategoryID);

            if (SelectedManufacturerFilter != null && SelectedManufacturerFilter.ManufacturerID != Guid.Empty)
                filtered = filtered.Where(p => p.Product.Manufacturer != null && p.Product.Manufacturer.ManufacturerID == SelectedManufacturerFilter.ManufacturerID);


            var result = filtered.Select((p, idx) => { p.RowNumber = idx + 1; return p; }).ToList();
            WarehouseProducts = new ObservableCollection<WarehouseProductResponse>(result);
        }

        private async Task AddWarehouseProduct()
        {
            var window = new WarehouseProductAddEditView();

            var vm = new WarehouseProductAddEditViewModel(
                window,
                async result =>
                {
                    if (result != null)
                    {
                        var addReq = new WarehouseProductAddRequest
                        {
                            ProductID = result.ProductID,
                            WarehouseID = result.WarehouseID,
                            Count = result.Count                       
                        };
                        try
                        {
                            await _warehouseProductsService.AddWarehouseProduct(addReq);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error adding product: {ex.Message}");
                        }

                        await LoadData();
                    }
                },
                _allProducts,
                _warehouse.WarehouseID,
                SelectedWarehouseProduct

            );

            window.DataContext = vm;
            window.ShowDialog();
        }


        private async Task UpdateProduct()
        {
            if (SelectedWarehouseProduct == null) return;

            var window = new WarehouseProductAddEditView();
            var vm = new WarehouseProductAddEditViewModel(window, async result =>
            {
                if (result != null)
                {
                    var updateRequest = new WarehouseProductUpdateRequest
                    {

                        WarehouseProductID = SelectedWarehouseProduct.WarehouseProductID,
                        WarehouseID = result.WarehouseID.Value,
                        ProductID = result.ProductID.Value,
                        Count = result.Count
                    };

                    try
                    {
                        await _warehouseProductsService.UpdateWarehouseProduct(updateRequest);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating product: {ex.Message}");
                    }

                    await LoadData();
                }
            }, _allProducts,
                _warehouse.WarehouseID, 
                SelectedWarehouseProduct);

            window.DataContext = vm;
            window.ShowDialog();
        }
        private async Task DeleteProduct()
        {
            var window = new WarehouseProductDeleteView();
            var vm = new WarehouseProductDeleteViewModel(window, 
                SelectedWarehouseProduct.Product.ProductName ?? "this product",
                _warehouse.WarehouseName ?? "this warehouse", 
                async confirmed =>
                {
                    if (confirmed)
                    {
                        try
                        {
                            await _warehouseProductsService.DeleteWarehouseProduct(SelectedWarehouseProduct.WarehouseProductID);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting product: {ex.Message}");
                        }
                        await LoadData();
                    }
                });
            window.DataContext = vm;
            window.ShowDialog();
        }
    }

}
