using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.CategoryDTOs
{
    public class CategoryAddRequest
    {
        [StringLength(50, ErrorMessage = "Category name is too long. It has to be less than 40 characters.")]
        public string? CategoryName { get; set; }
        public Category ToCategory() 
        {
            return new Category() { CategoryName = CategoryName };
        }
    }
}
