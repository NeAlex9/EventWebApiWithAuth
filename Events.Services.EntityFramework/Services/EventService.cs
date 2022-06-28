using AutoMapper;
using Events.Services.Entities;
using Events.Services.EntityFramework.Context;
using Events.Services.EntityFramework.Entities;
using Events.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Events.Services.EntityFramework.Services
{
    public class EventService : IEventService

    {
        private readonly EventContext _context;
        private readonly IMapper _mapper;

        public EventService(EventContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IAsyncEnumerable<Event> GetAllEventsAsync() =>
            _context.Events
                .AsNoTracking()
                .Include(e => e.Organizer)
                .Include(e => e.Speaker)
                .Select(e => _mapper.Map<Event>(e))
                .AsAsyncEnumerable();

        public async Task<Event?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} should be greater then 0");
            }

            var dto = await _context.Events
                .AsNoTracking()
                .Include(e => e.Organizer)
                .Include(e => e.Speaker)
                .FirstOrDefaultAsync(e => e.Id == id);

            return dto is null ? null : _mapper.Map<Event>(dto);
        }

        public async Task<int> CreateAsync(Event e) 
        {
            ArgumentNullException.ThrowIfNull(e);
            var dto = _mapper.Map<EventDTO>(e);
            await _context.Events.AddAsync(dto);
            await _context.SaveChangesAsync();
            return dto.Id;
        }

        public async Task<bool> UpdateAsync(int id, Event e)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} should be greater then 0");
            }

            ArgumentNullException.ThrowIfNull(e);
            var dto = await _context
                .Events
                .FindAsync(id);

            if (dto is null)
            {
                return false;
            }

            Update(e, dto);
            var updatedRows = await _context.SaveChangesAsync();
            return updatedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"{nameof(id)} should be greater then 0");
            }

            var dto = await _context
                .Events
                .FindAsync(id);

            if (dto is null)
            {
                return true;
            }

            var updatedRows = await _context.SaveChangesAsync();
            return updatedRows > 0;
        }

        private void Update(Event e, EventDTO dto)
        {
            dto.OrganizerId = e.OrganizerId;
            dto.SpeakerId = e.SpeakerId;
            dto.Address = e.Address;
            dto.City = e.City;
            dto.Description = e.Description;
            dto.Plan = e.Plan;
            dto.Title = e.Title;
            dto.EventDateTime = e.EventDateTime;
        }
    }
}
