namespace Events.Services.Entities
{
    public class Event
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Plan { get; set; }
        public DateTime EventDateTime { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public IEnumerable<Organizer> Organizers { get; set; }
        public IEnumerable<Speacker> Speakers { get; set; }
    }
}
