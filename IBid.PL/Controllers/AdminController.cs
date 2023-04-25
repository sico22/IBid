using IBid.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using IBid.DAL.Models;
using IBid.BLL.Services.Contracts;


namespace IBid.PL.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IVolunteerService _volunteerService;

        public AdminController(ILogger<AdminController> logger, IVolunteerService volunteerService)
        {
            _logger = logger;
            _volunteerService = volunteerService;
        }


        public async Task <IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("AdminLogin", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShowVolunteers()
        {
            List<Volunteer> volunteers = await _volunteerService.GetAllVolunteers();
            return View(volunteers);
        }

        public async Task<ActionResult> DeleteVolunteer(int id)
        {
            var volunteer = await _volunteerService.GetVolunteerById(id);

            if (volunteer == null)
            {
                return NotFound();
            }


            await _volunteerService.DeleteVolunteer(volunteer.VolunteerId);

            return RedirectToAction("ShowVolunteers", "Admin");
        }


        [HttpGet]
        public async Task<ActionResult> EditVolunteer(int id)
        {
            var volunteer = await _volunteerService.GetVolunteerById(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        [HttpPost]
        public async Task<ActionResult> EditVolunteer(int volunteerId, string username, string password, string name)
        {
            try
            {
                await _volunteerService.EditVolunteer(volunteerId,
                username, password, name);
                return RedirectToAction("ShowVolunteers", "Admin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = "Not all fields completed" });
            }
        }


    }
}