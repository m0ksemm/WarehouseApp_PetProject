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
    public class CategoriesAdderService : ICategoriesAdderService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesAdderService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> AddCategory(CategoryAddRequest? categoryAddRequest)
        {
            if (categoryAddRequest == null) 
            {
                throw new ArgumentNullException(nameof(categoryAddRequest));
            }
            if (categoryAddRequest.CategoryName == null) 
            {
                throw new ArgumentException(nameof(categoryAddRequest.CategoryName));
            }
            if (await _categoryRepository.GetCategoryByName(categoryAddRequest.CategoryName) != null)
            {
                throw new ArgumentException("Category with a given name already exists.");
            }
            Category category = categoryAddRequest.ToCategory();
            category.CategoryID = Guid.NewGuid();
            
            await _categoryRepository.AddCategory(category);
            return category.ToCategoryResponse();
        }
    }
}
