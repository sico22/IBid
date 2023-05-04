using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using IBid.PL.Models;

using IBid.DAL.Models;
using IAuthenticationService = IBid.BLL.Services.Contracts.IAuthenticationService;

namespace IBid.PL.Controllers
{
    public class AccessController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AccessController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public IActionResult AdminLogin()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
                return RedirectToAction("ShowVolunteers", "Admin");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(VMLogin modelLogin)
        {
            if (_authenticationService.SignInAdmin(modelLogin.Email, modelLogin.Password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                return RedirectToAction("ShowVolunteers", "Admin");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        public IActionResult VolunteerLogin()
        {
            ClaimsPrincipal claimsUser = HttpContext.User;
            if (claimsUser.Identity.IsAuthenticated)
                return RedirectToAction("ShowBids", "Volunteer");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VolunteerLogin(VMLogin modelLogin)
        {
            if (_authenticationService.SignInVolunteer(modelLogin.Email, modelLogin.Password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email)
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity), properties);

                return RedirectToAction("ShowBids", "Volunteer");
            }

            ViewData["ValidateMessage"] = "user not found";
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> VolunteerSignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> VolunteerSignUp(string username, string password, string name)
        {
            try
            {
                Volunteer newVolunteer = new Volunteer
                {
                    Username = username,
                    Password = password,
                    Name = name
                };


                await _authenticationService.SignUpVolunteer(newVolunteer);
                VolunteerLogin(new VMLogin { Email = username, Password = password });
                return RedirectToAction("ShowBids", "Volunteer");

            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields were completed" });
            }
        }
    }
}
