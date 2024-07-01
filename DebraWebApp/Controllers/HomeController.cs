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

        public HomeController(
            ILogger<HomeController> logger,SellService sellService,EventService eventService,HomeService homeService)
        {
            _logger = logger;
            _sellService = sellService;
            _eventService = eventService;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Admin(int? partnerId, int? eventID)
        {
            try
            {
                var partners = await _sellService.GetPartnersAsync();
                ViewBag.Partners = partners.Select(p => new { p.Id, p.Name }).ToList();

                var events = await _homeService.GetAllEventsAsync();
                ViewBag.Events = events.Select(e => new { e.EventId, e.Location }).ToList();

                var sell = await _sellService.GetAllSellAsync();
                ViewBag.Sell = sell;

                if (eventID.HasValue)
                {
                    var eSales = await _homeService.GetSalesByEventAsync(eventID.Value);
                    return View(eSales);
                }

                if (partnerId.HasValue)
                {
                    var sales = await _sellService.GetSalesByPartnerAsync(partnerId.Value);
                    return View(sales);
                }

                return View(sell);
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return View(new List<Admin>());
            }
        }



        /*public async Task<IActionResult> Admin(int? partnerId)
        {
            try
            {
                var partners = await _sellService.GetPartnersAsync();
                ViewBag.Partners = partners.Select(p => new { p.Id, p.Name }).ToList();

                if (partnerId.HasValue)
                {
                    var sales = await _sellService.GetSalesByPartnerAsync(partnerId.Value);
                    return View(sales);
                }
                else
                {
                    return View(new List<Admin>());
                }
            }
            catch (Exception ex)
            {
                return View(new List<Admin>());
            }
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
