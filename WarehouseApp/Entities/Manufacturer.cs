using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Manufacturer
    {
        [Required]
        public Guid ManufacturerID { get; set; }
        [Required]
        public string? ManufacturerName { get; set; }
        [Required]
        public int Deliveries { get; set; }
    }
}
