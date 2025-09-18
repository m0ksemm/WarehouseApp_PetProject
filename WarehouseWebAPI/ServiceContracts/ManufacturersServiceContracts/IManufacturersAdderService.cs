using ServiceContracts.DTOs.CategoryDTOs;
using ServiceContracts.DTOs.ManufacturersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.ManufacturersServiceContracts
{
    public interface IManufacturersAdderService
    {
        Task<ManufacturerResponse> AddManufacturer(ManufacturerAddRequest? manufacturerAddRequest);
    }
}
