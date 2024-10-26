using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    public class PatientUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
