using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Open.Controllers
{
    [Area("Open")]
    public class LocationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

// https://opencagedata.com/