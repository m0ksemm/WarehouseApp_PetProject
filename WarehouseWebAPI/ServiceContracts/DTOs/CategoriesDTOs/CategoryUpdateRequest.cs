using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.CategoryDTOs
{
    public class CategoryUpdateRequest
    {
        [Required(ErrorMessage = "Category ID can't be blank")]
        public Guid CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name can't be blank")]
        [StringLength(50, ErrorMessage = "Category name is too long. It has to be less than 50 characters.")]
        public string? CategoryName { get; set; }

        public Category ToCategory() 
        {
            return new Category 
            { 
                CategoryID = CategoryID, 
                CategoryName = CategoryName 
            };
        }
    }
}
