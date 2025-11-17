using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehousesDTOs
{
    public class WarehouseAddRequest
    {
        [Required(ErrorMessage = "Warehouse Name can't be blank.")]
        [StringLength(40, ErrorMessage = "Warehouse name is too long. It has to be less than 40 characters.")]
        public string? WarehouseName { get; set; }
        [Required(ErrorMessage = "Square Area can't be zero.")]
        public double SquareArea { get; set; }
        [Required(ErrorMessage = "Address can't be blank")]
        [StringLength(100, ErrorMessage = "Address is too long. It has to be less than 100 characters.")]
        public string? Address { get; set; }

        public Warehouse ToWarehouse()
        {
            return new Warehouse() { WarehouseName = WarehouseName, SquareArea = SquareArea, Address = Address };
        }
    }
}
