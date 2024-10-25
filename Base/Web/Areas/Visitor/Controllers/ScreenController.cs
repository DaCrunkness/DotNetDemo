using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class ScreenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TwoColumn()
        {
            return View();
        }

        public IActionResult Card()
        {
            return View();
        }

        public IActionResult FullWidth()
        {
            return View();
        }

        public IActionResult SideBar()
        {
            return View();
        }
    }
}
