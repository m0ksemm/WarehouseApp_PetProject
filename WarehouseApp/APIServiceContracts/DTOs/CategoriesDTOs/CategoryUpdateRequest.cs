using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServiceContracts.DTOs.CategoriesDTOs
{
    public class CategoryUpdateRequest
    {
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }
    }
}
