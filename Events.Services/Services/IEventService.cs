using Events.Services.Entities;

namespace Events.Services.Services
{
    public interface IEventService
    {
        IAsyncEnumerable<Event> GetAllEvents();
        Task<Event> GetById(int id);
        Task<Event> Create(Event e);
        Task<Event> Update(int id, Event e);
        Task<Event> Delete(int id);

    }
}
