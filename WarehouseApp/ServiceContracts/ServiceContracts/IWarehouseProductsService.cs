using Entities;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ServiceContracts
{
    public interface IWarehouseProductsService
    {
        Task<bool> AddWarehouseProduct(WarehouseProductAddRequest warehouseProductAddRequest);
        Task<List<WarehouseProductResponse>> GetAllWarehouseProducts();
        Task<WarehouseProductResponse?> GetWarehouseProductByWarehouseProductId(Guid warehouseProductId);
        Task<List<WarehouseProductResponse>?> GetWarehouseProductsByWarehouseId(Guid guid);
        Task<bool> UpdateWarehouseProduct(WarehouseProductUpdateRequest warehouseProduct);
        Task<bool> DeleteWarehouseProduct(Guid warehouseProductID);
    }
}
