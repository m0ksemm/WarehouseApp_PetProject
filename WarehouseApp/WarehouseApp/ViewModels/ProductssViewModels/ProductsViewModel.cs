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
using System.Windows.Input;

namespace WarehouseApp.ViewModels.ProductsViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IManufacturersService _manufacturersService;

        public ObservableCollection<ProductResponse> Products { get; set; } = new();
        private ObservableCollection<ProductResponse> _allProducts = new();

        public ObservableCollection<CategoryResponse> CategoriesFilter { get; set; } = new();
        public ObservableCollection<ManufacturerResponse> ManufacturersFilter { get; set; } = new();

        private CategoryResponse _selectedCategoryFilter;
        public CategoryResponse SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                _selectedCategoryFilter = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private ManufacturerResponse _selectedManufacturerFilter;
        public ManufacturerResponse SelectedManufacturerFilter
        {
            get => _selectedManufacturerFilter;
            set
            {
                _selectedManufacturerFilter = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private string _minPriceFilterText;
        public string MinPriceFilterText
        {
            get => _minPriceFilterText;
            set
            {
                _minPriceFilterText = value;
                OnPropertyChanged();
                if (decimal.TryParse(value, out var parsed))
                    MinPriceFilter = parsed;
                else
                    MinPriceFilter = null;
                ApplyFilter();
            }
        }

        private string _maxPriceFilterText;
        public string MaxPriceFilterText
        {
            get => _maxPriceFilterText;
            set
            {
                _maxPriceFilterText = value;
                OnPropertyChanged();
                if (decimal.TryParse(value, out var parsed))
                    MaxPriceFilter = parsed;
                else
                    MaxPriceFilter = null;
                ApplyFilter();
            }
        }

        //private string _categorySearchText;
        //public string CategorySearchText
        //{
        //    get => _categorySearchText;
        //    set
        //    {
        //        _categorySearchText = value;
        //        OnPropertyChanged();
        //        ApplyCategoryFilter();
        //    }
        //}

        //private void ApplyCategoryFilter()
        //{
        //    if (string.IsNullOrWhiteSpace(CategorySearchText))
        //        CategoriesFilter = new ObservableCollection<CategoryResponse>(_allCategories);
        //    else
        //        CategoriesFilter = new ObservableCollection<CategoryResponse>(
        //            _allCategories.Where(c =>
        //                c.CategoryName.Contains(CategorySearchText, StringComparison.OrdinalIgnoreCase)));
        //}

        private decimal? _minPriceFilter;
        public decimal? MinPriceFilter
        {
            get => _minPriceFilter;
            set { _minPriceFilter = value; OnPropertyChanged(); }
        }

        private decimal? _maxPriceFilter;
        public decimal? MaxPriceFilter
        {
            get => _maxPriceFilter;
            set { _maxPriceFilter = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ProductsViewModel()
        {
            _productsService = new ProductsService();
            _categoriesService = new CategoriesService();
            _manufacturersService = new ManufacturersService();

            AddCommand = new RelayCommand(async _ => await AddProduct());
            UpdateCommand = new RelayCommand(async _ => await UpdateProduct(), _ => SelectedProduct != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteProduct(), _ => SelectedProduct != null);

            Task.Run(async () =>
            {
                await LoadFilters();
                await LoadProducts();
            });
        }

        private async Task LoadFilters()
        {
            try
            {
                var categories = await _categoriesService.GetAllCategories();
                var manufacturers = await _manufacturersService.GetAllManufacturers();

                App.Current.Dispatcher.Invoke(() =>
                {
                    CategoriesFilter.Clear();
                    ManufacturersFilter.Clear();

                    CategoriesFilter.Add(new CategoryResponse { CategoryName = "All Categories" });
                    foreach (var c in categories)
                        CategoriesFilter.Add(c);

                    ManufacturersFilter.Add(new ManufacturerResponse { ManufacturerName = "All Manufacturers" });
                    foreach (var m in manufacturers)
                        ManufacturersFilter.Add(m);
                });
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

                for (int i = 0; i < products.Count; i++)
                    products[i].RowNumber = i + 1;

                App.Current.Dispatcher.Invoke(() =>
                {
                    _allProducts = new ObservableCollection<ProductResponse>(products);
                    ApplyFilter();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            var filtered = _allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
                filtered = filtered.Where(p => p.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            if (SelectedCategoryFilter != null && SelectedCategoryFilter.CategoryName != "All Categories")
                filtered = filtered.Where(p => p.Category.CategoryName == SelectedCategoryFilter.CategoryName);

            if (SelectedManufacturerFilter != null && SelectedManufacturerFilter.ManufacturerName != "All Manufacturers")
                filtered = filtered.Where(p => p.Manufacturer.ManufacturerName == SelectedManufacturerFilter.ManufacturerName);

            if (MinPriceFilter.HasValue)
                filtered = filtered.Where(p => p.Price >= (double)MinPriceFilter.Value);

            if (MaxPriceFilter.HasValue)
                filtered = filtered.Where(p => p.Price <= (double)MaxPriceFilter.Value);

            Products = new ObservableCollection<ProductResponse>(filtered);
            OnPropertyChanged(nameof(Products));
        }



        // Далі залишаєш свій код для Add / Update / Delete
        private async Task AddProduct() { /* ... */ }
        private async Task UpdateProduct() { /* ... */ }
        private async Task DeleteProduct() { /* ... */ }

        private ProductResponse _selectedProduct;
        public ProductResponse SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }
    }
}

