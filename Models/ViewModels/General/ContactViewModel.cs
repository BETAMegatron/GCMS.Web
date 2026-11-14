using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.General
{
    public class ContactViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست")]
        public string Email { get; set; } = string.Empty;


        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره باید دقیقاً ۱۱ رقم و فقط شامل عدد باشد")]
        public string Mobile { get; set; } = string.Empty;


        [Display(Name = "تلفن ثابت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره باید دقیقاً ۱۱ رقم و فقط شامل عدد باشد")]
        public string Phone { get; set; } = string.Empty;


        [Display(Name = "آدرس شرکت")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Address { get; set; } = string.Empty;


        [Display(Name = "شناسه تلگرام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string TelegramID { get; set; } = string.Empty;


        [Display(Name = "شناسه اینستاگرام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string InstagramID { get; set; } = string.Empty;


        [Display(Name = "شماره واتساپ")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "شماره باید دقیقاً ۱۱ رقم و فقط شامل عدد باشد")]
        public string WahtsApp { get; set; } = string.Empty;
    }
}