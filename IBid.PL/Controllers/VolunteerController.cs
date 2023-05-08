using IBid.PL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using IBid.DAL.Models;
using IBid.BLL.Services.Contracts;
using IBid.DAL;

namespace IBid.PL.Controllers
{
    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly ILogger<VolunteerController> _logger;
        private readonly IVolunteerService _volunteerService;
        private readonly IItemService _itemService;
        private readonly IBidService _bidService;
        private readonly IBidHistoryService _bidHistoryService;
        private readonly IAdminService _adminService;

        public VolunteerController(ILogger<VolunteerController> logger, IVolunteerService volunteerService, IItemService itemService, IBidService bidService, IBidHistoryService bidHistoryService, IAdminService adminService)
        {
            _logger = logger;
            _volunteerService = volunteerService;
            _itemService = itemService;
            _bidService = bidService;
            _bidHistoryService = bidHistoryService;
            _adminService = adminService;

        }


        public async Task <IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("VolunteerLogin", "Access");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> ShowBids()
        {
            List<Bid> bids = await _bidService.GetAllBids();
            return View(bids);
        }


        [HttpGet]
        public async Task<ActionResult> PlaceBid(int id)
        {
            var bid = await _bidService.GetBidById(id);

            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        [HttpPost]
        public async Task<ActionResult> PlaceBid(int id, int volunteerId, int currentPrice)
        {
            try
            {
                var admins = await _adminService.GetAllAdmins();
                var emailObserver = new EmailBidObserver(admins);
                var logObserver = new LogBidObserver(ConstantStrings.logPath);
                _bidService.Subscribe(emailObserver);
                _bidService.Subscribe(logObserver);

                await _bidService.PlaceBid(id, volunteerId, currentPrice);
                await _bidHistoryService.CreateBidHistory(new BidHistory { BidId = id, VolunteerId = volunteerId, BidAmount = currentPrice, BidTime = DateTime.Now});
                return RedirectToAction("ShowBids", "Volunteer");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.notAllFieldsCompleted });
            }
        }


    }
}