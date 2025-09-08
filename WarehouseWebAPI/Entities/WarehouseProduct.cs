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
        [Key]
        public Guid WarehouseProductID { get; set; }
        public Guid WarehouseID { get; set; }
        public Guid ProductID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public int Count { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Product? Product { get; set; }


    }
}
