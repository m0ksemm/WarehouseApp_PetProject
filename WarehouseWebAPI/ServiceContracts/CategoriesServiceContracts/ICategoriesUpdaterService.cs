using ServiceContracts.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.CategoriesServiceContracts
{
    public interface ICategoriesUpdaterService
    {
        Task<CategoryResponse> UpdateCategory(CategoryUpdateRequest? categoryUpdateRequest);
    }
}
