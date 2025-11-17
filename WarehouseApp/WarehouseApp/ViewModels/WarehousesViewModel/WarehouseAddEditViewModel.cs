using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarehouseApp.ViewModels.WarehousesViewModel
{
    public class WarehouseAddEditViewModel : BaseViewModel
    {
        private readonly Action<string> _onSave;

        public string WarehouseName
        {
            get => _warehouseName;
            set => SetProperty(ref _warehouseName, value);
        }
        private string _warehouseName;

        public ICommand SaveCommand { get; }

        public WarehouseAddEditViewModel(Action<string> onSave, string initial = "")
        {
            _onSave = onSave;
            WarehouseName = initial;

            SaveCommand = new RelayCommand<Window>(Save);
        }

        private void Save(Window window)
        {
            _onSave(WarehouseName);
            window.Close();
        }
    }
}
