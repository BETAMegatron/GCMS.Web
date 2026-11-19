using GCMS.Web.Models.Entities.Roles;
using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Areas.Admin.Models.ViewModel.User
{
    public class UserShowViewModel
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserProfileImageName { get; set; }
        public IFormFile? UserProfileImage { get; set; }
        public bool UserIsActived { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime UserCreationDate { get; set; } = DateTime.UtcNow;
        public string UserCreationDateToShamsi { get; set; }

    }
}
