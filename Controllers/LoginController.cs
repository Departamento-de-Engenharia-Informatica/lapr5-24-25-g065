using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using System.Linq;
using DDDSample1.Domain.Users;
using System.Text.Json;
using System;
using System.Security.Claims;

namespace DDDSample1.Controllers
{
    //private readonly UserService _userService; 
    [Route("[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly UserService _userService; 
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
            return new EmptyResult();
        }

        [HttpGet("google-response")]
        public async void GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select( claim => new{ 
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
            });
            string email = GetEmailFromClaims(claims);
            
            UserDto user = await _userService.GetUserByEmailAsync(email);
            RedirectToUserPage(user.Role);


        }

        private RedirectToActionResult RedirectToUserPage(Role role)
        {
            switch (role){
                case Role.Admin:
                    return RedirectToAction("Index", "Admin"); 
                case Role.Doctor:
                    return RedirectToAction("Index", "Doctor");
                case Role.Nurse:
                    return RedirectToAction("Index", "Nurse"); 
                case Role.Technician:
                    return RedirectToAction("Index", "Technician"); 
                case Role.Patient:
                    return RedirectToAction("Index", "Patient");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }

        private string GetEmailFromClaims(IEnumerable<dynamic> claims)
        {
            var emailClaim = claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            return emailClaim?.Value;
        }
    }
}
