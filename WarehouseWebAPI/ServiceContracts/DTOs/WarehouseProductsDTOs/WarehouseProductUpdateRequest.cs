using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehouseProductsDTOs
{
    public class WarehouseProductUpdateRequest
    {
        public Guid WarehouseProductID { get; set; }
        [Required(ErrorMessage = "Warehouse ID can't be blank")]
        public Guid WarehouseID { get; set; }
        [Required(ErrorMessage = "Product ID can't be blank")]
        public Guid ProductID { get; set; }
        public DateTime UpdatedAt { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0")]
        public int Count { get; set; }

        public WarehouseProduct ToWarehouseProduct()
        {
            return new WarehouseProduct()
            {
                WarehouseProductID = WarehouseProductID,
                WarehouseID = WarehouseID,
                ProductID = ProductID,
                UpdatedAt = UpdatedAt,
                Count = Count,
            };
        }
    }
}
