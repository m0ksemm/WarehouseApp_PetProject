using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.CategoryDTOs
{
    public class CategoryAddRequest
    {
        public string? CategoryName { get; set; }
        public Category ToCategory() 
        {
            return new Category() { CategoryName = CategoryName };
        }
    }
}
