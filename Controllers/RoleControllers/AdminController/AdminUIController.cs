using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    public class AdminUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
