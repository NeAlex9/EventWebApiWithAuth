namespace Events.Services.Entities
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
