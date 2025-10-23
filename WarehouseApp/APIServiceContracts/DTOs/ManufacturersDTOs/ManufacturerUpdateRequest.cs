using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServiceContracts.DTOs.ManufacturersDTOs
{
    public class ManufacturerUpdateRequest
    {
        public Guid ManufacturerID { get; set; }
        public string? ManufacturerName { get; set; }
        public int Deliveries { get; set; }
    }
}
