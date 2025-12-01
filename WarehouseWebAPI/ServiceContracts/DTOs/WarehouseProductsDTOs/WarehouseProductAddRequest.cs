using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehouseProductsDTOs
{
    public class WarehouseProductAddRequest
    {
        [Required(ErrorMessage = "Warehouse ID can't be blank")]
        public Guid? WarehouseID { get; set; }
        [Required(ErrorMessage = "Product ID can't be blank")]
        public Guid? ProductID { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Value must be greater than 0.")]
        [Required(ErrorMessage = "Number of products can't blank.")]
        public int Count { get; set; }
        public double WarehouseAreaUsed { get; set; }
        public WarehouseProduct ToWarehouseProduct()
        {
            return new WarehouseProduct()
            {
                WarehouseID = WarehouseID,
                ProductID = ProductID,
                UpdatedAt = DateTime.Now,
                Count = Count,
                WarehouseAreaUsed = WarehouseAreaUsed
            };
        }
    }
}
