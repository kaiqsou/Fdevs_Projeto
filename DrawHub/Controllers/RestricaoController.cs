using Microsoft.AspNetCore.Mvc;
using DrawHub.Filters;

namespace DrawHub.Controllers
{
    public class RestricaoController : Controller
    {
        [UserPage]
        public IActionResult Index()
        {
            return View();
        }
    }
}
