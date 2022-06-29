using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Services.EntityFramework.Entities
{
    [Table("UserRoles", Schema = "Identity")]
    public class UserRoleDTO
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public UserDTO User { get; set; }
        public RoleDTO Role { get; set; }
    }
}
