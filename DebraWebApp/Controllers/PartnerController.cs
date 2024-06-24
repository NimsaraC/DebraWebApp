// Controllers/PartnerController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using DebraWebApp.Models;
using DebraWebApp.Services;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Partner partner)
        {
            if (ModelState.IsValid)
            {
                var newPartner = await _partnerService.RegisterPartnerAsync(partner);
                return RedirectToAction(nameof(Details), new { id = newPartner.Id });
            }
            return View(partner);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var partner = await _partnerService.GetPartnerAsync(id);
            if (partner == null)
            {
                return NotFound();
            }
            return View(partner);
        }

        [HttpPost]
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
