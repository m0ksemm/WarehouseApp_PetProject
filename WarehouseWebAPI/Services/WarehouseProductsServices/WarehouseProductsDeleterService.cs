using Entities;
using RepositoryContracts;
using ServiceContracts.WarehouseProductsServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehouseProductsServices
{
    public class WarehouseProductsDeleterService : IWarehouseProductsDeleterService
    {
        private readonly IWarehouseProductRepository _warehouseProductRepository;
        public WarehouseProductsDeleterService(IWarehouseProductRepository warehouseProductRepository)
        {
            _warehouseProductRepository = warehouseProductRepository;
        }
        public async Task<bool> DeleteWarehouseProduct(Guid? warehouseProductID)
        {
            if (warehouseProductID == null || warehouseProductID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseProductID));
            }
            WarehouseProduct? warehouseProduct = await _warehouseProductRepository.GetWarehouseProductByWarehouseProductId(warehouseProductID.Value);
            if (warehouseProduct == null)
            {
                return false;
            }
            await _warehouseProductRepository.DeleteWarehouseProduct(warehouseProductID.Value);
            return true;
        }
    }
}
