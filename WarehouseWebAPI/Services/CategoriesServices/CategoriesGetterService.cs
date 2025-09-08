using Entities;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoriesServices
{
    public class CategoriesGetterService : ICategoriesGetterService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesGetterService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<Category> categories = await _categoryRepository.GetAllCategories();
            return categories.Select(category => category.ToCategoryResponse()).ToList();
        }

        public async Task<CategoryResponse?> GetCategoryById(Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
            {
                return null;
            }
            Category? category_response_from_list = await _categoryRepository.GetCategoryById(guid.Value);
            if (category_response_from_list == null) 
            { 
                return null; 
            }
            return category_response_from_list.ToCategoryResponse();
        }
    }
}
