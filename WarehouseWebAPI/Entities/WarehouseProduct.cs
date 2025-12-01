using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class WarehouseProduct
    {
        [Key]
        public Guid WarehouseProductID { get; set; }
        public Guid? WarehouseID { get; set; }
        public Guid? ProductID { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public int Count { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public double WarehouseAreaUsed { get; set; }
        [ForeignKey("WarehouseID")]
        public Warehouse? Warehouse { get; set; }
        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
    }
}
