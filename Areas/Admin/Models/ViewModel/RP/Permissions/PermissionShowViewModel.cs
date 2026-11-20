using GCMS.Web.Models.Entities.Permission;

namespace GCMS.Web.Areas.Admin.Models.ViewModel.RP.Permissions
{
    public class PermissionShowViewModel
    {
            public int PermissionID { get; set; }
            public string PermissionName { get; set; }
            public bool PermissionIsActived { get; set; } = true;
            public bool deleted { get; set; } = false;
            public DateTime PermissionCreationDate { get; set; } = DateTime.UtcNow;
            public string? PermissionCreationDateToShamsi { get; set; }

    }
}
