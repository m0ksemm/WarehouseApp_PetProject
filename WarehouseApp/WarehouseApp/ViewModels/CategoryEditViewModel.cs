using ServiceContracts.DTOs.CategoriesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WarehouseApp.ViewModels
{
    public class CategoryEditViewModel : BaseViewModel
    {
        private readonly Action<CategoryResponse?> _onSave;
        private readonly Window _window;

        public string CategoryName { get; set; } = string.Empty;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public CategoryEditViewModel(Window window, Action<CategoryResponse?> onSave, CategoryResponse? existingCategory = null)
        {
            _window = window;
            _onSave = onSave;

            if (existingCategory != null)
            {
                CategoryName = existingCategory.CategoryName ?? "";
                CategoryID = existingCategory.CategoryID;
            }

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public Guid CategoryID { get; set; }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                MessageBox.Show("Category name cannot be empty.");
                return;
            }

            var result = new CategoryResponse
            {
                CategoryID = CategoryID == Guid.Empty ? Guid.NewGuid() : CategoryID,
                CategoryName = CategoryName
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
