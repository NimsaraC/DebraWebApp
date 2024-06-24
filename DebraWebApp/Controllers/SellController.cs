using System.Threading.Tasks;
using DebraWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DebraWebApp.Controllers
{
    public class SellController : Controller
    {
        private readonly SellService _sellService;

        public SellController(SellService sellService)
        {
            _sellService = sellService;
        }

        public async Task<IActionResult> Index(int? partnerId)
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
                    return View(new List<Sell>());
                }
            }
            catch (Exception ex)
            {
                return View(new List<Sell>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var sell = await _sellService.GetSellAsync(id);
            if (sell == null)
            {
                return NotFound();
            }
            return View(sell);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sell sell)
        {
            if (ModelState.IsValid)
            {
                await _sellService.CreateSellAsync(sell);
                return RedirectToAction(nameof(Index));
            }
            return View(sell);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sell = await _sellService.GetSellAsync(id);
            if (sell == null)
            {
                return NotFound();
            }
            return View(sell);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Sell sell)
        {
            if (id != sell.SellId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _sellService.UpdateSellAsync(id, sell);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var sell = await _sellService.GetSellAsync(id);
            if (sell == null)
            {
                return NotFound();
            }
            return View(sell);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sellService.DeleteSellAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("partner/{partnerId}")]
        public async Task<IActionResult> SalesByPartner(int partnerId)
        {
            var sales = await _sellService.GetSalesByPartnerAsync(partnerId);
            return View(sales);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> SalesByEvent(int eventId)
        {
            var sales = await _sellService.GetSalesByEventAsync(eventId);
            return View(sales);
        }

        [HttpGet("earnings/event/{eventId}")]
        public async Task<IActionResult> EarningsByEvent(int eventId)
        {
            var earnings = await _sellService.GetEarningsByEventAsync(eventId);
            return View(earnings);
        }

        [HttpGet("earnings/partner/{partnerId}")]
        public async Task<IActionResult> EarningsByPartner(int partnerId)
        {
            var earnings = await _sellService.GetEarningsByPartnerAsync(partnerId);
            return View(earnings);
        }
    }
}