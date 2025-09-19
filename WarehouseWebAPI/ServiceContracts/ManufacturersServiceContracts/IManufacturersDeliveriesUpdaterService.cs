using ServiceContracts.DTOs.ManufacturersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ManufacturersServiceContracts
{
    public interface IManufacturersDeliveriesUpdaterService
    {
        Task<ManufacturerResponse> UpdateManufacturerDeliveries(Guid guid, int deliveries);
    }
}
