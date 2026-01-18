using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs.WarehouseProductsDTOs
{
    public class WarehouseProductResponse
    {
        public Guid WarehouseProductID { get; set; }
        public Guid? WarehouseID { get; set; }
        public Guid? ProductID { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Count { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Product? Product { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(WarehouseProductResponse))
            {
                return false;
            }
            WarehouseProductResponse warehouseProductResponse = (WarehouseProductResponse)obj;
            return WarehouseProductID == warehouseProductResponse.WarehouseProductID &&
                WarehouseID == warehouseProductResponse.WarehouseID &&
                ProductID == warehouseProductResponse.ProductID &&
                UpdatedAt == warehouseProductResponse.UpdatedAt &&
                Count == warehouseProductResponse.Count;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class WarehouseProductExtensions
    {
        public static WarehouseProductResponse ToWarehouseProductResponse(this WarehouseProduct warehouseProduct)
        {
            return new WarehouseProductResponse()
            {
                WarehouseProductID = warehouseProduct.WarehouseProductID,
                WarehouseID = warehouseProduct.WarehouseID,
                ProductID = warehouseProduct.ProductID,
                UpdatedAt = warehouseProduct.UpdatedAt,
                Count = warehouseProduct.Count,
                Warehouse = new Warehouse 
                {
                    WarehouseID = warehouseProduct.Warehouse.WarehouseID,
                    WarehouseName = warehouseProduct.Warehouse.WarehouseName,
                    SquareArea = warehouseProduct.Warehouse.SquareArea,
                    Address = warehouseProduct.Warehouse.Address
                },
                Product = new Product
                {
                    ProductID = warehouseProduct.Product.ProductID,
                    ProductName = warehouseProduct.Product.ProductName,
                    CategoryID = warehouseProduct.Product.CategoryID,
                    ManufacturerID = warehouseProduct.Product.ManufacturerID,
                    Weight = warehouseProduct.Product.Weight,
                    Price = warehouseProduct.Product.Price,
                    BarCode = warehouseProduct.Product.BarCode,
                    Category = new Category
                    {
                        CategoryID = warehouseProduct.Product.Category.CategoryID,
                        CategoryName = warehouseProduct.Product.Category.CategoryName
                    },
                    Manufacturer = new Manufacturer
                    {
                        ManufacturerID = warehouseProduct.Product.Manufacturer.ManufacturerID,
                        ManufacturerName = warehouseProduct.Product.Manufacturer.ManufacturerName
                    }
                }
            };
        }
    }
}
