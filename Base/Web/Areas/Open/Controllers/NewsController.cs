using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Open.Controllers
{
    [Area("Open")]
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


// https://newsapi.org/