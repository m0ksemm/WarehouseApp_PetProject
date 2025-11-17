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
        public double SquareArea { get; set; }
        public string? Address { get; set; }

        public int RowNumber { get; set; }
        public override string ToString()
        {
            return WarehouseName;
        }
    }
}
