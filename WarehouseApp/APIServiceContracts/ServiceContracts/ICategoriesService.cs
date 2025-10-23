using Entities;
using ServiceContracts.DTOs.CategoriesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ServiceContracts
{
    public interface ICategoriesService
    {
        Task<bool> AddCategory(CategoryAddRequest category);
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse?> GetCategoryById(Guid guid);
        Task<CategoryResponse?> GetCategoryByName(string categoryName);
        Task<bool> UpdateCategory(CategoryUpdateRequest category);
        Task<bool> DeleteCategory(Guid guid);
    }
}
