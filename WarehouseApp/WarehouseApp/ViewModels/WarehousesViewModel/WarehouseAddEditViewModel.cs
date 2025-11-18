using GalaSoft.MvvmLight.Command;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace WarehouseApp.ViewModels.WarehousesViewModel
{
    public class WarehouseAddEditViewModel : BaseViewModel
    {
        private readonly Func<WarehouseResponse?, Task> _onSave;
        private readonly Window _window;

        public Guid WarehouseID { get; set; } = Guid.Empty;
        public string WarehouseName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string SquareArea { get; set; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public WarehouseAddEditViewModel(Window window, Func<WarehouseResponse?, Task> onSave, WarehouseResponse? existing = null)
        {
            _window = window ?? throw new ArgumentNullException(nameof(window));
            _onSave = onSave ?? throw new ArgumentNullException(nameof(onSave));

            if (existing != null)
            {
                WarehouseID = existing.WarehouseID;
                WarehouseName = existing.WarehouseName ?? string.Empty;
                Address = existing.Address ?? string.Empty;
                SquareArea = existing.SquareArea.ToString();
            }

            SaveCommand = new RelayCommand(async _ => await Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(WarehouseName))
            {
                MessageBox.Show("Warehouse name cannot be empty.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                MessageBox.Show("Address cannot be empty.");
                return;
            }

            if (!double.TryParse(SquareArea.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double squareArea) || squareArea <= 0)
            {
                MessageBox.Show("Weight must be a valid number greater than zero.");
                return;
            }


            var resp = new WarehouseResponse
            {
                WarehouseID = WarehouseID == Guid.Empty ? Guid.NewGuid() : WarehouseID,
                WarehouseName = WarehouseName,
                Address = Address,
                SquareArea = squareArea
            };

            await _onSave(resp);
            _window.Close();
        }

        private void Cancel()
        {
            _onSave?.Invoke(null);
            _window.Close();
        }
    }
}
