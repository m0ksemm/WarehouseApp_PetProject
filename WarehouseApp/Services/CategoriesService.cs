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

        public Task<bool> AddCategory(CategoryAddRequest category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            try
            {
                var categories = await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("https://localhost:7053/Categories/GetAllCategories");
                if (categories != null)
                {
                    return categories;
                }
                else return null;
            }
            catch (Exception ex)
            {
                // TODO: додати логування або повідомлення користувачу
                throw new Exception($"Помилка при завантаженні категорій: {ex.Message}");
            }
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
