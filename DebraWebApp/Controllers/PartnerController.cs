using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DebraWebApp.Models;
using DebraWebApp.Services;
using Microsoft.AspNetCore.Identity;

namespace DebraWebApp.Controllers
{
    public class PartnerController : Controller
    {
        private readonly PartnerService _partnerService;

        public PartnerController(PartnerService partnerService)
        {
            _partnerService = partnerService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
            
        }
        public async Task<IActionResult> PartnerAdmin()
        {
            var partners = await _partnerService.GetPartnersAsync();
            return View(partners);
        }

        public async Task<IActionResult> Details(int id)
        {
            var partner = await _partnerService.GetPartnerAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        public async Task<IActionResult> DetailsUI(string email)
        {
            var partner = await _partnerService.GetPartnerAsync(email);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        [HttpGet("Partner/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Partner/create")]
        public async Task<IActionResult> Create(Partner partner)
        {
            if (ModelState.IsValid)
            {
                var newPartner = await _partnerService.RegisterPartnerAsync(partner);
                return RedirectToAction(nameof(Details), new { id = newPartner.Id });
            }
            return View(partner);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _partnerService.GetPartnerAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Partner partner)
        {
            if (id != partner.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _partnerService.UpdatePartnerAsync(id, partner);
            return RedirectToAction(nameof(Details), new { id = partner.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _partnerService.GetPartnerAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _partnerService.DeletePartnerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
