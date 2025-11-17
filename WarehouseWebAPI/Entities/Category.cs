using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        [StringLength(40)]
        public string? CategoryName { get; set; }
    }
}
