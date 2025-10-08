using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehouseProductsServiceContracts
{
    public interface IWarehouseProductsUpdaterService
    {
        Task<bool> UpdateWarehouseProduct(WarehouseProductUpdateRequest? warehouseProductUpdateRequest);
    }
}
