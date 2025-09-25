using Entities;
using RepositoryContracts;
using ServiceContracts.ManufacturersServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ManufacturersService
{
    public class ManufacturersDeleterService : IManufacturersDeleterService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturersDeleterService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<bool> DeleteManufacturer(Guid? manufacturerID)
        {
            if (manufacturerID == null || manufacturerID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(manufacturerID));
            }
            Manufacturer? manufacturer = await _manufacturerRepository.GetManufacturerById(manufacturerID.Value);
            if (manufacturer == null)
            {
                return false;
            }
            //List<Product> products = 
            //Chech if products with such category exist
            await _manufacturerRepository.DeleteManufacturer(manufacturerID.Value);
            return true;
        }
    }
}
