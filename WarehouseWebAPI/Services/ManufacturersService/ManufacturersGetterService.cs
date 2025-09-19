using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.ManufacturersServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ManufacturersService
{
    public class ManufacturersGetterService : IManufacturersGetterService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturersGetterService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<List<ManufacturerResponse>> GetAllManufacturers()
        {
            List<Manufacturer> manufacturers = await _manufacturerRepository.GetAllManufacturers();
            return manufacturers.Select(manufacturer => manufacturer.ToManufacturerResponse()).ToList();
        }

        public async Task<ManufacturerResponse?> GetManufacturerById(Guid? guid)
        {
            if (guid == null || guid == Guid.Empty)
            {
                return null;
            }
            Manufacturer? manufacturer_response_from_list = await _manufacturerRepository.GetManufacturerById(guid.Value);
            if (manufacturer_response_from_list == null)
            {
                return null;
            }
            return manufacturer_response_from_list.ToManufacturerResponse();
        }
    }
}
