namespace EventsWebApi.Models
{
    public class EventToUpdate
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Plan { get; set; }
        public DateTime EventDateTime { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int? OrganizerId { get; set; }
        public int? SpeakerId { get; set; }
    }
}
