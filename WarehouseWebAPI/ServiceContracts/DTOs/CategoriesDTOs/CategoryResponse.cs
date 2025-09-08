using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.CategoryDTOs
{
    public class CategoryResponse
    {
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(CategoryResponse)) 
            {
                return false;
            }
            CategoryResponse category_to_compare = (CategoryResponse)obj;
            return CategoryID == category_to_compare.CategoryID && 
                CategoryName == category_to_compare.CategoryName;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public CategoryUpdateRequest ToCategoryUpdateRequest() 
        {
            return new CategoryUpdateRequest()
            {
                CategoryID = CategoryID,
                CategoryName = CategoryName
            };
        }
    }

    public static class CategoryExtensions
    {
        public static CategoryResponse ToCategoryResponse(this Category category) 
        {
            return new CategoryResponse 
            { 
                CategoryID = category.CategoryID, 
                CategoryName = category.CategoryName 
            };
        }
    }
}
