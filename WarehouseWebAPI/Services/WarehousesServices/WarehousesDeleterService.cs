using Entities;
using RepositoryContracts;
using ServiceContracts.WarehousesServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehousesServices
{
    public class WarehousesDeleterService : IWarehousesDeleterService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseProductRepository _warehouseProductRepository;
        public WarehousesDeleterService(IWarehouseRepository warehouseRepository, IWarehouseProductRepository warehouseProductRepository)
        {
            _warehouseRepository = warehouseRepository;
            _warehouseProductRepository = warehouseProductRepository;
        }
        public async Task<bool> DeleteWarehouse(Guid? warehouseID)
        {
            if (warehouseID == null || warehouseID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(warehouseID));
            }
            Warehouse? warehouse = await _warehouseRepository.GetWarehouseById(warehouseID.Value);
            if (warehouse == null)
            {
                return false;
            }
            List<WarehouseProduct> warehouseProducts = await _warehouseProductRepository.GetAllWarehouseProducts();
            if (warehouseProducts.Select(warehouseProduct => warehouseProduct.WarehouseID == warehouseID).Count() != 0)
            {
                throw new ArgumentException("This warehouse can not be deleted since there are products that belong to it.");
            }
            await _warehouseRepository.DeleteWarehouse(warehouseID.Value);
            return true;
        }
    }
}
