using Entities;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.DTOs.CategoryDTOs;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoriesServices
{
    public class CategoriesUpdaterService : ICategoriesUpdaterService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesUpdaterService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> UpdateCategory(CategoryUpdateRequest? categoryUpdateRequest)
        {
            if (categoryUpdateRequest == null) 
            {
                throw new ArgumentNullException(nameof(categoryUpdateRequest));
            }
            ValidationHelper.ModelValidation(categoryUpdateRequest);

            Category? matchingCategory = await _categoryRepository
                .GetCategoryById(categoryUpdateRequest.CategoryID);
            if (matchingCategory == null) 
            {
                throw new ArgumentException("Given Category ID does not exist.");
            }
            matchingCategory.CategoryName = categoryUpdateRequest.CategoryName;
            await _categoryRepository.UpdateCategory(matchingCategory);
            return matchingCategory.ToCategoryResponse();
        }
    }
}
