using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts.CategoriesServiceContracts;
using ServiceContracts.DTOs.CategoryDTOs;
using WarehouseWebAPI.Filters;

namespace WarehouseWebAPI.Controllers
{
    [ApiController]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesAdderService _categoriesAdderService;
        private readonly ICategoriesDeleterService _categoriesDeleterService;
        private readonly ICategoriesGetterService _categoriesGetterService;
        private readonly ICategoriesUpdaterService _categoriesUpdaterService;

        public CategoriesController(ICategoriesAdderService categoriesAdderService,
                    ICategoriesDeleterService categoriesDeleterService,
                    ICategoriesGetterService categoriesGetterService,
                    ICategoriesUpdaterService categoriesUpdaterService)
        {
            _categoriesAdderService = categoriesAdderService;
            _categoriesDeleterService = categoriesDeleterService;
            _categoriesGetterService = categoriesGetterService;
            _categoriesUpdaterService = categoriesUpdaterService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<CategoryResponse>> CreateCategory(CategoryAddRequest categoryAddRequest)
        {
            CategoryResponse countryResponse = await _categoriesAdderService.AddCategory(categoryAddRequest);
            if (countryResponse == null)
            {
                return BadRequest(countryResponse);
            }
            return Ok(countryResponse);
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            List<CategoryResponse> categories = await _categoriesGetterService.GetAllCategories();
            if (categories == null)
            {
                return BadRequest(categories);
            }
            return Ok(categories);
        }

        [HttpGet]
        [Route("[action]/{categoryID}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(Guid categoryID)
        {
            CategoryResponse? categoryResponse = await _categoriesGetterService.GetCategoryById(categoryID);
            if (categoryResponse == null)
            {
                return BadRequest(categoryResponse);
            }
            return Ok(categoryResponse);
        }

        [HttpDelete]
        [Route("[action]/{categoryID}")]
        public async Task<ActionResult> DeleteCategory(Guid categoryID)
        {
            CategoryResponse? categoryResponse = await _categoriesGetterService
                .GetCategoryById(categoryID);
            if (categoryResponse == null)
            {
                return NotFound("Category does not exist.");
            }

            //check if it is possible to remove

            bool ifDeleted = await _categoriesDeleterService.DeleteCategory(categoryID);
            return Ok(ifDeleted);
        }

        [HttpPut]
        [Route("[action]/{categoryID}")]
        public async Task<ActionResult<CategoryResponse>> UpdateCategory(CategoryUpdateRequest categoryUpdateRequest)
        {
            CategoryResponse? categoryResponse = await _categoriesGetterService.GetCategoryById(categoryUpdateRequest.CategoryID);
            if (categoryResponse == null)
            {
                return NotFound("Category does not exist.");
            }

            List<CategoryResponse> categories = await _categoriesGetterService.GetAllCategories();
            if (categories.Any(category => category.CategoryName == categoryUpdateRequest.CategoryName)) 
            {
                return BadRequest("Category with this name already exists.");
            }

            CategoryResponse updatedCategoryResponse = await _categoriesUpdaterService.UpdateCategory(categoryUpdateRequest);
            return Ok(updatedCategoryResponse);
        }
    }
}
