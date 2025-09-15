using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IManufacturerRepository
    {
        Task<Manufacturer> AddManufacturer(Manufacturer manufacturer);
        Task<List<Manufacturer>> GetAllManufacturers();
        Task<Manufacturer?> GetManufacturerById(Guid guid);
        Task<Manufacturer?> GetManufacturerByName(string manufacturerName);
        Task<Manufacturer?> UpdateManufacturer(Manufacturer manufacturer);
        Task<bool> UpdateManufacturerDeliveries(Guid guid, int deliveries);
        Task<bool> DeleteManufacturer(Guid guid);
    }
}
