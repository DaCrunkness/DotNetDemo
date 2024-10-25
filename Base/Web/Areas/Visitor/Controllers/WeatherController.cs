using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}