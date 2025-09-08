using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(Guid guid);
        Task<Category?> GetCategoryByName(string categoryName);
        Task<Category> UpdateCategory(Category category); 
        Task<Category> DeleteCategory(Guid guid);
    }
}
