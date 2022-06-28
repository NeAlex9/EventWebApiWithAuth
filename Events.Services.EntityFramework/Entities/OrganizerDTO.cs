using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Events.Services.Entities;

namespace Events.Services.EntityFramework.Entities
{
    [Table("organizer")]
    public class OrganizerDTO
    {
        public OrganizerDTO()
        {
            Events = new HashSet<EventDTO>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }

        [InverseProperty("Organizer")]
        public virtual ICollection<EventDTO>? Events { get; set; }
    }
}
