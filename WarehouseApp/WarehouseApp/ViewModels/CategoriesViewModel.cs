using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.ServiceContracts;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WarehouseApp.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public string Title => "📦 Categories page";


        private readonly HttpClient _httpClient;
        private readonly ICategoriesService _categoriesService;

        private const string BaseUrl = "https://localhost:7096/api/categories"; // свій API endpoint

        private ObservableCollection<CategoryResponse> _categories = new();
        public ObservableCollection<CategoryResponse> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        private ObservableCollection<CategoryResponse> _allCategories = new();

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        private CategoryResponse _selectedCategory;
        public CategoryResponse SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public CategoriesViewModel()
        {
            _httpClient = new HttpClient();
            _categoriesService = new CategoriesService();

            AddCommand = new RelayCommand(async _ => await AddCategory());
            UpdateCommand = new RelayCommand(async _ => await UpdateCategory(), _ => SelectedCategory != null);
            DeleteCommand = new RelayCommand(async _ => await DeleteCategory(), _ => SelectedCategory != null);
            RefreshCommand = new RelayCommand(async _ => await LoadCategories());

            Task.Run(LoadCategories);
        }

        private async Task LoadCategories()
        {
            try
            {

                List<CategoryResponse> categories = await _categoriesService.GetAllCategories();

                if (categories != null)
                {
                    _allCategories = new ObservableCollection<CategoryResponse>(categories);
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                // TODO: додати логування або повідомлення користувачу
                Console.WriteLine($"Помилка при завантаженні категорій: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Categories = new ObservableCollection<CategoryResponse>(_allCategories);
            }
            else
            {
                Categories = new ObservableCollection<CategoryResponse>(
                    _allCategories.Where(c => c.CategoryName != null &&
                                              c.CategoryName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }

            // Пронумеруємо рядки для відображення (як ID)
            int index = 1;
            foreach (var c in Categories)
            {
                // Можеш у DataGrid просто показувати індекс через DataGridTemplateColumn
                // або додати сюди тимчасову властивість Number, якщо хочеш напряму
            }
        }

        private async Task AddCategory()
        {
            // TODO: викликати діалогове вікно для введення назви нової категорії
            var newCategory = new CategoryAddRequest { CategoryName = "New category" };

            var response = await _httpClient.PostAsJsonAsync(BaseUrl, newCategory);
            if (response.IsSuccessStatusCode)
                await LoadCategories();
        }

        private async Task UpdateCategory()
        {
            if (SelectedCategory == null) return;

            // TODO: відкрити вікно редагування (наприклад, CategoryEditDialog)
            var updated = new CategoryUpdateRequest
            {
                CategoryID = SelectedCategory.CategoryID,
                CategoryName = SelectedCategory.CategoryName + " (edited)"
            };

            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{updated.CategoryID}", updated);
            if (response.IsSuccessStatusCode)
                await LoadCategories();
        }

        private async Task DeleteCategory()
        {
            if (SelectedCategory == null) return;

            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{SelectedCategory.CategoryID}");
            if (response.IsSuccessStatusCode)
                await LoadCategories();
        }
    }
}
