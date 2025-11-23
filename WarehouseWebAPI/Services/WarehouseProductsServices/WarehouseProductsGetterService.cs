using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ProductsDTO;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehouseProductsServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehouseProductsServices
{
    public class WarehouseProductsGetterService : IWarehouseProductsGetterService
    {
        private readonly IWarehouseProductRepository _warehouseProductRepository;
        public WarehouseProductsGetterService(IWarehouseProductRepository warehouseProductRepository)
        {
            _warehouseProductRepository = warehouseProductRepository;
        }
        public async Task<List<WarehouseProductResponse>> GetAllWarehouseProducts()
        {
            List<WarehouseProduct> warehouseProducts = await _warehouseProductRepository.GetAllWarehouseProducts();
            return warehouseProducts.Select(warehouseProduct => warehouseProduct.ToWarehouseProductResponse()).ToList();
        }

        public async Task<WarehouseProductResponse?> GetWarehouseProductById(Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
            {
                return null;
            }
            WarehouseProduct? warehouseProduct_response_from_list = await _warehouseProductRepository.GetWarehouseProductByWarehouseProductId(guid.Value);
            if (warehouseProduct_response_from_list == null)
            {
                return null;
            }
            return warehouseProduct_response_from_list.ToWarehouseProductResponse();
        }

        public async Task<WarehouseProductResponse?> GetWarehouseProductByWarehouseProductId(Guid? warehouseProductId)
        {
            if (warehouseProductId == null)
                return null;

            WarehouseProduct? product = await _warehouseProductRepository.GetWarehouseProductByWarehouseProductId(warehouseProductId.Value);

            if (product == null)
                return null;

            return product.ToWarehouseProductResponse();
        }

        public async Task<List<WarehouseProductResponse>?> GetWarehouseProductsByWarehouseId(Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
            {
                return null;
            }
            List<WarehouseProduct>? warehouseProducts = await _warehouseProductRepository.GetWarehouseProductsByWarehouseId(guid.Value);
            return warehouseProducts.Select(warehouseProduct => warehouseProduct.ToWarehouseProductResponse()).ToList();
        }
    }
}
