using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehouseProductsServiceContracts
{
    public interface IWarehouseProductsAdderService
    {
        Task<WarehouseProductResponse> AddWarehouseProduct(WarehouseProductAddRequest? warehouseProductAddRequest);
    }
}
