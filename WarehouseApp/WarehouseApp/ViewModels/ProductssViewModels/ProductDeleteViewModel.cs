using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarehouseApp.ViewModels.ProductssViewModels
{
    public class ProductDeleteViewModel
    {
        private readonly Window _window;
        private readonly Action<bool> _onResult;

        public string Message { get; }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public ProductDeleteViewModel(Window window, string productName, Action<bool> onResult)
        {
            _window = window;
            _onResult = onResult;
            Message = $"Are you sure you want to delete product \"{productName}\"?";

            ConfirmCommand = new RelayCommand(_ =>
            {
                _onResult(true);
                _window.Close();
            });

            CancelCommand = new RelayCommand(_ =>
            {
                _onResult(false);
                _window.Close();
            });
        }
    }
}
