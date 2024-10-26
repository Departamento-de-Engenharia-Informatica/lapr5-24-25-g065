using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    public class DoctorUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
