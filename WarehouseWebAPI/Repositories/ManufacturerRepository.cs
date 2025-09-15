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
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ManufacturerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Manufacturer> AddManufacturer(Manufacturer manufacturer)
        {
            _dbContext.Manufacturers.Add(manufacturer);
            await _dbContext.SaveChangesAsync();
            return manufacturer;
        }

        public async Task<bool> DeleteManufacturer(Guid guid)
        {
            _dbContext.Manufacturers.RemoveRange(_dbContext.Manufacturers.Where(temp => temp.ManufacturerID == guid));
            int rowsDeleted = await _dbContext.SaveChangesAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Manufacturer>> GetAllManufacturers()
        {
            return await _dbContext.Manufacturers.ToListAsync();
        }

        public async Task<Manufacturer?> GetManufacturerById(Guid guid)
        {
            return await _dbContext.Manufacturers.FirstOrDefaultAsync(temp => temp.ManufacturerID == guid);
        }

        public async Task<Manufacturer?> GetManufacturerByName(string manufacturerName)
        {
            return await _dbContext.Manufacturers.FirstOrDefaultAsync(temp => temp.ManufacturerName == manufacturerName);
        }

        public async Task<Manufacturer?> UpdateManufacturer(Manufacturer manufacturer)
        {
            Manufacturer? matchingManufacturer = await _dbContext.Manufacturers
                .FirstOrDefaultAsync(temp => temp.ManufacturerID == manufacturer.ManufacturerID);
            if (matchingManufacturer == null)
            {
                return null;
            }
            matchingManufacturer.ManufacturerName = manufacturer.ManufacturerName;
            matchingManufacturer.Deliveries = manufacturer.Deliveries;
            int countUpdated = await _dbContext.SaveChangesAsync();
            return matchingManufacturer;
        }

        public async Task<bool> UpdateManufacturerDeliveries(Guid guid, int deliveries)
        {
            Manufacturer? matchingManufacturer = await _dbContext.Manufacturers
                .FirstOrDefaultAsync(temp => temp.ManufacturerID == guid);
            if (matchingManufacturer == null)
            {
                return false;
            }
            matchingManufacturer.Deliveries += deliveries;
            int countUpdated = await _dbContext.SaveChangesAsync();
            return countUpdated > 0;
        }
    }
}
