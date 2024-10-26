using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using DDDSample1.Domain.Users;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;


public class LoginController : Controller
{
    private readonly UserService _userService; 

    public LoginController(UserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(string userName, string password)
    {
       
            var user = await _userService.AuthenticateAsync(userName, password);
            if (user != null)
            {
                
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            
            switch (user.Role)
            {
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

        ModelState.AddModelError("", "Invalid login attempt.");

        return View();
    }

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
