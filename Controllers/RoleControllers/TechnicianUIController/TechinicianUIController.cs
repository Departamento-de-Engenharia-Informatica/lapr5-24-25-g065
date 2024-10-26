using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    public class TechnicianUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
