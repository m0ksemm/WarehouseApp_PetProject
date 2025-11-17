using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehousesDTOs
{
    public class WarehouseAddRequest
    {
        public string? WarehouseName { get; set; }
        public double SquareArea { get; set; }
        public string? Address { get; set; }
    }
}
