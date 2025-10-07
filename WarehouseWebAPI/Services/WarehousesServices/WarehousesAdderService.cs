using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehousesServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehousesServices
{
    public class WarehousesAdderService : IWarehousesAdderService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehousesAdderService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<WarehouseResponse> AddWarehouse(WarehouseAddRequest? warehouseAddRequest)
        {
            if (warehouseAddRequest == null)
            {
                throw new ArgumentNullException(nameof(warehouseAddRequest));
            }
            if (warehouseAddRequest.Name == null)
            {
                throw new ArgumentException(nameof(warehouseAddRequest.Name));
            }
            List<Warehouse> warehouses = await _warehouseRepository.GetAllWarehouses();
            if (warehouses.Any(warehouse => warehouse.WarehouseName == warehouseAddRequest.Name &&
                warehouse.SquareArea == warehouseAddRequest.SquareArea &&
                warehouse.Address == warehouseAddRequest.Address))
            {
                throw new ArgumentException("Such warehouse already exists.");
            }

            ValidationHelper.ModelValidation(warehouseAddRequest);

            Warehouse warehouse = warehouseAddRequest.ToWarehouse();
            warehouse.WarehouseID = Guid.NewGuid();

            await _warehouseRepository.AddWarehouse(warehouse);
            return warehouse.ToWarehouseResponse();
        }
    }
}
