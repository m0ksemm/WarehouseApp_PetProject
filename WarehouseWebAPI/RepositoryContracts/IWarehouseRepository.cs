using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IWarehouseRepository
    {
        Task<Warehouse> AddWarehouse(Warehouse warehouse);
        Task<List<Warehouse>> GetAllWarehouses();
        Task<Warehouse?> GetWarehouseById(Guid guid);
        Task<Warehouse?> GetWarehouseByName(string warehouseName);
        Task<Warehouse> UpdateWarehouse(Warehouse warehouse);
        Task<Warehouse> DeleteWarehouse(Guid guid);
    }
}
