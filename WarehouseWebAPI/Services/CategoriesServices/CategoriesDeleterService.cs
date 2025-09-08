using Entities;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CategoriesServices
{
    public class CategoriesDeleterService : ICategoriesDeleterService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesDeleterService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> DeleteCategory(Guid? categoryID)
        {
            if (categoryID == null || categoryID == Guid.Empty) 
            {
                throw new ArgumentNullException(nameof(categoryID));
            }
            Category? category = await _categoryRepository.GetCategoryById(categoryID.Value);
            if (category == null) 
            {
                return false;
            }
            await _categoryRepository.DeleteCategory(categoryID.Value);
            return true;
        }
    }
}
