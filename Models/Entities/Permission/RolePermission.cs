using GCMS.Web.Models.Entities.Roles;

namespace GCMS.Web.Models.Entities.Permission
{
    public class RolePermission
    {
        public int RolePermissionID { get; set; }
        public bool Deleted { get; set; } = false;
        public bool RolePermissionIsActived { get; set; } = true;
        public DateTime RolePermissionCreationDate { get; set; } = DateTime.UtcNow;

        public int RoleID { get; set; }
        public Role role { get; set; } = null!;
        public int PermissionID { get; set; }
        public Permission permission { get; set; } = null!;
    }
}
