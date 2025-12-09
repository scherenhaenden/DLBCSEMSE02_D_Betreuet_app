using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.DatabaseAccess.Entities
{
    [Table("UserRoles")]
    public class UserRoleDataAccessModel
    {
        public Guid UserId { get; set; }
        public UserDataAccessModel User { get; set; }

        public Guid RoleId { get; set; }
        public RoleDataAccessModel Role { get; set; }
    }
}
