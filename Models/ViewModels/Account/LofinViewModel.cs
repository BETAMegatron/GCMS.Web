using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفاً نام کاربری را وارد کنید")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید")]
        public string Password { get; set; } = string.Empty;
    }
}