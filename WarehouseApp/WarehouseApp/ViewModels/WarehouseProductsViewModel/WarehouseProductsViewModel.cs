using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
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

            LoadData();
        }

        private async void LoadData()
        {
            // Завантаження
            //_allProducts = await Api.GetWarehouseProducts(_warehouse.Id);
            //Categories = new ObservableCollection<CategoryResponse>(await Api.GetCategories());
            //Manufacturers = new ObservableCollection<ManufacturerResponse>(await Api.GetManufacturers());

            //WarehouseProducts = new ObservableCollection<WarehouseProductResponse>(_allProducts);
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
