using ServiceContracts.DTOs.WarehousesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.WarehousesServiceContracts
{
    public interface IWarehousesGetterService
    {
        Task<List<WarehouseResponse>> GetAllWarehouses();
        Task<WarehouseResponse?> GetWarehouseById(Guid? guid);
    }
}
