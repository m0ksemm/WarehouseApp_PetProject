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
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }

        public async Task<bool> DeleteCategory(Guid guid)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7053/Categories/DeleteCategory/{guid}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var categories = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("https://localhost:7053/Categories/GetAllCategories");
            if (categories != null)
            {
                return categories;
            }
            else throw new Exception("There are no categories.");
        }

        public Task<CategoryResponse?> GetCategoryById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryResponse?> GetCategoryByName(string categoryName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateCategory(CategoryUpdateRequest categoryUpdateRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7053/Categories/UpdateCategory/{categoryUpdateRequest.CategoryID}", categoryUpdateRequest);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = $"Error: \n{response.StatusCode}";

                throw new Exception(errorMessage);
            }
        }
    }
}
