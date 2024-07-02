using DebraWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DebraWebApp.Controllers
{
    [Route("events")]
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return View(eventModel);

        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Event eventModel)
        {
            if (ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(eventModel);
                TempData["SuccessMessage"] = "Event saved successfully.";
                return RedirectToAction("Create");
            }
            return View(eventModel);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return View(eventModel);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(int id, Event eventModel)
        {
            if (id != eventModel.EventId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventService.UpdateEventAsync(id, eventModel);
            TempData["SuccessMessage"] = "Event saved successfully.";

            return RedirectToAction("Edit", new { id = id });
        }


        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return View(eventModel);
        }

        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("partner/{partnerId}")]
        public async Task<IActionResult> EventsByPartner(int partnerId)
        {
            var events = await _eventService.GetEventsByPartnerAsync(partnerId);
            return View(events);
        }
    }
}
