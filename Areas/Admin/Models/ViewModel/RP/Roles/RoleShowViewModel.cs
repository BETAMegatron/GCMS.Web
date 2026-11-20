using GCMS.Web.Models.Entities.Permission;
using GCMS.Web.Models.Entities.Roles;

namespace GCMS.Web.Areas.Admin.Models.ViewModel.RP.Roles
{
    public class RoleShowViewModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleIsActived { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime RoleCreationDate { get; set; } = DateTime.UtcNow;
        public string? RoleCreationDateToShamsi { get; set; } 
    }
}
