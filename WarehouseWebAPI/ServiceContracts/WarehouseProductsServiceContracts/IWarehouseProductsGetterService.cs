using Entities;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehouseProductsServiceContracts
{
    public interface IWarehouseProductsGetterService
    {
        Task<List<WarehouseProductResponse>> GetAllWarehouseProducts();
        Task<WarehouseProductResponse?> GetWarehouseProductById(Guid? guid);
        Task<WarehouseProductResponse?> GetWarehouseProductByWarehouseProductId(Guid? warehouseProductId);
        Task<List<WarehouseProductResponse>?> GetWarehouseProductsByWarehouseId(Guid? guid);
    }
}
