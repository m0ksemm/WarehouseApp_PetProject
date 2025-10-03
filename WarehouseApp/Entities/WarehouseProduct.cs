using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class WarehouseProduct
    {
        [Required]
        public Guid WarehouseProductID { get; set; }
        [Required]
        public Guid WarehouseID { get; set; }
        [Required]
        public Guid ProductID { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public Warehouse? Warehouse { get; set; }
        [Required]
        public Product? Product { get; set; }
    }
}
