using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehousesServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.WarehousesServices
{
    public class WarehousesGetterService : IWarehousesGetterService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehousesGetterService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<List<WarehouseResponse>> GetAllWarehouses()
        {
            List<Warehouse> warehouses = await _warehouseRepository.GetAllWarehouses();
            return warehouses.Select(warehouse => warehouse.ToWarehouseResponse()).ToList();
        }

        public async Task<WarehouseResponse?> GetWarehouseById(Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
            {
                return null;
            }
            Warehouse? warehouse_response_from_list = await _warehouseRepository.GetWarehouseById(guid.Value);
            if (warehouse_response_from_list == null)
            {
                return null;
            }
            return warehouse_response_from_list.ToWarehouseResponse();
        }
    }
}
