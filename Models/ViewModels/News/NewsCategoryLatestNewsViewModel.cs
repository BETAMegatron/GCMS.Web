using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.ViewModels.News
{
    public class NewsCategoryLatestNewsViewModel
    {
        public int NewsID { get; set; }

        [Display(Name = "عنوان خبر")]
        [MaxLength(50)]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string NewsTitle { get; set; } = string.Empty;
        [Display(Name = "تاریخ انتشار")]
        public DateTime? NewsPublishedDate { get; set; }
        public int NewsCategoryID { get; set; }
        public string? NewsImageName { get; set; }
    }
}
