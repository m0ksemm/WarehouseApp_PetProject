using Entities;
using RepositoryContracts;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.DTOs.CategoryDTOs;
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
    public class ManufacturersUpdaterService : IManufacturersUpdaterService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturersUpdaterService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }
        public async Task<ManufacturerResponse> UpdateManufacturer(ManufacturerUpdateRequest? manufacturerUpdateRequest)
        {
            if (manufacturerUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(manufacturerUpdateRequest));
            }
            ValidationHelper.ModelValidation(manufacturerUpdateRequest);

            Manufacturer? matchingManufacturer = await _manufacturerRepository
                .GetManufacturerById(manufacturerUpdateRequest.ManufacturerID);
            if (matchingManufacturer == null)
            {
                throw new ArgumentException("Given Manufacturer ID does not exist.");
            }

            List<Manufacturer> manufacturers = await _manufacturerRepository.GetAllManufacturers();
            if (manufacturers.Select(manufacturer => manufacturer.ManufacturerName).Contains(manufacturerUpdateRequest.ManufacturerName))
            {
                throw new ArgumentException("Manufacturer with this name already exists.");
            }

            matchingManufacturer.ManufacturerName = manufacturerUpdateRequest.ManufacturerName;

            await _manufacturerRepository.UpdateManufacturer(matchingManufacturer);
            return matchingManufacturer.ToManufacturerResponse();
        }

        public async Task<ManufacturerResponse> UpdateManufacturerDeliveries(Guid? guid, int deliveries)
        {
            if (guid == null || guid == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(guid));
            }
            if (deliveries == 0)
            {
                throw new ArgumentException(nameof(deliveries));
            }
            Manufacturer? matchingManufacturer = await _manufacturerRepository
                .GetManufacturerById(guid.Value);
            if (matchingManufacturer == null)
            {
                throw new ArgumentException("Given Manufacturer ID does not exist.");
            }

            matchingManufacturer.Deliveries = checked(matchingManufacturer.Deliveries + deliveries);

            await _manufacturerRepository.UpdateManufacturer(matchingManufacturer);
            return matchingManufacturer.ToManufacturerResponse();
        }
    }
}
