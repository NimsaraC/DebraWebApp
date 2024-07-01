using DebraWebApp.Models;
using DebraWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace DebraWebApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IActionResult> Index2(int eventId)
        {
            var tickets = await _ticketService.GetTicketsByEvent(eventId);
            return View(tickets);
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTickets();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketDTO createTicketDTO)
        {
            if (ModelState.IsValid)
            {
                var ticket = await _ticketService.SetTicketDetails(createTicketDTO);
                return RedirectToAction(nameof(Details), new { id = ticket.TicketId });
            }
            return View(createTicketDTO);
        }
    }
}
