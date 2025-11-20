using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs.WarehouseProductsDTOs;
using ServiceContracts.DTOs.WarehousesDTOs;
using ServiceContracts.WarehouseProductsServiceContracts;
using ServiceContracts.WarehousesServiceContracts;
using Services.WarehousesServices;
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


        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<WarehouseProductResponse>> CreateWarehouseProduct(WarehouseProductAddRequest warehouseProductAddRequest)
        {
            WarehouseProductResponse warehouseProductResponce = await _warehouseProductsAdderService.AddWarehouseProduct(warehouseProductAddRequest);
            if (warehouseProductResponce == null)
            {
                return BadRequest(warehouseProductResponce);
            }
            return Ok(warehouseProductResponce);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<WarehouseProductResponse>>> GetAllWarehouseProducts()
        {
            List<WarehouseProductResponse> warehouseProduct = await _warehouseProductsGetterService
                .GetAllWarehouseProducts();
            if (warehouseProduct == null)
            {
                return BadRequest(warehouseProduct);
            }
            return Ok(warehouseProduct);
        }

        //[HttpGet]
        //[Route("[action]/{warehouseID}")]
        //public async Task<ActionResult<WarehouseResponse>> GetWarehouseById(Guid warehouseID)
        //{
        //    WarehouseResponse? warehouseResponse = await _warehousesGetterService.GetWarehouseById(warehouseID);
        //    if (warehouseResponse == null)
        //    {
        //        return NotFound("Warehouse does not exist.");
        //    }
        //    return Ok(warehouseResponse);
        //}

        [HttpDelete]
        [Route("[action]/{warehouseProductID}")]
        public async Task<ActionResult> DeleteWarehouseProduct(Guid warehouseProductID)
        {
            WarehouseProductResponse? warehouseProductsResponse = await _warehouseProductsGetterService
                .GetWarehouseProductById(warehouseProductID);
            if (warehouseProductsResponse == null)
            {
                return NotFound("Warehouse does not exist.");
            }

            bool ifDeleted = await _warehouseProductsDeleterService.DeleteWarehouseProduct(warehouseProductID);
            return Ok(ifDeleted);
        }

        [HttpPut]
        [Route("[action]/{warehouseProductID}")]
        public async Task<ActionResult<WarehouseProductResponse>> UpdateWarehouseProduct(WarehouseProductUpdateRequest warehouseProductUpdateRequest)
        {
            WarehouseProductResponse? warehouseProductResponse = await _warehouseProductsGetterService.GetWarehouseProductById(warehouseProductUpdateRequest.WarehouseProductID);
            if (warehouseProductResponse == null)
            {
                return NotFound("Warehouse does not exist.");
            }

            bool ifDeleted = await _warehouseProductsUpdaterService.UpdateWarehouseProduct(warehouseProductUpdateRequest);
            return Ok(ifDeleted);
        }

    }
}


