using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Open.Controllers
{
    [Area("Open")]
    public class HealthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


// https://developer.nutritionix.com/