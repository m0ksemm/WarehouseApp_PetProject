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

        public string ProductName { get; set; } = string.Empty;

        public ObservableCollection<CategoryResponse> Categories { get; set; } = new ObservableCollection<CategoryResponse>();

        private CategoryResponse? _selectedCategory;
        public CategoryResponse? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    OnPropertyChanged();

                    if (_selectedCategory != null)
                    {
                        _categorySearchText = _selectedCategory.CategoryName;
                        OnPropertyChanged(nameof(CategorySearchText));
                    }
                }
            }
        }
        private string _categorySearchText = "";
        public string CategorySearchText
        {
            get => _categorySearchText;
            set
            {
                if (_categorySearchText != value)
                {
                    _categorySearchText = value;
                    OnPropertyChanged();
                    FilterCategories();
                }
            }
        }


        public ObservableCollection<ManufacturerResponse> Manufacturers { get; set; } = new ObservableCollection<ManufacturerResponse>();

        private ManufacturerResponse? _selectedManufacturer;
        public ManufacturerResponse? SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                if (_selectedManufacturer != value)
                {
                    _selectedManufacturer = value;
                    OnPropertyChanged();
                    if (_selectedManufacturer != null)
                    {
                        _manufacturerSearchText = _selectedManufacturer.ManufacturerName;
                        OnPropertyChanged(nameof(ManufacturerSearchText));
                    }
                }
            }
        }

        // Змінюємо на string для вводу з Behavior
        private string _weight = "0";
        public string Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _price = "0";
        public string Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged();
                }
            }
        }

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

            FilteredCategories = new ObservableCollection<CategoryResponse>(Categories);
            FilteredManufacturers = new ObservableCollection<ManufacturerResponse>(Manufacturers);

            if (existingProduct != null)
            {
                ProductID = existingProduct.ProductID;
                ProductName = existingProduct.ProductName ?? "";

                SelectedCategory = categories.FirstOrDefault(c => c.CategoryID == existingProduct.CategoryID);
                SelectedManufacturer = manufacturers.FirstOrDefault(m => m.ManufacturerID == existingProduct.ManufacturerID);

                Weight = (existingProduct.Weight ?? 0).ToString("G");
                Price = (existingProduct.Price ?? 0).ToString("G");
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

            if (!double.TryParse(Weight.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double weightValue) || weightValue <= 0)
            {
                MessageBox.Show("Weight must be a valid number greater than zero.");
                return;
            }

            if (!double.TryParse(Price.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double priceValue) || priceValue <= 0)
            {
                MessageBox.Show("Price must be a valid number greater than zero.");
                return;
            }

            var result = new ProductResponse
            {
                ProductID = ProductID == Guid.Empty ? Guid.NewGuid() : ProductID,
                ProductName = ProductName,
                CategoryID = SelectedCategory.CategoryID,
                ManufacturerID = SelectedManufacturer.ManufacturerID,
                Weight = weightValue,
                Price = priceValue,
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

        // --- Filtering ---  
        public ObservableCollection<CategoryResponse> FilteredCategories { get; set; }
        private void FilterCategories()
        {
            var filtered = Categories
                .Where(c => string.IsNullOrWhiteSpace(CategorySearchText) ||
                            c.CategoryName.Contains(CategorySearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredCategories.Clear();
            foreach (var c in filtered) FilteredCategories.Add(c);
        }

       

        public ObservableCollection<ManufacturerResponse> FilteredManufacturers { get; set; }

        private string _manufacturerSearchText = "";
        public string ManufacturerSearchText
        {
            get => _manufacturerSearchText;
            set
            {
                if (_manufacturerSearchText != value)
                {
                    _manufacturerSearchText = value;
                    OnPropertyChanged();
                    FilterManufacturers();
                }
            }
        }
        private void FilterManufacturers()
        {
            var filtered = Manufacturers
                .Where(m => string.IsNullOrWhiteSpace(ManufacturerSearchText) ||
                            m.ManufacturerName.Contains(ManufacturerSearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredManufacturers.Clear();
            foreach (var m in filtered) 
                FilteredManufacturers.Add(m);
        }
    }
}
