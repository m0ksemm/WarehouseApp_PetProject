using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehousesServiceContracts
{
    public interface IWarehousesAdderService
    {
        Task<WarehouseResponse> AddWarehouse(WarehouseAddRequest? warehouseAddRequest);
    }
}
