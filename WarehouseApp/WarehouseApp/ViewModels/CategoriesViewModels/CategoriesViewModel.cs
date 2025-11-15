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
using System.Windows;
using System.Windows.Input;
using WarehouseApp.Views;
using WarehouseApp.Views.CategoriesViews;

namespace WarehouseApp.ViewModels.CategoriesViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public string Title => "📦 Categories page";


        private readonly HttpClient _httpClient;
        private readonly ICategoriesService _categoriesService;

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
                    // Присвоюємо номери рядків
                    for (int i = 0; i < categories.Count; i++)
                        categories[i].RowNumber = i + 1;

                    _allCategories = new ObservableCollection<CategoryResponse>(categories);
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with downloading categories: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            IEnumerable<CategoryResponse> filtered;

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = _allCategories;
            }
            else
            {
                filtered = _allCategories.Where(c =>
                    c.CategoryName != null &&
                    c.CategoryName.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            var resultList = filtered.Select((c, index) =>
            {
                c.RowNumber = index + 1;
                return c;
            }).ToList();

            Categories = new ObservableCollection<CategoryResponse>(resultList);
        }


        private async Task DeleteCategory()
        {
            var window = new CategoryDeleteView();
            var vm = new CategoryDeleteViewModel(window, SelectedCategory.CategoryName ?? "this category", async confirmed =>
            {
                if (confirmed)
                {
                    try
                    {
                        await _categoriesService.DeleteCategory(SelectedCategory.CategoryID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting category: {ex.Message}");
                    }
                    await LoadCategories();
                }
            });
            window.DataContext = vm;
            window.ShowDialog();
        }



        private async Task AddCategory()
        {
            var window = new Views.CategoriesViews.CategoryAddEditView();
            var vm =  new CategoryEditViewModel(window, async result => 
            {
                if (result != null)
                {
                    var addReq = new CategoryAddRequest { CategoryName = result.CategoryName };
                    try
                    {
                         await _categoriesService.AddCategory(addReq);
                    }
                    catch (Exception ex)
                    { 
                        MessageBox.Show($"Error adding category: {ex.Message}");
                    }
                        
                    await LoadCategories();
                }
            });
            window.DataContext = vm;
            window.ShowDialog();
        }

        private async Task UpdateCategory()
        {
            if (SelectedCategory == null) return;

            var window = new CategoryAddEditView();
            var vm = new CategoryEditViewModel(window, async result =>
            {
                if (result != null)
                {
                    var updateRequest = new CategoryUpdateRequest
                    {
                        CategoryID = SelectedCategory.CategoryID,
                        CategoryName = result.CategoryName
                    };

                    try
                    {
                        await _categoriesService.UpdateCategory(updateRequest);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding category: {ex.Message}");
                    }

                    await LoadCategories();
                }
            }, SelectedCategory);

            window.DataContext = vm;
            window.ShowDialog();
        }

    }


}
