using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.ProductsDTO
{
    public class ProductUpdateRequest
    {
        [Required(ErrorMessage = "Product ID can't be blank")]
        public Guid ProductID { get; set; }
        [StringLength(60, ErrorMessage = "Product name is too long. It has to be less than 60 characters.")]
        [Required(ErrorMessage = "Product Name can not be blank.")]
        public string? ProductName { get; set; }
        public Guid? CategoryID { get; set; }
        public Guid? ManufacturerID { get; set; }
        [Range(0.0001, 5000, ErrorMessage = "Weight must be greater than 0 and less than 5000")]
        [Required(ErrorMessage = "Weight can not be 0.")]
        public double? Weight { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Required(ErrorMessage = "Price can not be 0.")]
        public double? Price { get; set; }
        [StringLength(30, ErrorMessage = "Bar Code is too long. It has to be less than 30 characters.")]
        [Required(ErrorMessage = "Bar Code can not be blank.")]
        public string? BarCode { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }
        [ForeignKey("ManufacturerID")]
        public Manufacturer? Manufacturer { get; set; }

        public Product ToManufacturer()
        {
            return new Product
            {
                ProductID = ProductID,
                ProductName = ProductName,
                CategoryID = CategoryID,
                ManufacturerID = ManufacturerID,
                Weight = Weight,
                Price = Price,
                BarCode = BarCode
            };
        }
    }
}
