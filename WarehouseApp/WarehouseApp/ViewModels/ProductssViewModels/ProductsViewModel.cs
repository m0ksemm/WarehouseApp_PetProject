using Entities;
using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.DTOs.ProductsDTOs;
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
using WarehouseApp.ViewModels.ManufacturersViewModels;
using WarehouseApp.ViewModels.ProductssViewModels;
using WarehouseApp.Views.ManufacturersViews;
using WarehouseApp.Views.ProductsViews;

namespace WarehouseApp.ViewModels.ProductsViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IManufacturersService _manufacturersService;

        // Products
        private ObservableCollection<ProductResponse> _products = new();
        public ObservableCollection<ProductResponse> Products
        {
            get => _products;
            set => SetProperty(ref _products, value);
        }
        private ObservableCollection<ProductResponse> _allProducts = new();

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
                    if (value != null && value.CategoryName != "All Categories")
                        CategorySearchText = value.CategoryName!;
                    else
                        CategorySearchText = string.Empty;

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
                    ApplyFilter();
            }
        }

        // Search text for ComboBoxes (live filter)
        private string _categorySearchText;
        public string CategorySearchText
        {
            get => _categorySearchText;
            set
            {
                if (SetProperty(ref _categorySearchText, value))
                    ApplyCategorySearch();
            }
        }

        private string _manufacturerSearchText;
        public string ManufacturerSearchText
        {
            get => _manufacturerSearchText;
            set
            {
                if (SetProperty(ref _manufacturerSearchText, value))
                    ApplyManufacturerSearch();
            }
        }

        // product search + price filters (text-backed)
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { if (SetProperty(ref _searchText, value)) ApplyFilter(); }
        }

        private string _minPriceFilterText;
        public string MinPriceFilterText
        {
            get => _minPriceFilterText;
            set
            {
                if (SetProperty(ref _minPriceFilterText, value))
                {
                    if (decimal.TryParse(value, out var p)) MinPriceFilter = p;
                    else MinPriceFilter = null;
                    ApplyFilter();
                }
            }
        }

        private string _maxPriceFilterText;
        public string MaxPriceFilterText
        {
            get => _maxPriceFilterText;
            set
            {
                if (SetProperty(ref _maxPriceFilterText, value))
                {
                    if (decimal.TryParse(value, out var p)) MaxPriceFilter = p;
                    else MaxPriceFilter = null;
                    ApplyFilter();
                }
            }
        }

        private decimal? _minPriceFilter;
        public decimal? MinPriceFilter { get => _minPriceFilter; set => SetProperty(ref _minPriceFilter, value); }

        private decimal? _maxPriceFilter;
        public decimal? MaxPriceFilter { get => _maxPriceFilter; set => SetProperty(ref _maxPriceFilter, value); }

        // Commands (stubs)
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        private ProductResponse _selectedProduct;
        public ProductResponse SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }

        public ProductsViewModel()
        {
            _productsService = new ProductsService();
            _categoriesService = new CategoriesService();
            _manufacturersService = new ManufacturersService(); 

            AddCommand = new RelayCommand(async _ => await AddProduct());
            UpdateCommand = new RelayCommand(async _ => await UpdateProduct(), _ => SelectedProduct != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteProduct(), _ => SelectedProduct != null);

            // load on background
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadFilters();
            await LoadProducts();
        }

        private async Task LoadFilters()
        {
            try
            {
                var categories = await _categoriesService.GetAllCategories();
                var manufacturers = await _manufacturersService.GetAllManufacturers();

                // keep full copies
                _allCategories = new ObservableCollection<CategoryResponse>(categories ?? Enumerable.Empty<CategoryResponse>());
                _allManufacturers = new ObservableCollection<ManufacturerResponse>(manufacturers ?? Enumerable.Empty<ManufacturerResponse>());

                // add "All ..." item at top
                var allCat = new CategoryResponse { CategoryID = Guid.Empty, CategoryName = "All Categories" };
                var allMan = new ManufacturerResponse { ManufacturerID = Guid.Empty, ManufacturerName = "All Manufacturers" };

                var cats = new ObservableCollection<CategoryResponse> { allCat };
                foreach (var c in _allCategories) cats.Add(c);
                CategoriesFilter = cats;

                var mans = new ObservableCollection<ManufacturerResponse> { allMan };
                foreach (var m in _allManufacturers) mans.Add(m);
                ManufacturersFilter = mans;

                // default selected
                SelectedCategoryFilter = allCat;
                SelectedManufacturerFilter = allMan;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading filters: {ex.Message}");
            }
        }

        private async Task LoadProducts()
        {
            try
            {
                var products = await _productsService.GetAllProducts();
                if (products != null)
                {
                    for (int i = 0; i < products.Count; i++)
                        products[i].RowNumber = i + 1;

                    _allProducts = new ObservableCollection<ProductResponse>(products);
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void ApplyCategorySearch()
        {
            if (string.IsNullOrWhiteSpace(CategorySearchText))
            {
                // rebuild from full
                var cats = new ObservableCollection<CategoryResponse> { new CategoryResponse { CategoryID = Guid.Empty, CategoryName = "All Categories" } };
                foreach (var c in _allCategories) cats.Add(c);
                CategoriesFilter = cats;
            }
            else
            {
                var filtered = _allCategories
                    .Where(c => !string.IsNullOrWhiteSpace(c.CategoryName) && c.CategoryName.IndexOf(CategorySearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                var cats = new ObservableCollection<CategoryResponse> { new CategoryResponse { CategoryID = Guid.Empty, CategoryName = "All Categories" } };
                foreach (var c in filtered) cats.Add(c);
                CategoriesFilter = cats;
            }
        }

        private void ApplyManufacturerSearch()
        {
            if (string.IsNullOrWhiteSpace(ManufacturerSearchText))
            {
                var mans = new ObservableCollection<ManufacturerResponse> { new ManufacturerResponse { ManufacturerID = Guid.Empty, ManufacturerName = "All Manufacturers" } };
                foreach (var m in _allManufacturers) mans.Add(m);
                ManufacturersFilter = mans;
            }
            else
            {
                var filtered = _allManufacturers
                    .Where(m => !string.IsNullOrWhiteSpace(m.ManufacturerName) && m.ManufacturerName.IndexOf(ManufacturerSearchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                var mans = new ObservableCollection<ManufacturerResponse> { new ManufacturerResponse { ManufacturerID = Guid.Empty, ManufacturerName = "All Manufacturers" } };
                foreach (var m in filtered) mans.Add(m);
                ManufacturersFilter = mans;
            }
        }

        private void ApplyFilter()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => !string.IsNullOrEmpty(p.ProductName) && p.ProductName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0  
                    || p.BarCode.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Category.CategoryName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
                    || p.Manufacturer.ManufacturerName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0 );

            if (SelectedCategoryFilter != null && SelectedCategoryFilter.CategoryID != Guid.Empty)
                filtered = filtered.Where(p => p.Category != null && p.Category.CategoryID == SelectedCategoryFilter.CategoryID);

            if (SelectedManufacturerFilter != null && SelectedManufacturerFilter.ManufacturerID != Guid.Empty)
                filtered = filtered.Where(p => p.Manufacturer != null && p.Manufacturer.ManufacturerID == SelectedManufacturerFilter.ManufacturerID);

            if (MinPriceFilter.HasValue)
                filtered = filtered.Where(p => p.Price >= (double)MinPriceFilter.Value);

            if (MaxPriceFilter.HasValue)
                filtered = filtered.Where(p => p.Price <= (double)MaxPriceFilter.Value);

            var result = filtered.Select((p, idx) => { p.RowNumber = idx + 1; return p; }).ToList();
            Products = new ObservableCollection<ProductResponse>(result);
        }

        private async Task AddProduct()
        {
            var window = new ProductAddEditView();

            var vm = new ProductAddEditViewModel(
                window,
                async result =>
                {
                    if (result != null)
                    {
                        var addReq = new ProductAddRequest
                        {
                            ProductName = result.ProductName,
                            CategoryID = result.CategoryID,
                            ManufacturerID = result.ManufacturerID,
                            Weight = result.Weight,
                            Price = result.Price,
                            BarCode = result.BarCode
                        };

                        try
                        {
                            await _productsService.AddProduct(addReq);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error adding product: {ex.Message}");
                        }

                        await LoadProducts();
                    }
                },

                _allCategories,        
                _allManufacturers      
            );

            window.DataContext = vm;
            window.ShowDialog();
        }
        private async Task UpdateProduct() 
        {
            if (SelectedProduct == null) return;

            var window = new ProductAddEditView();
            var vm = new ProductAddEditViewModel(window, async result =>
            {
                if (result != null)
                {
                    var updateRequest = new ProductUpdateRequest
                    {
                        ProductID = SelectedProduct.ProductID,
                        ManufacturerID = result.ManufacturerID,
                        CategoryID = result.CategoryID,
                        ProductName = result.ProductName,
                        Weight = result.Weight,
                        Price = result.Price,
                        BarCode = result.BarCode
                    };

                    try
                    {
                        await _productsService.UpdateProduct(updateRequest);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating product: {ex.Message}");
                    }

                    await LoadProducts();
                }
            }, _allCategories,
                _allManufacturers, existingProduct: SelectedProduct);

            window.DataContext = vm;
            window.ShowDialog();
        }
        private async Task DeleteProduct() 
        {

            var window = new ProductDeleteView();
            var vm = new ProductDeleteViewModel(window, SelectedProduct.ProductName ?? "this product", async confirmed =>
            {
                if (confirmed)
                {
                    try
                    {
                        await _productsService.DeleteProduct(SelectedProduct.ProductID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting product: {ex.Message}");
                    }
                    await LoadProducts();
                }
            });
            window.DataContext = vm;
            window.ShowDialog();
        }
    }
}

