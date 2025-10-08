using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehouseProductsServiceContracts;
using ServiceContracts.WarehousesServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehouseProductsServices
{
    public class WarehouseProductsUpdaterService : IWarehouseProductsUpdaterService
    {
        private readonly IWarehouseProductRepository _warehouseProductRepository;
        public WarehouseProductsUpdaterService(IWarehouseProductRepository warehouseProductRepository)
        {
            _warehouseProductRepository = warehouseProductRepository;
        }
        public async Task<bool> UpdateWarehouseProduct(WarehouseProductUpdateRequest? warehouseProductUpdateRequest)
        {
            if (warehouseProductUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(warehouseProductUpdateRequest));
            }
            ValidationHelper.ModelValidation(warehouseProductUpdateRequest);

            WarehouseProduct? matchingWarehouseProduct = await _warehouseProductRepository
                .GetWarehouseProductById(warehouseProductUpdateRequest.WarehouseProductID);
            if (matchingWarehouseProduct == null)
            {
                throw new ArgumentException("Threr is no such product in warehouse.");
            }

            List<WarehouseProduct> warehouseProducts = await _warehouseProductRepository.GetAllWarehouseProducts();
            if (warehouseProducts.Any(warehouse => warehouse.WarehouseID == warehouseProductUpdateRequest.WarehouseID &&
                warehouse.ProductID == warehouseProductUpdateRequest.ProductID &&
                warehouse.Count == warehouseProductUpdateRequest.Count && 
                warehouse.UpdatedAt == warehouseProductUpdateRequest.UpdatedAt))
            {
                throw new ArgumentException("Such Product already already exists in this Warehouse.");
            }

            matchingWarehouseProduct.ProductID = warehouseProductUpdateRequest.ProductID;
            matchingWarehouseProduct.WarehouseID = warehouseProductUpdateRequest.WarehouseID;
            matchingWarehouseProduct.Count = warehouseProductUpdateRequest.Count;
            matchingWarehouseProduct.UpdatedAt = DateTime.Now;

            return await _warehouseProductRepository.UpdateWarehouseProduct(matchingWarehouseProduct);
        }
    }
}
