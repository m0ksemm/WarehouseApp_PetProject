using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ProductsDTOs
{
    public class ProductUpdateRequest
    {
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? ManufacturerID { get; set; }
        public double? Weight { get; set; }
        public double? Price { get; set; }
        public string? BarCode { get; set; }
        public Category? Category { get; set; }
        public Manufacturer? Manufacturer { get; set; }
    }
}
