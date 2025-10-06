using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehousesServiceContracts;
using WarehouseWebAPI.Filters;

namespace WarehouseWebAPI.Controllers
{
    [ApiController]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [Route("[controller]")]
    public class WarehousesController : Controller
    {
        private readonly IWarehousesAdderService _warehousesAdderService;
        private readonly IWarehousesGetterService _warehousesGetterService;
        private readonly IWarehousesDeleterService _warehousesDeleterService;
        private readonly IWarehousesUpdaterService _warehousesUpdaterService;

        public WarehousesController(IWarehousesAdderService warehousesAdderService, IWarehousesGetterService warehousesGetterService, IWarehousesDeleterService warehousesDeleterService, IWarehousesUpdaterService warehousesUpdaterService)
        {
            _warehousesAdderService = warehousesAdderService;
            _warehousesGetterService = warehousesGetterService;
            _warehousesDeleterService = warehousesDeleterService;
            _warehousesUpdaterService = warehousesUpdaterService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<WarehouseResponse>> CreateWarehouse(WarehouseAddRequest warehouseAddRequest)
        {
            WarehouseResponse warehouseResponce = await _warehousesAdderService.AddWarehouse(warehouseAddRequest);
            if (warehouseResponce == null)
            {
                return BadRequest(warehouseResponce);
            }
            return Ok(warehouseResponce);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<WarehouseResponse>>> GetAllWarehouses()
        {
            List<WarehouseResponse> warehouse = await _warehousesGetterService
                .GetAllWarehouses();
            if (warehouse == null)
            {
                return BadRequest(warehouse);
            }
            return Ok(warehouse);
        }

        [HttpGet]
        [Route("[action]/{warehouseID}")]
        public async Task<ActionResult<WarehouseResponse>> GetWarehouseById(Guid warehouseID)
        {
            WarehouseResponse? warehouseResponse = await _warehousesGetterService.GetWarehouseById(warehouseID);
            if (warehouseResponse == null)
            {
                return NotFound("Warehouse does not exist.");
            }
            return Ok(warehouseResponse);
        }

        [HttpDelete]
        [Route("[action]/{warehouseID}")]
        public async Task<ActionResult> DeleteWarehouse(Guid warehouseID)
        {
            WarehouseResponse? warehousesResponse = await _warehousesGetterService
                .GetWarehouseById(warehouseID);
            if (warehousesResponse == null)
            {
                return NotFound("Warehouse does not exist.");
            }

            bool ifDeleted = await _warehousesDeleterService.DeleteWarehouse(warehouseID);
            return Ok(ifDeleted);
        }

        [HttpPut]
        [Route("[action]/{warehouseID}")]
        public async Task<ActionResult<WarehouseResponse>> UpdateWarehouse(WarehouseUpdateRequest warehouseUpdateRequest)
        {
            WarehouseResponse? warehouseResponse = await _warehousesGetterService.GetWarehouseById(warehouseUpdateRequest.WarehouseID);
            if (warehouseResponse == null)
            {
                return NotFound("Warehouse does not exist.");
            }

            WarehouseResponse updatedWarehouseResponse = await _warehousesUpdaterService.UpdateWarehouse(warehouseUpdateRequest);
            return Ok(updatedWarehouseResponse);
        }

    }
}
