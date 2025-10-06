﻿using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ManufacturersDTO;
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
    public class WarehousesUpdaterService : IWarehousesUpdaterService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehousesUpdaterService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task<WarehouseResponse> UpdateWarehouse(WarehouseUpdateRequest? warehouseUpdateRequest)
        {
            if (warehouseUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(warehouseUpdateRequest));
            }
            ValidationHelper.ModelValidation(warehouseUpdateRequest);

            Warehouse? matchingWarehouse = await _warehouseRepository
                .GetWarehouseById(warehouseUpdateRequest.WarehouseID);
            if (matchingWarehouse == null)
            {
                throw new ArgumentException("Given Warehouse ID does not exist.");
            }

            List<Warehouse> warehouses = await _warehouseRepository.GetAllWarehouses();
            if (warehouses.Select(warehouse => warehouse.Name == warehouseUpdateRequest.Name &&
                warehouse.Address == warehouseUpdateRequest.Address &&
                warehouse.SquareArea == warehouseUpdateRequest.SquareArea).Count() != 0)
            {
                throw new ArgumentException("Such Warehouse already exists.");
            }

            matchingWarehouse.Name = warehouseUpdateRequest.Name;

            await _warehouseRepository.UpdateWarehouse(matchingWarehouse);
            return matchingWarehouse.ToWarehouseResponse();
        }
    }
}
