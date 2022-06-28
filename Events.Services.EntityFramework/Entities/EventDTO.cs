using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Services.EntityFramework.Entities
{
    [Table("event")]
    public class EventDTO
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string? Title { get; set; }

        [Column(TypeName = "ntext")]
        public string? Description { get; set; }

        [Column(TypeName = "ntext")]
        public string? Plan { get; set; }

        [Column(name: "event_date_time", TypeName = "datetime")]
        public DateTime? EventDateTime { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar")]
        public string? City { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string? Address { get; set; }

        [ForeignKey("Organizer")]
        [Column(name: "organizer_id")]
        public int? OrganizerId { get; set; }

        [ForeignKey("Speaker")]
        [Column(name: "speaker_id")]
        public int? SpeakerId { get; set; }
        
        public virtual OrganizerDTO? Organizer { get; set; }
        public virtual SpeakerDTO? Speaker { get; set; }
    }
}
