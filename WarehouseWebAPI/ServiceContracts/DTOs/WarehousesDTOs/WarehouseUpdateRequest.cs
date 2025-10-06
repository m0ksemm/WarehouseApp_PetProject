using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehousesDTOs
{
    public class WarehouseUpdateRequest
    {
        [Required(ErrorMessage = "Warehouse ID can't be blank")]
        public Guid WarehouseID { get; set; }
        [Required(ErrorMessage = "Warehouse Name can't be blank")]
        [StringLength(40, ErrorMessage = "Warehouse Name is too long. It has to be less than 40 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Square Area can't be zero.")]
        [Range(typeof(decimal), "100", "1000000", ErrorMessage = "Square Area should be between 100 and 1000000 square meters.")]
        public double SquareArea { get; set; }
        [Required(ErrorMessage = "Address can't be blank")]
        [StringLength(100, ErrorMessage = "Address is too long. It has to be less than 100 characters.")]
        public string? Address { get; set; }

        public Warehouse ToWarehouse()
        {
            return new Warehouse
            {
                WarehouseID = WarehouseID,
                Name = Name,
                SquareArea = SquareArea,
                Address = Address
            };
        }
    }
}
