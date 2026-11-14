
using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.News
{
    public class NewsCategoryViewModel
    {
        public int NewsCategoryID { get; set; }

        [Display(Name = "عنوان دسته‌بندی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "عنوان دسته‌بندی نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد")]
        public string NewsCategoryTitle { get; set; } = string.Empty;

        [Display(Name = "توضیحات دسته‌بندی")]
        [MaxLength(500, ErrorMessage = "توضیحات نمی‌تواند بیشتر از ۵۰۰ کاراکتر باشد")]
        public string? NewsCategoryDescription { get; set; }

        [Display(Name = "تعداد اخبار")]
        public int NewsQuantitiy { get; set; }

        [Display(Name = "تصویر دسته‌بندی")]
        public string? NewsCategoryImageName { get; set; }

        [Display(Name = "انتخاب تصویر دسته‌بندی")]
        public IFormFile? NewsCategoryImage { get; set; }

        [Display(Name = "وضعیت دسته‌بندی")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime NewsCategoryCreationDate { get; set; } = DateTime.UtcNow;
    }
}
