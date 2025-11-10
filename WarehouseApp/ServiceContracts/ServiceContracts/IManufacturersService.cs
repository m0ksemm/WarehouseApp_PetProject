using ServiceContracts.DTOs.ManufacturersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ServiceContracts
{
    public interface IManufacturersService
    {
        Task<bool> AddManufacturer(ManufacturerAddRequest manufacturer);
        Task<List<ManufacturerResponse>> GetAllManufacturers();
        Task<ManufacturerResponse?> GetManufacturerById(Guid guid);
        Task<ManufacturerResponse?> GetManufacturerByName(string manufacturerName);
        Task<bool> UpdateManufacturer(ManufacturerUpdateRequest manufacturer);
        Task<bool> DeleteManufacturer(Guid guid);
    }
}
