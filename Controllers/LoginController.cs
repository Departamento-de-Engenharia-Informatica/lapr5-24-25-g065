using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DDDSample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task Login(){
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties{
                    RedirectUri=Url.Action("GoogleResponse")
                });
        }

        public async Task<IActionResult> GoogleResponse(){
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticaionDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select( claims => new{ 
            claim.Issuer,
            claims.OriginalIssuer,
            claim.Type,
            claim.Value
            });

            return Json(claims);
        }
    }
}
