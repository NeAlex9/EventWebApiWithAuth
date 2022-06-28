using Events.Services.Entities;

namespace Events.Services.Services
{
    public interface IEventService
    {
        IAsyncEnumerable<Event> GetAllEventsAsync();
        Task<Event?> GetByIdAsync(int id);
        Task<int> CreateAsync(Event e);
        Task<bool> UpdateAsync(int id, Event e);
        Task<bool> DeleteAsync(int id);

    }
}
