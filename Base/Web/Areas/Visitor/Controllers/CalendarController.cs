using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class CalendarController : Controller
    {
        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BackgroundEvent()
        {
            return View();
        }

        public IActionResult DaygridViews()
        {
            return View();
        }

        public IActionResult ExternalDraggingTwoCals()
        {
            return View();
        }

        public IActionResult ExternalDraggingBuiltIn()
        {
            return View();
        }

        public IActionResult FullHeight()
        {
            return View();
        }

        public IActionResult ListStickyHeader()
        {
            return View();
        }

        public IActionResult ListViews()
        {
            return View();
        }

        public IActionResult MonthView()
        {
            return View();
        }

        public IActionResult MultiMonthView()
        {
            return View();
        }

        public IActionResult MultiWeekView()
        {
            return View();
        }

        public IActionResult NaturalHeight()
        {
            return View();
        }

        public IActionResult Selectable()
        {
            return View();
        }

        public IActionResult TimeGridViews()
        {
            return View();
        }

        public IActionResult TimeGridViewsModal()
        {
            return View();
        }

    }
}
