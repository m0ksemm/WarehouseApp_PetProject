using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WarehouseRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Warehouse> AddWarehouse(Warehouse warehouse)
        {
            _dbContext.Warehouses.Add(warehouse);
            await _dbContext.SaveChangesAsync();
            return warehouse;
        }

        public async Task<bool> DeleteWarehouse(Guid guid)
        {
            _dbContext.Warehouses.RemoveRange(_dbContext.Warehouses.Where(temp => temp.WarehouseID == guid));
            int rowsDeleted = await _dbContext.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Warehouse>> GetAllWarehouses()
        {
            return await _dbContext.Warehouses.ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseById(Guid guid)
        {
            return await _dbContext.Warehouses.FirstOrDefaultAsync(temp => temp.WarehouseID == guid);
        }

        public async Task<Warehouse?> GetWarehouseByName(string warehouseName)
        {
            return await _dbContext.Warehouses.FirstOrDefaultAsync(temp => temp.Name == warehouseName);
        }

        public async Task<bool> UpdateWarehouse(Warehouse warehouse)
        {
            Warehouse? matchingWarehouse = await _dbContext.Warehouses
                .FirstOrDefaultAsync(temp => temp.WarehouseID == warehouse.WarehouseID);
            if (matchingWarehouse == null)
            {
                return false;
            }
            matchingWarehouse.Name = warehouse.Name;
            matchingWarehouse.SquareArea = warehouse.SquareArea;
            matchingWarehouse.Address = warehouse.Address;

            int countUpdated = await _dbContext.SaveChangesAsync();
            return countUpdated > 0;
        }
    }
}
