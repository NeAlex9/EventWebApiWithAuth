using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Services.EntityFramework.Entities
{
    [Table("speaker")]
    public class SpeakerDTO
    {
        public SpeakerDTO()
        {
            Events = new HashSet<EventDTO>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar")]
        public string? Name { get; set; }

        [InverseProperty("Speaker")]
        public virtual ICollection<EventDTO>? Events { get; set; }
    }
}
