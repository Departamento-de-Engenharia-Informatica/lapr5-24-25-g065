using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Linq;

namespace DDDSample1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public async Task Login(){
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties{
                    RedirectUri=Url.Action("GoogleResponse")
                });
        }
         [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse(){
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select( claim => new{ 
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
            });
            return Json(claims);
        }
    }
}