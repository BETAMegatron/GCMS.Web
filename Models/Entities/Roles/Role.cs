using GCMS.Web.Models.Entities.Permission;
using GCMS.Web.Models.Entities.Users;

namespace GCMS.Web.Models.Entities.Roles
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleIsActived { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime RoleCreationDate { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole> userroles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> rolepermissions { get; set; } = new List<RolePermission>();
    }
}
