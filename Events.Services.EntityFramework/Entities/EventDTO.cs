using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Services.EntityFramework.Entities
{
    public class EventDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar")]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar")]
        public string Plan { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime EventDateTime { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar")]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string Address { get; set; }

        [ForeignKey("Organizer")]
        public int? OrganizerId { get; set; }

        [ForeignKey("Speaker")]
        public int? SpeakerId { get; set; }
        
        public virtual OrganizerDTO Organizer { get; set; }
        public virtual SpeakerDTO Speaker { get; set; }
    }
}
