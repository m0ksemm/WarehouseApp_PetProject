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
    public class WarehouseProductRepository : IWarehouseProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public WarehouseProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WarehouseProduct> AddWarehouseProduct(WarehouseProduct warehouseProduct)
        {
            _dbContext.WarehouseProducts.Add(warehouseProduct);
            await _dbContext.SaveChangesAsync();
            return warehouseProduct;
        }

        public async Task<bool> DeleteWarehouseProduct(Guid warehouseProductID)
        {
            _dbContext.WarehouseProducts.RemoveRange(_dbContext.WarehouseProducts
                .Where(temp => temp.WarehouseProductID == warehouseProductID));
            int rowsDeleted = await _dbContext.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<WarehouseProduct>> GetAllWarehouseProducts()
        {
            return await _dbContext.WarehouseProducts.Include(wp => wp.Warehouse)
                .Include(wp => wp.Product)
                    .ThenInclude(p => p.Category)
                .Include(wp => wp.Product)
                    .ThenInclude(p => p.Manufacturer).ToListAsync();
        }

        public async Task<WarehouseProduct?> GetWarehouseProductByWarehouseProductId(Guid warehouseProductId)
        {
            return await _dbContext.WarehouseProducts
                .Include(wp => wp.Warehouse)
                .Include(wp => wp.Product)
                    .ThenInclude(p => p.Category)
                .Include(wp => wp.Product)
                    .ThenInclude(p => p.Manufacturer)
                .FirstOrDefaultAsync(wp => wp.WarehouseProductID == warehouseProductId);
        }

        public async Task<List<WarehouseProduct>?> GetWarehouseProductsByWarehouseId(Guid guid)
        {
            return await _dbContext.WarehouseProducts
                .Include(wp => wp.Warehouse)
                .Include(wp => wp.Product)
                    .ThenInclude(p => p.Category)
                .Include(wp => wp.Product)
                    .ThenInclude(p => p.Manufacturer)
                .Where(wp => wp.WarehouseID == guid).ToListAsync();
        }

        public async Task<bool> UpdateWarehouseProduct(WarehouseProduct warehouseProduct)
        {
            WarehouseProduct? matchingWarehouseProduct = await _dbContext.WarehouseProducts
                .Include("Product").Include("Warehouse")
                .FirstOrDefaultAsync(temp => temp.WarehouseProductID == warehouseProduct.WarehouseProductID);
            if (matchingWarehouseProduct == null)
            {
                return false;
            }
            matchingWarehouseProduct.WarehouseID = warehouseProduct.WarehouseID;
            matchingWarehouseProduct.ProductID = warehouseProduct.ProductID;
            matchingWarehouseProduct.UpdatedAt = warehouseProduct.UpdatedAt;
            matchingWarehouseProduct.Count = warehouseProduct.Count;

            int countUpdated = await _dbContext.SaveChangesAsync();

            return countUpdated > 0;
        }
    }
}
