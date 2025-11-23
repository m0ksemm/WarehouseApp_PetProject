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

namespace WarehouseApp.ViewModels.WarehouseProductsViewModel
{
    public class WarehouseProductsViewModel : BaseViewModel
    {
        private readonly WarehouseResponse _warehouse;

        private readonly IWarehouseProductsService _warehouseProductsService;
        private readonly ICategoriesService _categoriesService;
        private readonly IManufacturersService _manufacturersService;

        public string WarehouseTitle => $"Products in '{_warehouse.WarehouseName}'";

        public ObservableCollection<WarehouseProductResponse> WarehouseProducts { get; set; }
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


        public WarehouseProductResponse SelectedProduct { get; set; }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; FilterProducts(); OnPropertyChanged(); }
        }
        private string _searchText;

        public CategoryResponse SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; FilterProducts(); OnPropertyChanged(); }
        }
        private CategoryResponse _selectedCategory;

        public ManufacturerResponse SelectedManufacturer
        {
            get => _selectedManufacturer;
            set { _selectedManufacturer = value; FilterProducts(); OnPropertyChanged(); }
        }
        private ManufacturerResponse _selectedManufacturer;

        private List<WarehouseProductResponse> _allProducts;

        public WarehouseProductsViewModel(WarehouseResponse warehouse)
        {
            _warehouse = warehouse;
            _warehouseProductsService = new WarehouseProductsService();
            _categoriesService = new CategoriesService();
            _manufacturersService = new ManufacturersService();

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
                if (warehouseProducts != null)
                {
                    for (int i = 0; i < warehouseProducts.Count; i++)
                        warehouseProducts[i].RowNumber = i + 1;

                    _allProducts = warehouseProducts.ToList(); // не забуваємо

                    WarehouseProducts = new ObservableCollection<WarehouseProductResponse>(warehouseProducts);
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
            var filtered = _allProducts.AsEnumerable();

            //if (!string.IsNullOrWhiteSpace(SearchText))
            //    filtered = filtered.Where(p => !string.IsNullOrEmpty(p.ProductName) && p.ProductName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
            //        || p.BarCode.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
            //        || p.Category.CategoryName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0
            //        || p.Manufacturer.ManufacturerName.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);

            //if (SelectedCategoryFilter != null && SelectedCategoryFilter.CategoryID != Guid.Empty)
            //    filtered = filtered.Where(p => p.Category != null && p.Category.CategoryID == SelectedCategoryFilter.CategoryID);

            //if (SelectedManufacturerFilter != null && SelectedManufacturerFilter.ManufacturerID != Guid.Empty)
            //    filtered = filtered.Where(p => p.Manufacturer != null && p.Manufacturer.ManufacturerID == SelectedManufacturerFilter.ManufacturerID);

            //if (MinPriceFilter.HasValue)
            //    filtered = filtered.Where(p => p.Price >= (double)MinPriceFilter.Value);

            //if (MaxPriceFilter.HasValue)
            //    filtered = filtered.Where(p => p.Price <= (double)MaxPriceFilter.Value);

            //var result = filtered.Select((p, idx) => { p.RowNumber = idx + 1; return p; }).ToList();
            //Products = new ObservableCollection<ProductResponse>(result);
        }

        private void FilterProducts()
        {
            //if (_allProducts == null) return;

            //var filtered = _allProducts.AsEnumerable();

            //if (!string.IsNullOrWhiteSpace(SearchText))
            //    filtered = filtered.Where(p => p.Product.ProductName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            //if (SelectedCategory != null)
            //    filtered = filtered.Where(p => p.Product.Category.Id == SelectedCategory.Id);

            //if (SelectedManufacturer != null)
            //    filtered = filtered.Where(p => p.Product.Manufacturer.Id == SelectedManufacturer.Id);

            //WarehouseProducts = new ObservableCollection<WarehouseProductResponse>(filtered);
            //OnPropertyChanged(nameof(WarehouseProducts));
        }
    }

}
