using ServiceContracts.DTOs.ManufacturersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarehouseApp.ViewModels.ManufacturersViewModels
{
    public class ManufacturerAddEditViewModel : BaseViewModel
    {
        private readonly Action<ManufacturerResponse?> _onSave;
        private readonly Window _window;

        public string ManufacturerName { get; set; } = string.Empty;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public ManufacturerAddEditViewModel(Window window, Action<ManufacturerResponse?> onSave, ManufacturerResponse? existingManufacturer = null)
        {
            _window = window;
            _onSave = onSave;

            if (existingManufacturer != null)
            {
                ManufacturerName = existingManufacturer.ManufacturerName ?? "";
                ManufacturerID = existingManufacturer.ManufacturerID;
            }

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public Guid ManufacturerID { get; set; }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(ManufacturerName))
            {
                MessageBox.Show("Manufacturer name cannot be empty.");
                return;
            }

            var result = new ManufacturerResponse
            {
                ManufacturerID = ManufacturerID == Guid.Empty ? Guid.NewGuid() : ManufacturerID,
                ManufacturerName = ManufacturerName
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
