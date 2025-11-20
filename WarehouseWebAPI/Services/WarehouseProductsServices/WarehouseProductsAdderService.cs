using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehouseProductsServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehouseProductsServices
{
    public class WarehouseProductsAdderService : IWarehouseProductsAdderService
    {
        private readonly IWarehouseProductRepository _warehouseProductRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IProductRepository _productRepository;


        public WarehouseProductsAdderService(
            IWarehouseProductRepository warehouseProductRepository, 
            IWarehouseRepository warehouseRepository, 
            IProductRepository productRepository)
        {
            _warehouseProductRepository = warehouseProductRepository;
            _warehouseRepository = warehouseRepository;
            _productRepository = productRepository;
        }
        public async Task<WarehouseProductResponse> AddWarehouseProduct(WarehouseProductAddRequest? warehouseProductAddRequest)
        {
            if (warehouseProductAddRequest == null)
            {
                throw new ArgumentNullException(nameof(warehouseProductAddRequest));
            }
            if (warehouseProductAddRequest.WarehouseID == null)
            {
                throw new ArgumentException(nameof(warehouseProductAddRequest.WarehouseID));
            }
            if (warehouseProductAddRequest.ProductID == null)
            {
                throw new ArgumentException(nameof(warehouseProductAddRequest.ProductID));
            }

            Product? product = await _productRepository.GetProductById(warehouseProductAddRequest.ProductID.Value);
            if (product == null)
            {
                throw new ArgumentNullException("There is no such Product.");
            }

            Warehouse? warehouse = await _warehouseRepository.GetWarehouseById(warehouseProductAddRequest.WarehouseID.Value);
            if (warehouse == null)
            {
                throw new ArgumentNullException("There is no such Warehouse.");
            }

            List<WarehouseProduct> warehouseProducts = await _warehouseProductRepository.GetAllWarehouseProducts();
            if (warehouseProducts.Any(warehouseProduct => 
                warehouseProduct.WarehouseID == warehouseProductAddRequest.WarehouseID &&
                warehouseProduct.ProductID == warehouseProductAddRequest.ProductID &&
                warehouseProduct.Count == warehouseProductAddRequest.Count))
            {
                throw new ArgumentException("Such Product already exists in this Warehouse.");
            }

            ValidationHelper.ModelValidation(warehouseProductAddRequest);

            WarehouseProduct warehouseProduct = warehouseProductAddRequest.ToWarehouseProduct();
            warehouseProduct.WarehouseProductID = Guid.NewGuid();
            warehouseProduct.UpdatedAt = DateTime.Now;

            await _warehouseProductRepository.AddWarehouseProduct(warehouseProduct);
            return warehouseProduct.ToWarehouseProductResponse();
        }
    }
}
