namespace Events.Services.Entities
{
    public class Organizer
    {
        public string Name { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
