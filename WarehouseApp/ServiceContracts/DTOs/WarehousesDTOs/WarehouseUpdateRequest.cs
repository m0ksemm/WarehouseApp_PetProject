using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehousesDTOs
{
    public class WarehouseUpdateRequest
    {
        public Guid WarehouseID { get; set; }
        public string? Name { get; set; }
        public double SquareArea { get; set; }
        public string? Address { get; set; }
    }
}
