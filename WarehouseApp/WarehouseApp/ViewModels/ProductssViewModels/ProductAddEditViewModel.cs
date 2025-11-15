using Entities;
using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.DTOs.ProductsDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarehouseApp.ViewModels.ProductssViewModels
{
    public class ProductAddEditViewModel : BaseViewModel
    {
        private readonly Action<ProductResponse?> _onSave;
        private readonly Window _window;

        // --- Properties ---  

        public string ProductName { get; set; } = string.Empty;

        public ObservableCollection<CategoryResponse> Categories { get; set; }
            = new ObservableCollection<CategoryResponse>();

        public CategoryResponse? SelectedCategory { get; set; }

        public ObservableCollection<ManufacturerResponse> Manufacturers { get; set; }
            = new ObservableCollection<ManufacturerResponse>();

        public ManufacturerResponse? SelectedManufacturer { get; set; }

        public double Weight { get; set; }
        public double Price { get; set; }
        public string BarCode { get; set; } = string.Empty;

        public Guid ProductID { get; set; }

        // --- Commands ---  
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        // --- Constructor ---  
        public ProductAddEditViewModel(
            Window window,
            Action<ProductResponse?> onSave,
            ObservableCollection<CategoryResponse> categories,
            ObservableCollection<ManufacturerResponse> manufacturers,
            ProductResponse? existingProduct = null)
        {
            _window = window;
            _onSave = onSave;

            Categories = categories;
            Manufacturers = manufacturers;

            // Якщо редагуємо — заповнюємо поля  
            if (existingProduct != null)
            {
                ProductID = existingProduct.ProductID;
                ProductName = existingProduct.ProductName ?? "";

                SelectedCategory = Categories.FirstOrDefault(c => c.CategoryID == existingProduct.CategoryID);
                SelectedManufacturer = Manufacturers.FirstOrDefault(m => m.ManufacturerID == existingProduct.ManufacturerID);

                Weight = existingProduct.Weight ?? 0;
                Price = existingProduct.Price ?? 0;
                BarCode = existingProduct.BarCode ?? "";
            }

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        // --- Methods ---  
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                MessageBox.Show("Product name cannot be empty.");
                return;
            }

            if (SelectedCategory == null)
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (SelectedManufacturer == null)
            {
                MessageBox.Show("Please select a manufacturer.");
                return;
            }

            if (Weight <= 0)
            {
                MessageBox.Show("Weight must be greater than zero.");
                return;
            }

            if (Price <= 0)
            {
                MessageBox.Show("Price must be greater than zero.");
                return;
            }

            var result = new ProductResponse
            {
                ProductID = ProductID == Guid.Empty ? Guid.NewGuid() : ProductID,
                ProductName = ProductName,

                CategoryID = SelectedCategory.CategoryID,
                ManufacturerID = SelectedManufacturer.ManufacturerID,

                Weight = Weight,
                Price = Price,
                BarCode = BarCode
            };

            _onSave?.Invoke(result);
            _window.Close();
        }

        private void Cancel()
        {
            _onSave?.Invoke(null);
            _window.Close();
        }
    }
}
