using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        [Required]
        public Guid ProductID { get; set; }
        [Required]
        public string? ProductName { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? ManufacturerID { get; set; }
        [Required]
        public double? Weight { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public string? BarCode { get; set; }
        [Required]
        public Category? Category { get; set; }
        [Required]
        public Manufacturer? Manufacturer { get; set; }
    }
}
