using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Manufacturer
    {
        [Key]
        public Guid ManufacturerID { get; set; }
        [StringLength(60)]
        public string? ManufacturerName { get; set; }
        [Range(0, int.MaxValue)]
        public int Deliveries { get; set; }
    }
}
