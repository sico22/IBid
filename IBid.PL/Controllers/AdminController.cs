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
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IVolunteerService _volunteerService;
        private readonly IItemService _itemService;
        private readonly IBidService _bidService;

        public AdminController(ILogger<AdminController> logger, IVolunteerService volunteerService, IItemService itemService, IBidService bidService)
        {
            _logger = logger;
            _volunteerService = volunteerService;
            _itemService = itemService;
            _bidService = bidService;
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

        public async Task<ActionResult> UndoBid(int id)
        {
            var bid = await _bidService.GetBidById(id);

            if (bid == null)
            {
                return NotFound();
            }


            await _bidService.UndoBid(id);

            return RedirectToAction("ShowBids", "Admin");
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
                return StatusCode(StatusCodes.Status401Unauthorized, new { ConstantStrings.notAllFieldsCompleted });
            }
        }

        public async Task<IActionResult> ShowItems()
        {
            List<Item> items = await _itemService.GetAllItems();
            return View(items);
        }

        [HttpGet]
        public async Task<ActionResult> CreateItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem(string name, string description)
        {
            try
            {
                Item newItem = new Item
                {
                    Name = name,
                    Description = description
                };

                await _itemService.CreateItem(newItem);
                return RedirectToAction("ShowItems", "Admin");
            }
            catch
            {
                return RedirectToAction("CreateItem", "Admin");
            }
        }

        public async Task<ActionResult> DeleteItem(int id)
        {
            var item = await _itemService.GetItemById(id);

            if (item == null)
            {
                return NotFound();
            }


            await _itemService.DeleteItem(item.ItemId);

            return RedirectToAction("ShowItems", "Admin");
        }


        [HttpGet]
        public async Task<ActionResult> EditItem(int id)
        {
            var item = await _itemService.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        [HttpPost]
        public async Task<ActionResult> EditItem(int itemId, string name, string description)
        {
            try
            {
                await _itemService.EditItem(itemId,
                name, description);
                return RedirectToAction("ShowItems", "Admin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { ConstantStrings.notAllFieldsCompleted });
            }
        }

        public async Task<IActionResult> ShowBids()
        {
            List<Bid> bids = await _bidService.GetAllBids();
            return View(bids);
        }

        [HttpGet]
        public async Task<ActionResult> CreateBid()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBid(int itemId, int startingPrice, DateTime startTime, DateTime endTime)
        {
            var item = _itemService.GetItemById(itemId);

            if(item == null)
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.notAllFieldsCompleted });
            

            try
            {
                Bid newBid = new Bid
                {
                    ItemId = itemId,
                    StartingPrice = startingPrice,
                    StartTime = startTime,
                    EndTime = endTime
                };

                await _bidService.CreateBid(newBid);
                return RedirectToAction("ShowBids", "Admin");
            }
            catch
            {
                return RedirectToAction("CreateBid", "Admin");
            }
        }

        public async Task<ActionResult> DeleteBid(int id)
        {
            var bid = await _bidService.GetBidById(id);

            if (bid == null)
            {
                return NotFound();
            }


            await _bidService.DeleteBid(bid.BidId);

            return RedirectToAction("ShowBids", "Admin");
        }


        [HttpGet]
        public async Task<ActionResult> EditBid(int id)
        {
            var bid = await _bidService.GetBidById(id);
            if (bid == null)
            {
                return NotFound();
            }

            return View(bid);
        }

        [HttpPost]
        public async Task<ActionResult> EditBid(int id, int itemId, int volunteerId, int startingPrice, int currentPrice, DateTime startTime, DateTime endTime)
        {
            try
            {
                await _bidService.EditBid(id, itemId, volunteerId, startingPrice, currentPrice, startTime, endTime);
                return RedirectToAction("ShowBids", "Admin");
            }
            catch
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { message = ConstantStrings.notAllFieldsCompleted });
            }
        }


    }
}