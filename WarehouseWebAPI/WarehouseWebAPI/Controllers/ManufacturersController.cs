using Microsoft.AspNetCore.Mvc;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.DTOs.CategoryDTOs;
using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.ManufacturersServiceContracts;
using Services.CategoriesServices;
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
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> ReadAllManufacturers()
        {
            List<ManufacturerResponse> manufacturer = await _manufacturersGetterService
                .GetAllManufacturers();
            if (manufacturer == null)
            {
                return BadRequest(manufacturer);
            }
            return Ok(manufacturer);
        }
    }
}
