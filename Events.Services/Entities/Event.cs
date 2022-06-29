namespace Events.Services.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Plan { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? OrganizerId { get; set; }
        public string? SpeakerId { get; set; }
        public User? Organizer{ get; set; }
        public User? Speaker { get; set; }
    }
}
