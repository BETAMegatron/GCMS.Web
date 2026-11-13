
using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
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

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفاً تکرار رمز عبور را وارد کنید")]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیستند")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "تصویر پروفایل")]
        public IFormFile UserProfileImage { get; set; }

        [Display(Name = "موافقت با قوانین سایت")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "برای ثبت‌نام باید با قوانین سایت موافقت کنید")]
        public bool AcceptTerms { get; set; }
    }
}
