using Microsoft.AspNetCore.Mvc;
using ServiceContracts.WarehouseProductsServiceContracts;
using ServiceContracts.WarehousesServiceContracts;
using WarehouseWebAPI.Filters;

namespace WarehouseWebAPI.Controllers
{
    [ApiController]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [Route("[controller]")]
    public class WarehouseProductsController : Controller
    {
        private readonly IWarehouseProductsAdderService _warehouseProductsAdderService;
        private readonly IWarehouseProductsGetterService _warehouseProductsGetterService;
        private readonly IWarehouseProductsDeleterService _warehouseProductsDeleterService;
        private readonly IWarehouseProductsUpdaterService _warehouseProductsUpdaterService;

        public WarehouseProductsController(IWarehouseProductsAdderService warehouseProductsAdderService, IWarehouseProductsGetterService warehouseProductsGetterService, IWarehouseProductsDeleterService warehouseProductsDeleterService, IWarehouseProductsUpdaterService warehouseProductsUpdaterService)
        {
            _warehouseProductsAdderService = warehouseProductsAdderService;
            _warehouseProductsGetterService = warehouseProductsGetterService;
            _warehouseProductsDeleterService = warehouseProductsDeleterService;
            _warehouseProductsUpdaterService = warehouseProductsUpdaterService;
        }

    }
}
