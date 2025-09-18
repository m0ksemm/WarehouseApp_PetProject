using ServiceContracts.DTOs.ManufacturersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ManufacturersServiceContracts
{
    public interface IManufacturersGetterService
    {
        Task<List<ManufacturerResponse>> GetAllManufacturers();
        Task<ManufacturerResponse?> GetManufacturerById(Guid? guid);
    }
}
