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
        Task<WarehouseProduct?> GetWarehouseProductByWarehouseProductId(Guid warehouseProductId);
        Task<List<WarehouseProduct>?> GetWarehouseProductsByWarehouseId(Guid guid);
        Task<bool> UpdateWarehouseProduct(WarehouseProduct warehouseProduct);
        Task<bool> DeleteWarehouseProduct(Guid warehouseProductID);
    }
}
