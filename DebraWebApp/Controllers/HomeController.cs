using DebraWebApp.Models;
using DebraWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DebraWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SellService _sellService;
        private readonly EventService _eventService;
        private readonly HomeService _homeService;
        private readonly PartnerService _partnerService;

        public HomeController(
            ILogger<HomeController> logger, SellService sellService, EventService eventService, HomeService homeService, PartnerService partnerService)
        {
            _logger = logger;
            _sellService = sellService;
            _eventService = eventService;
            _homeService = homeService;
            _partnerService = partnerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            try
            {
                var partners = await _sellService.GetPartnersAsync();
                ViewBag.Partners = partners.Select(p => new { p.Id, p.Name }).ToList();

                var events = await _homeService.GetAllEventsAsync();
                ViewBag.Events = events.Select(e => new { e.EventId, e.Location }).ToList();

                var sell = await _sellService.GetAllSellAsync();
                ViewBag.Sell = sell;

                return View();
            }
            catch (Exception ex)
            {
                return View(new List<Admin>());
            }
        }

        public async Task<IActionResult> GetSalesByEvent(int eventID)
        {
            try
            {
                var eSales = await _homeService.GetSalesByEventAsync(eventID);
                return PartialView("_SalesByEventPartial", eSales);
            }
            catch (Exception ex)
            {
                return PartialView("_SalesByEventPartial", new List<Admin>());
            }
        }

        public async Task<IActionResult> GetEarningsByPartner(int partnerId)
        {
            try
            {
                var earnings = await _homeService.GetEarningsByPartnerAsync(partnerId);
                return Json(earnings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching earnings by partner ID {PartnerId}", partnerId);
                return Json(0m);
            }
        }
        public async Task<IActionResult> EarningsByEvent(int eventId)
        {
            try
            {
                var earnings2 = await _homeService.GetEarningsByEventAsync(eventId);
                return Json(earnings2);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching earnings by partner ID {PartnerId}", eventId);
                return Json(0m);
            }
        }

        public async Task<IActionResult> GetSalesByPartner(int partnerId)
        {
            try
            {
                var sales = await _sellService.GetSalesByPartnerAsync(partnerId);
                return PartialView("_SalesByPartnerPartial", sales);
            }
            catch (Exception ex)
            {
                return PartialView("_SalesByPartnerPartial", new List<Admin>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminLogin(AdminLogin request)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _homeService.AuthenticatePartnerAsync(request.Username, request.Password);
                if (isSuccess)
                {
                    return RedirectToAction("Admin");
                }
                ViewBag.ErrorMessage = errorMessage ?? "Invalid login attempt.";
            }
            return View(request);
        }


    }
}
