using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.Entities.News
{
    public class News
    {
        [Key]
        public int NewsID { get; set; }

        [Display(Name = "عنوان خبر")]
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string NewsTitle { get; set; } = string.Empty;

        [Display(Name = "توضیحات خبر")]
        [MaxLength(500)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string NewsDescription { get; set; } = string.Empty;

        [Display(Name = "تاریخ ایجاد")]
        public DateTime NewsCreationDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "تاریخ ویرایش")]
        public DateTime? NewsModifiedDate { get; set; }

        [Display(Name = "تاریخ انتشار")]
        public DateTime? NewsPublishedDate { get; set; }

        [Display(Name = "آیا خبر منتشر شده است؟")]
        public bool NewsIsPublished { get; set; } = true;

        public string NewsImageName { get; set; }
        public string NewsVideoName { get; set; }

        public bool Deleted { get; set; } = false;

        // Foreign Key
        public int NewsCategoryID { get; set; }

        // Navigation Property
        public NewsCategory NewsCategory { get; set; } = null!;
    }
}