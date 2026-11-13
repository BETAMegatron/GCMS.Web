
using GCMS.Web.Models.Entities.Roles;
using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.Entities.Users
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "تصویر پروفایل")]
        public string UserProfileImageName { get; set; }

        public bool UserIsActived { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime UserCreationDate { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
