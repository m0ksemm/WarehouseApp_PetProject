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
    public class ProductResponse
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

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(ProductResponse))
            {
                return false;
            }
            ProductResponse manufacturer_to_compare = (ProductResponse)obj;
            return ProductID == manufacturer_to_compare.ProductID &&
                ProductName == manufacturer_to_compare.ProductName &&
                CategoryID == manufacturer_to_compare.CategoryID &&
                ManufacturerID == manufacturer_to_compare.ManufacturerID &&
                Weight == manufacturer_to_compare.Weight &&
                Price == manufacturer_to_compare.Price &&
                BarCode == manufacturer_to_compare.BarCode;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class ProductExtensions
    {
        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                ManufacturerID = product.ManufacturerID,
                Weight = product.Weight,
                Price = product.Price,
                BarCode = product.BarCode
            };
        }
    }
}
