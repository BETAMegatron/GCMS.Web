using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.Entities.News
{
    public class NewsCategory
    {
        [Key]
        public int NewsCategoryID { get; set; }

        [Display(Name = "عنوان دسته‌بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100)]
        public string NewsCategoryTitle { get; set; } = string.Empty;

        [Display(Name = "توضیحات دسته‌بندی")]
        public string? NewsCategoryDescription { get; set; }

        public bool Deleted { get; set; } = false;

        public bool IsActive { get; set; } = true;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime NewsCategoryCreationDate { get; set; } = DateTime.UtcNow;

        // Navigation Property
        public ICollection<News> News { get; set; } = new List<News>();
    }
}