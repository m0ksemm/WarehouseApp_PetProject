using Microsoft.AspNetCore.Mvc;
using ServiceContracts.DTOs.ManufacturersDTO;
using ServiceContracts.DTOs.ProductsDTO;
using ServiceContracts.ProductsServiceContracts;
using WarehouseWebAPI.Filters;

namespace WarehouseWebAPI.Controllers
{
    [ApiController]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductsAdderService _productsAdderService;
        private readonly IProductsGetterService _productsGetterService;
        private readonly IProductsDeleterService _productsDeleterService;
        private readonly IProductsUpdaterService _productsUpdaterService;

        public ProductsController(IProductsAdderService productsAdderService, 
            IProductsGetterService productsGetterService,
            IProductsDeleterService productsDeleterService,
            IProductsUpdaterService productsUpdaterService) 
        {
            _productsAdderService = productsAdderService;
            _productsGetterService = productsGetterService;
            _productsDeleterService = productsDeleterService;
            _productsUpdaterService = productsUpdaterService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ProductResponse>> CreateProduct(ProductAddRequest productAddRequest)
        {
            ProductResponse productResponse = await _productsAdderService.AddProduct(productAddRequest);
            if (productResponse == null)
            {
                return BadRequest(productResponse);
            }
            return Ok(productResponse);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            List<ProductResponse> product = await _productsGetterService
                .GetAllProducts();
            if (product == null)
            {
                return BadRequest(product);
            }
            return Ok(product);
        }

        [HttpGet]
        [Route("[action]/{productID}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(Guid productID)
        {
            ProductResponse? productResponse = await _productsGetterService.GetProductById(productID);
            if (productResponse == null)
            {
                return NotFound("Product does not exist.");
            }
            return Ok(productResponse);
        }


        [HttpDelete]
        [Route("[action]/{productID}")]
        public async Task<ActionResult> DeleteProduct(Guid productID)
        {
            ProductResponse? productResponse = await _productsGetterService
                .GetProductById(productID);
            if (productResponse == null)
            {
                return NotFound("Product does not exist.");
            }

            //check if it is possible to delete

            bool ifDeleted = await _productsDeleterService.DeleteProduct(productID);
            return Ok(ifDeleted);
        }

        [HttpPut]
        [Route("[action]/{productID}")]
        public async Task<ActionResult<ProductResponse>> UpdateProduct(ProductUpdateRequest productUpdateRequest)
        {
            ProductResponse? productResponse = await _productsGetterService.GetProductById(productUpdateRequest.ProductID);
            if (productResponse == null)
            {
                return NotFound("Product does not exist.");
            }

            

            ProductResponse updatedProductResponse = await _productsUpdaterService.UpdateProduct(productUpdateRequest);
            return Ok(updatedProductResponse);
        }
    }
}
