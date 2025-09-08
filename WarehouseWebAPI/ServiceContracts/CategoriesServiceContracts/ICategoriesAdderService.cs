using ServiceContracts.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CategoriesServiceContracts
{
    public interface ICategoriesAdderService
    {
        Task<CategoryResponse> AddCategory(CategoryAddRequest? categoryAddRequest);
    }
}
