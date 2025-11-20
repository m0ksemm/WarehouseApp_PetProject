using ServiceContracts.DTOs.WarehouseProductsDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ServiceContracts
{
    internal interface IWarehouseProductsService
    {
        Task<bool> AddWarehouseProduct(WarehouseProductAddRequest warehouseProductAddRequest);
        Task<List<WarehouseProductResponse>> GetAllCategories();
        //Task<WarehouseProductResponse?> GetWarehouseProductByWarehouseId(Guid guid);
        Task<bool> UpdateWarehouseProduct(WarehouseProductUpdateRequest category);
        Task<bool> DeleteWarehouseProduct(Guid guid);
    }
}
