using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Services.EntityFramework.Entities
{
    [Table("User", Schema = "Identity")]
    public class UserDTO
    {
        [Key]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public virtual ICollection<RoleDTO>? Roles { get; set; }
    }
}