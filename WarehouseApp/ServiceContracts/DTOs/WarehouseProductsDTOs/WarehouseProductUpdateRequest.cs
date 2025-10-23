using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehouseProductsDTOs
{
    public class WarehouseProductUpdateRequest
    {
        public Guid WarehouseProductID { get; set; }
        public Guid WarehouseID { get; set; }
        public Guid ProductID { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Count { get; set; }
    }
}
