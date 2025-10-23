using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehouseProductsDTOs
{
    public class WarehouseProductResponse
    {
        public Guid WarehouseProductID { get; set; }
        public Guid? WarehouseID { get; set; }
        public Guid? ProductID { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Count { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Product? Product { get; set; }
    }
}
