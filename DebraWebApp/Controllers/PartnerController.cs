using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DebraWebApp.Models;
using DebraWebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace DebraWebApp.Controllers
{
    public class PartnerController : Controller
    {
        private readonly PartnerService _partnerService;
        private readonly SellService _sellServise;
        private readonly EventService _eventService;
        private readonly TicketService _ticketService;

        public PartnerController(PartnerService partnerService, SellService sellServise, EventService eventService, TicketService ticketService)
        {
            _partnerService = partnerService;
            _sellServise = sellServise;
            _eventService = eventService;
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index(string email)
        {
            var partner = await _partnerService.GetPartnerAsync(email);

            if (partner == null)
            {
                return NotFound();
            }

            int partnerId = partner.Id;

            var managedEvents = await _eventService.GetEventsByPartnerAsync(partnerId);
            var recentSales = await _sellServise.GetSalesBySPartnerAsync(partnerId);

            var eventIds = managedEvents.Select(e => e.EventId).ToList();

            var tickets = new List<Ticket>();
            foreach (var eventId in eventIds)
            {
                var eventTickets = await _ticketService.GetTicketsByEvent(eventId);
                tickets.AddRange(eventTickets);
            }

            var partnerDashboard = new PartnerDashboard
            {
                Partner = partner,
                ManagedEvents = managedEvents,
                RecentSales = recentSales,
                Tickets = tickets
            };

            return View(partnerDashboard);
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
                TempData["SuccessMessage"] = "Registration successfully.";
                return RedirectToAction("Create");
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
            var partner2 = await _partnerService.GetPartnerAsync(id);
            string Email = partner2.Email;
            return RedirectToAction(nameof(Index), new { email = partner2.Email });
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(PartnerLogin request)
        {
            if (ModelState.IsValid)
            {
                var (isSuccess, errorMessage) = await _partnerService.AuthenticatePartnerAsync(request.Email, request.Password);
                if (isSuccess)
                {
                    return RedirectToAction("Index", new { email = request.Email });
                }
                ViewBag.ErrorMessage = errorMessage ?? "Invalid login attempt.";
            }
            return View(request);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
