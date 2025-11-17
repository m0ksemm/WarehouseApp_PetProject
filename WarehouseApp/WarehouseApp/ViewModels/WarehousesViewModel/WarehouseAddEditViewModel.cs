using Entities;
using GalaSoft.MvvmLight.Command;
using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace WarehouseApp.ViewModels.WarehousesViewModel
{
    public class WarehouseAddEditViewModel : BaseViewModel
    {
        private readonly Action<WarehouseResponse?> _onSave;
        private readonly Window _window;

        public string WarehouseName
        {
            get => _warehouseName;
            set => SetProperty(ref _warehouseName, value);
        }
        private string _warehouseName;

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
        private string _address;

        public string SquareArea
        {
            get => _squareArea;
            set
            {
                if (_squareArea != value)
                {
                    _squareArea = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _squareArea = "0";


        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public WarehouseAddEditViewModel(Window window,
            Action<WarehouseResponse?> onSave)
        {
            _onSave = onSave;

            SaveCommand = new RelayCommand<Window>(Save);
        }

        private void Save(Window _window)
        {
            //if (string.IsNullOrWhiteSpace(ProductName))
            //{
            //    MessageBox.Show("Product name cannot be empty.");
            //    return;
            //}

            //if (SelectedCategory == null)
            //{
            //    MessageBox.Show("Please select a category.");
            //    return;
            //}

            //if (SelectedManufacturer == null)
            //{
            //    MessageBox.Show("Please select a manufacturer.");
            //    return;
            //}

            //if (!double.TryParse(Weight.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double weightValue) || weightValue <= 0)
            //{
            //    MessageBox.Show("Weight must be a valid number greater than zero.");
            //    return;
            //}

            //if (!double.TryParse(Price.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double priceValue) || priceValue <= 0)
            //{
            //    MessageBox.Show("Price must be a valid number greater than zero.");
            //    return;
            //}

            var result = new WarehouseResponse
            {
                //ProductID = ProductID == Guid.Empty ? Guid.NewGuid() : ProductID,
                //ProductName = ProductName,
                //CategoryID = SelectedCategory.CategoryID,
                //ManufacturerID = SelectedManufacturer.ManufacturerID,
                //Weight = weightValue,
                //Price = priceValue,
                //BarCode = BarCode
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
