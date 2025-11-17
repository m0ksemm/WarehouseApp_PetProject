using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehousesDTOs
{
    public class WarehouseResponse
    {
        public Guid WarehouseID { get; set; }
        public string? WarehouseName { get; set; }
        [Range(typeof(decimal), "100", "1000000")]
        public double SquareArea { get; set; }
        public string? Address { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(WarehouseResponse))
            {
                return false;
            }
            WarehouseResponse warehouse_to_compare = (WarehouseResponse)obj;
            return WarehouseID == warehouse_to_compare.WarehouseID &&
               WarehouseName == warehouse_to_compare.WarehouseName &&
               SquareArea == warehouse_to_compare.SquareArea &&
               Address == warehouse_to_compare.Address;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class WarehouseExtensions
    {
        public static WarehouseResponse ToWarehouseResponse(this Warehouse warehouse)
        {
            return new WarehouseResponse
            {
                WarehouseID = warehouse.WarehouseID,
                WarehouseName = warehouse.WarehouseName,
                SquareArea = warehouse.SquareArea,
                Address = warehouse.Address
            };
        }
    }
}
