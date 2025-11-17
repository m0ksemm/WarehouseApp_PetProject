using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ServiceContracts
{
    public interface IWarehousesService
    {
        Task<bool> AddWarehouse(WarehouseAddRequest warehouse);
        Task<List<WarehouseResponse>> GetAllWarehouses();
        Task<WarehouseResponse?> GetWarehouseById(Guid guid);
        Task<WarehouseResponse?> GetWarehouseByName(string warehouseName);
        Task<bool> UpdateWarehouse(WarehouseUpdateRequest warehouse);
        Task<bool> DeleteWarehouse(Guid guid);
    }
}
