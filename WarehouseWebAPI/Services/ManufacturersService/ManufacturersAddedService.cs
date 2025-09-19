using Entities;
using RepositoryContracts;
using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.ManufacturersServiceContracts;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ManufacturersService
{
    public class ManufacturersAddedService : IManufacturersAdderService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturersAddedService(IManufacturerRepository manufacturerRepository) 
        {
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<ManufacturerResponse> AddManufacturer(ManufacturerAddRequest? manufacturerAddRequest)
        {
            if (manufacturerAddRequest == null)
            {
                throw new ArgumentNullException(nameof(manufacturerAddRequest));
            }
            if (manufacturerAddRequest.ManufacturerName == null)
            {
                throw new ArgumentException(nameof(manufacturerAddRequest.ManufacturerName));
            }
            if (await _manufacturerRepository.GetManufacturerByName(manufacturerAddRequest.ManufacturerName) != null)
            {
                throw new ArgumentException("Manufacturer with a given name already exists.");
            }

            ValidationHelper.ModelValidation(manufacturerAddRequest);

            Manufacturer manufacturer = manufacturerAddRequest.ToManufacturer();
            manufacturer.ManufacturerID = Guid.NewGuid();

            await _manufacturerRepository.AddManufacturer(manufacturer);
            return manufacturer.ToManufacturerResponse();
        }
    }
}
