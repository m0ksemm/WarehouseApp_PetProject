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

            LoadData();
        }

        private async void LoadData()
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
