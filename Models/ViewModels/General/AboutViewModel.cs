using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.General
{
    public class AboutViewModel
    {
        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفاً توضیحات کوتاه را وارد کنید")]
        [MaxLength(300, ErrorMessage = "توضیحات کوتاه نمی‌تواند بیشتر از ۳۰۰ کاراکتر باشد")]
        public string AboutShortDescription { get; set; } = string.Empty;

        [Display(Name = "توضیحات کامل")]
        [Required(ErrorMessage = "لطفاً توضیحات کامل را وارد کنید")]
        public string AboutLongDescription { get; set; } = string.Empty;

        [Display(Name = "تصویر درباره ما")]
        public string? PictureName { get; set; }

        public IFormFile? PictureFile { get; set; }
    }
}