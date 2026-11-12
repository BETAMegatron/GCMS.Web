namespace GCMS.Web.Models.Entities.Permission
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public bool PermissionIsActived { get; set; } = true;
        public bool deleted { get; set; } = false;
        public DateTime PermissionCreationDate { get; set; } = DateTime.UtcNow;
        public ICollection<RolePermission> rolepermissions { get; set; } = new List<RolePermission>();
    }
}
