using Microsoft.AspNetCore.Mvc;

namespace DDDSample1.Controllers
{
    public class NurseUIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
