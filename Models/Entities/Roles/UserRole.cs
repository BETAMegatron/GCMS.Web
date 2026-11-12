using GCMS.Web.Models.Entities.Users;

namespace GCMS.Web.Models.Entities.Roles
{
    public class UserRole
    {
        public int UserRoleID { get; set; }
        public bool Deleted { get; set; } = false;
        public bool UserRoleIsActivated { get; set; } = true;
        public DateTime UserRoleCreationDate { get; set; } = DateTime.UtcNow;

        public int UserID { get; set; } 
        public int RoleID { get; set; }
        public User user { get; set; } = null!;
        public Role role { get; set; } = null!;

    }
}
