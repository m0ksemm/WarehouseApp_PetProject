using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Warehouse
    {
        [Key]
        public Guid WarehouseID { get; set; }
        public string? Name { get; set; }
        [Range(typeof(decimal), "100", "1000000")]
        public double SquareArea { get; set; }
        public string? Address { get; set; }
    }
}
