using System.ComponentModel.DataAnnotations;
namespace GCMS.Web.Models.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "رمز عبور فعلی را وارد کنید")]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "رمز عبور جدید را وارد کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور جدید باید حداقل ۶ کاراکتر باشد")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "تکرار رمز عبور جدید را وارد کنید")]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تکرار آن یکسان نیستند")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}