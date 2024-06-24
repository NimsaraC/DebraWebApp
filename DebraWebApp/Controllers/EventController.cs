using DebraWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DebraWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return View(eventModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event eventModel)
        {
            if (ModelState.IsValid)
            {
                await _eventService.CreateEventAsync(eventModel);
                return RedirectToAction(nameof(Index));
            }
            return View(eventModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return View(eventModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event eventModel)
        {
            if (id != eventModel.EventId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            await _eventService.UpdateEventAsync(id, eventModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var eventModel = await _eventService.GetEventAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return View(eventModel);
        }

        [HttpPost, ActionName("Delete")]
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
