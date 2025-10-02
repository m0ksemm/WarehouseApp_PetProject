using Microsoft.AspNetCore.Mvc;
using WarehouseWebAPI.Filters;

namespace WarehouseWebAPI.Controllers
{
    [ApiController]
    [TypeFilter(typeof(HandleExceptionFilter))]
    [Route("[controller]")]
    public class WarehousesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
