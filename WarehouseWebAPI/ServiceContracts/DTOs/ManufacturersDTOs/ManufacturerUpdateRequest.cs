using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ManufacturersDTO
{
    public class ManufacturerUpdateRequest
    {
        [Required(ErrorMessage = "Manufacturer ID can't be blank")]
        public Guid ManufacturerID { get; set; }

        [Required(ErrorMessage = "Manufacturer Name can't be blank")]
        [StringLength(60, ErrorMessage = "Manufacturer Name is too long. It has to be less than 60 characters.")]
        public string? ManufacturerName { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Deliveries count can not be less than zero.")]
        public int Deliveries { get; set; }

        public Manufacturer ToManufacturer()
        {
            return new Manufacturer
            {
                ManufacturerID = ManufacturerID,
                ManufacturerName = ManufacturerName,
                Deliveries = Deliveries
            };
        }
    }
}
