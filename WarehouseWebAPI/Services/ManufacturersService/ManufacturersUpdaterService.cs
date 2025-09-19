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
            matchingManufacturer.ManufacturerName = manufacturerUpdateRequest.ManufacturerName;
            matchingManufacturer
            await _manufacturerRepository.UpdateCategory(matchingManufacturer);
            return matchingManufacturer.ToCategoryResponse();
        }
    }
}
