using Entities;
using ServiceContracts.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ManufacturersDTO
{
    public class ManufacturerResponse
    {
        public Guid ManufacturerID { get; set; }
        public string? ManufacturerName { get; set; }
        public int Deliveries { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(ManufacturerResponse))
            {
                return false;
            }
            ManufacturerResponse manufacturer_to_compare = (ManufacturerResponse)obj;
            return ManufacturerID == manufacturer_to_compare.ManufacturerID &&
                ManufacturerName == manufacturer_to_compare.ManufacturerName &&
                Deliveries == manufacturer_to_compare.Deliveries;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class ManufacturerExtensions
    {
        public static ManufacturerResponse ToManufacturerResponse(this Manufacturer manufacturer)
        {
            return new ManufacturerResponse
            {
                ManufacturerID = manufacturer.ManufacturerID,
                ManufacturerName = manufacturer.ManufacturerName,
                Deliveries = manufacturer.Deliveries
            };
        }
    }
}
