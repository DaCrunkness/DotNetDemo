using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Open.Controllers
{
    [Area("Open")]
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


// https://openweathermap.org