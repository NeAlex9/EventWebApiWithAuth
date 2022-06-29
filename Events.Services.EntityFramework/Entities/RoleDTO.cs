using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Services.EntityFramework.Entities
{
    [Table("Role", Schema = "Identity")]
    public class RoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserDTO> Users { get; set; }
    }
}
