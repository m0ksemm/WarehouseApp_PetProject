using Entities;
using GalaSoft.MvvmLight.Command;
using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.DTOs.ManufacturersDTOs;
using ServiceContracts.DTOs.ProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Guid WarehouseID { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public WarehouseAddEditViewModel(Window window,
            Action<WarehouseResponse?> onSave, WarehouseResponse? existingWarehouse = null)
        {
            _window = window;
            _onSave = onSave;


            if (existingWarehouse != null)
            {
                WarehouseID = existingWarehouse.WarehouseID;
                WarehouseName = existingWarehouse.WarehouseName ?? "";
                SquareArea = (existingWarehouse.SquareArea).ToString("G");
                Address = existingWarehouse.Address ?? "";
            }

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(WarehouseName))
            {
                MessageBox.Show("Warehouse name cannot be empty.");
                return;
            }
            if (!double.TryParse(SquareArea.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double squareArea) || squareArea <= 0)
            {
                MessageBox.Show("Weight must be a valid number greater than zero.");
                return;
            }
            if (string.IsNullOrWhiteSpace(Address))
            {
                MessageBox.Show("Address name cannot be empty.");
                return;
            }

            var result = new WarehouseResponse
            {
                WarehouseID = WarehouseID == Guid.Empty ? Guid.NewGuid() : WarehouseID,
                WarehouseName = WarehouseName,
                SquareArea = squareArea,
                Address = Address
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
