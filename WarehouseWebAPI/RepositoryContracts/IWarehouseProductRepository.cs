using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IWarehouseProductRepository
    {
        Task<WarehouseProduct> AddWarehouseProduct(WarehouseProduct warehouseProduct);
        Task<List<WarehouseProduct>> GetAllWarehouseProducts();
        Task<WarehouseProduct?> GetWarehouseProductById(Guid guid);
        Task<WarehouseProduct> UpdateWarehouseProduct(WarehouseProduct warehouseProduct);
        Task<WarehouseProduct> DeleteWarehouseProduct(Guid guid);
    }
}
