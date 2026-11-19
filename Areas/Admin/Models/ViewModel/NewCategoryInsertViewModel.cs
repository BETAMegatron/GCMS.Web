using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Areas.Admin.Models.ViewModel
{
    public class NewCategoryInsertViewModel
    {
        public int NewsCategoryID { get; set; }

        [Required(ErrorMessage = "عنوان دسته‌بندی را وارد کنید")]
        public string NewsCategoryTitle { get; set; } = string.Empty;

        public string? NewsCategoryDescription { get; set; }

        public string? NewsCategoryImageName { get; set; }

        public IFormFile? NewsCategoryImage { get; set; }

        public bool IsActive { get; set; }

        public DateTime NewsCategoryCreationDate { get; set; }

        public string? NewsCategoryCreationDateToShamsi { get; set; }
    }
}