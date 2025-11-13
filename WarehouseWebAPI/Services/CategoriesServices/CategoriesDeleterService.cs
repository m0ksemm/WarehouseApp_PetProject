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
        private readonly IProductRepository _productRepository;

        public CategoriesDeleterService(ICategoryRepository categoryRepository, 
            IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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
            List<Product> products = await _productRepository.GetAllProducts();
            if (products.Any(product => product.CategoryID == categoryID))
            {
                throw new ArgumentException("This Category can not be deleted since there are products that belong to it.");
            }
            await _categoryRepository.DeleteCategory(categoryID.Value);
            return true;
        }
    }
}
