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
        public async Task<IActionResult> Login()  // Updated to return IActionResult
        {
            // Initiates the Google authentication challenge
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse") // Ensure this matches the authorized redirect URI in Google Console
                });
            return new EmptyResult(); // Return an empty result after challenge
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            // Authenticate with the cookie scheme
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Check if the result is null or if the principal is null
            if (result == null || result.Principal == null)
            {
                // Handle the case when authentication fails
                return Unauthorized(); // Return 401 Unauthorized
            }

            // Extract claims from the authenticated principal
            var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(claim => new {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            // Return claims as JSON
            return Json(claims);
        }
    }
}
