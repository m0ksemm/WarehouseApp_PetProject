using ServiceContracts.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CategoriesServiceContracts
{
    public interface ICategoriesGetterService
    {
        Task<List<CategoryResponse>> GetAllCategories();
        Task<CategoryResponse?> GetCategoryById(Guid? guid);
    }
}
