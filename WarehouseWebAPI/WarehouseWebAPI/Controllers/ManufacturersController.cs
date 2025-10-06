using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.ManufacturersServiceContracts;
using Services.ManufacturersService;
using WarehouseWebAPI.Filters;

namespace WarehouseWebAPI.Controllers
{
    [ApiController]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [Route("[controller]")]
    public class ManufacturersController : Controller
    {
        private readonly IManufacturersAdderService _manufacturersAdderService;
        private readonly IManufacturersDeleterService _manufacturersDeleterService;
        private readonly IManufacturersGetterService _manufacturersGetterService;
        private readonly IManufacturersUpdaterService _manufacturersUpdaterService;

        public ManufacturersController(IManufacturersAdderService manufacturersAdderService,
            IManufacturersDeleterService manufacturersDeleterService,
            IManufacturersGetterService manufacturersGetterService,
            IManufacturersUpdaterService manufacturersUpdaterService)
        {
            _manufacturersAdderService = manufacturersAdderService;
            _manufacturersDeleterService = manufacturersDeleterService;
            _manufacturersGetterService = manufacturersGetterService;
            _manufacturersUpdaterService = manufacturersUpdaterService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ManufacturerResponse>> CreateManufacturer(ManufacturerAddRequest manufacturerAddRequest)
        {
            ManufacturerResponse manufacturerResponse = await _manufacturersAdderService.AddManufacturer(manufacturerAddRequest);
            if (manufacturerResponse == null)
            {
                return BadRequest(manufacturerResponse);
            }
            return Ok(manufacturerResponse);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ManufacturerResponse>>> GetAllManufacturers()
        {
            List<ManufacturerResponse> manufacturer = await _manufacturersGetterService
                .GetAllManufacturers();
            if (manufacturer == null)
            {
                return BadRequest(manufacturer);
            }
            return Ok(manufacturer);
        }

        [HttpGet]
        [Route("[action]/{manufacturerID}")]
        public async Task<ActionResult<ManufacturerResponse>> GetManufacturerById(Guid manufacturerID)
        {
            ManufacturerResponse? manufacturerResponse = await _manufacturersGetterService.GetManufacturerById(manufacturerID);
            if (manufacturerResponse == null)
            {
                return NotFound("Manufacturer does not exist.");
            }
            return Ok(manufacturerResponse);
        }

        [HttpDelete]
        [Route("[action]/{manufacturerID}")]
        public async Task<ActionResult> DeleteManufacturer(Guid manufacturerID)
        {
            ManufacturerResponse? manufacturerResponse = await _manufacturersGetterService
                .GetManufacturerById(manufacturerID);
            if (manufacturerResponse == null)
            {
                return NotFound("Manufacturer does not exist.");
            }

            //check if it is possible to remove

            bool ifDeleted = await _manufacturersDeleterService.DeleteManufacturer(manufacturerID);
            return Ok(ifDeleted);
        }

        [HttpPut]
        [Route("[action]/{manufacturerID}")]
        public async Task<ActionResult<ManufacturerResponse>> UpdateManufacturer(ManufacturerUpdateRequest manufacturerUpdateRequest)
        {
            ManufacturerResponse? manufacturerResponse = await _manufacturersGetterService.GetManufacturerById(manufacturerUpdateRequest.ManufacturerID);
            if (manufacturerResponse == null)
            {
                return NotFound("Manufacturer does not exist.");
            }

            ManufacturerResponse updatedManufacturerResponse = await _manufacturersUpdaterService.UpdateManufacturer(manufacturerUpdateRequest);
            return Ok(updatedManufacturerResponse);
        }

        [HttpPut]
        [Route("[action]/{manufacturerID}")]
        public async Task<ActionResult<ManufacturerResponse>> UpdateManufacturerDeliveries(Guid manufacturerID, int deliveriesUpdateNumber)
        {
            ManufacturerResponse? manufacturerResponse = await _manufacturersGetterService.GetManufacturerById(manufacturerID);
            if (manufacturerResponse == null)
            {
                return NotFound("Manufacturer does not exist.");
            }

            ManufacturerResponse updatedManufacturerResponse = await _manufacturersUpdaterService.UpdateManufacturerDeliveries(manufacturerID, deliveriesUpdateNumber);
            return Ok(updatedManufacturerResponse);
        }
    }
}
