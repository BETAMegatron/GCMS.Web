namespace GCMS.Web.Areas.Admin.Models.ViewModel.User
{
    public class UserEditByAdminViewModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? UserProfileImageName { get; set; }
        public IFormFile? UserProfileImage { get; set; }
        public bool UserIsActived { get; set; } = true;
        public DateTime UserCreationDate { get; set; }
        public string? UserCreationDateToShamsi { get; set; }
    }

}
