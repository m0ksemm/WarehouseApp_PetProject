using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }
        [StringLength(60)]
        public string? ProductName { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? ManufacturerID { get; set; }
        [Range(0.0001, 5000, ErrorMessage = "Weight must be greater than 0")]
        public double? Weight { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")] 
        public double? Price { get; set; }
        [StringLength(30)]
        public string? BarCode { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }
        [ForeignKey("ManufacturerID")]
        public Manufacturer? Manufacturer { get; set; }
    }
}
