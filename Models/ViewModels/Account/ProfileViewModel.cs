
using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.Account
{
    public class ProfileViewModel
    {
        public int UserID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً نام را وارد کنید")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً نام خانوادگی را وارد کنید")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفاً نام کاربری را وارد کنید")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "تصویر پروفایل")]
        public IFormFile? UserProfileImage { get; set; }

        public string? UserProfileImageName { get; set; }

        public DateTime UserCreationDate { get; set; }

        public bool UserIsActived { get; set; }
    }
}
