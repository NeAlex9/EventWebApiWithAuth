namespace Events.Services.Entities
{
    public class Speaker
    {
        public string Name { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
