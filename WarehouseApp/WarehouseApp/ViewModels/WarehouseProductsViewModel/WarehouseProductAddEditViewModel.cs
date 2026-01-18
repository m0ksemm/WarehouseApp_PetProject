using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarehouseApp.ViewModels.WarehouseProductsViewModel
{
    public class WarehouseProductAddEditViewModel : BaseViewModel
    {
        private readonly Action<WarehouseProductResponse?> _onSave;
        private readonly Window _window;

        public ObservableCollection<ProductResponse> Products { get; } = new ObservableCollection<ProductResponse>();
        public ObservableCollection<ProductResponse> FilteredProducts { get; }

        private ProductResponse? _selectedProduct;
        public ProductResponse? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged();

                    if (_selectedProduct != null) 
                    {
                        _productSearchText = _selectedProduct.ProductName;
                        OnPropertyChanged(nameof(ProductSearchText));
                    }
                        
                }
            }
        }
        private string _productSearchText = "";
        public string ProductSearchText
        {
            get => _productSearchText;
            set
            {
                if (_productSearchText != value)
                {
                    _productSearchText = value;
                    OnPropertyChanged();
                    FilterProducts();
                }
            }
        }

        private string _count = "0";
        public string Count
        {
            get => _count;
            set
            {
                if (_count != value)
                {
                    _count = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid WarehouseProductID { get; set; }
        public Guid WarehouseID { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public WarehouseProductAddEditViewModel(
            Window window,
            Action<WarehouseProductResponse?> onSave,
            ObservableCollection<ProductResponse> products, Guid warehouseID,
            WarehouseProductResponse? existingWarehouseProduct = null
            )
        {
            _window = window;
            _onSave = onSave;

            Products = products;
            FilteredProducts = new ObservableCollection<ProductResponse>(products);
            WarehouseID = warehouseID;

            if (existingWarehouseProduct != null)
            {
                WarehouseProductID = existingWarehouseProduct.WarehouseProductID;
                SelectedProduct = products.FirstOrDefault(p => p.ProductID == existingWarehouseProduct.ProductID);
                Count = existingWarehouseProduct.Count.ToString();
            }


            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Save()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            if (!int.TryParse(Count, out int parsedCount) || parsedCount <= 0)
            {
                MessageBox.Show("Count must be a valid number greater than zero.");
                return;
            }

            var result = new WarehouseProductResponse
            {
                WarehouseProductID = WarehouseProductID == Guid.Empty ? Guid.NewGuid() : WarehouseProductID,
                WarehouseID = WarehouseID,
                ProductID = SelectedProduct.ProductID,
                Count = parsedCount
            };

            _onSave?.Invoke(result);
            _window.Close();
        }

        private void Cancel()
        {
            _onSave?.Invoke(null);
            _window.Close();
        }

        private void FilterProducts()
        {
            var filtered = Products
                .Where(p => string.IsNullOrWhiteSpace(ProductSearchText) ||
                            p.ToString().Contains(ProductSearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredProducts.Clear();
            foreach (var p in filtered)
                FilteredProducts.Add(p);
        }
    }

}
