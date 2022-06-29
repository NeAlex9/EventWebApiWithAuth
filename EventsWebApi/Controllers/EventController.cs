using AutoMapper;
using Events.Services.Entities;
using Events.Services.Services;
using EventsWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Authorize(Roles = "speaker")]
        [HttpGet]
        public IAsyncEnumerable<Event> GetAllEvents() =>
            _eventService.GetAllEventsAsync();

        [Authorize(Roles = "organizer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            var e = await _eventService.GetByIdAsync(id);
            return e is null ? NotFound() : e;
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(EventToCreate eventToCreate)
        {
            var e = _mapper.Map<Event>(eventToCreate);
            var result = await _eventService.CreateAsync(e);
            if (result < 0)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetEventById), new {id = result}, await _eventService.GetByIdAsync(result));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventService.DeleteAsync(id);
            return result ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Event>> UpdateEvent(int id, EventToUpdate eventToUpdate)
        {
            var e = _mapper.Map<Event>(eventToUpdate);
            var result = await _eventService.UpdateAsync(id, e);
            return result ? Ok() : BadRequest();
        }
    }
}
