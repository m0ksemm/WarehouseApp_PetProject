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
        private readonly IManufacturerRepository _manufacturerRepository;


        public WarehouseProductsAdderService(
            IWarehouseProductRepository warehouseProductRepository, 
            IWarehouseRepository warehouseRepository, 
            IProductRepository productRepository,
            IManufacturerRepository manufacturerRepository)
        {
            _warehouseProductRepository = warehouseProductRepository;
            _warehouseRepository = warehouseRepository;
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;
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
                warehouseProduct.ProductID == warehouseProductAddRequest.ProductID 
                ))
            {
                //just add
                throw new ArgumentException("Such Product already exists in this Warehouse.");
            }

            ValidationHelper.ModelValidation(warehouseProductAddRequest);

            WarehouseProduct warehouseProduct = warehouseProductAddRequest.ToWarehouseProduct();
            warehouseProduct.WarehouseProductID = Guid.NewGuid();
            warehouseProduct.UpdatedAt = DateTime.Now;



            if (product.Manufacturer != null)
            {
                await _manufacturerRepository.UpdateManufacturerDeliveries(product.Manufacturer.ManufacturerID, 1);
            }
            else 
            {
                throw new ArgumentNullException("Manufacturer that needs to be updated was not found.");
            }

                await _warehouseProductRepository.AddWarehouseProduct(warehouseProduct);
            return warehouseProduct.ToWarehouseProductResponse();
        }
    }
}
