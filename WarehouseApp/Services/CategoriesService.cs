using ServiceContracts.DTOs.CategoriesDTOs;
using ServiceContracts.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient _httpClient;

        public CategoriesService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> AddCategory(CategoryAddRequest categoryAddRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7053/Categories/CreateCategory", categoryAddRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                // Зчитуємо текст помилки, який повертає HandleExceptionFilter
                string errorMessage = await response.Content.ReadAsStringAsync();

                // Якщо сервер не повернув текст — даємо дефолт
                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Server returned error code: {response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }

        public Task<bool> DeleteCategory(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("https://localhost:7053/Categories/GetAllCategories");
            if (categories != null)
            {
                return categories;
            }
            else return null;
        }

        public Task<CategoryResponse?> GetCategoryById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse?> GetCategoryByName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(CategoryUpdateRequest category)
        {
            throw new NotImplementedException();
        }
    }
}
