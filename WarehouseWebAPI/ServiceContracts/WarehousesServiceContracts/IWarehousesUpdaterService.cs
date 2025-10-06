using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehousesServiceContracts
{
    public interface IWarehousesUpdaterService
    {
        Task<WarehouseResponse> UpdateWarehouse(WarehouseUpdateRequest? warehouseUpdateRequest);
    }
}
