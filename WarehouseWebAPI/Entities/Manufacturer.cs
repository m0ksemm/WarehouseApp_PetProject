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
        [Key]
        public Guid ManufacturerID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int Deliveries { get; set; }
    }
}
