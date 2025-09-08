using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoriesRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoriesRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(Guid guid)
        {
            _dbContext.Categories.RemoveRange(_dbContext.Categories.Where(temp => temp.CategoryID == guid));
            int rowsDeleted = await _dbContext.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(Guid guid)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(temp => temp.CategoryID == guid);
        }

        public async Task<Category?> GetCategoryByName(string categoryName)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(temp => temp.CategoryName == categoryName);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            Category? matchingCategory = await _dbContext.Categories
                .FirstOrDefaultAsync(temp => temp.CategoryID == category.CategoryID);
            if (matchingCategory == null) 
            {
                return category;
            }
            matchingCategory.CategoryName = category.CategoryName;
            int countUpdated = await _dbContext.SaveChangesAsync();
            return matchingCategory;
        }
    }
}
