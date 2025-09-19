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
    internal class ManufacturersDeliveriesUpdaterService : IManufacturersDeliveriesUpdaterService
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        public ManufacturersDeliveriesUpdaterService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
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
