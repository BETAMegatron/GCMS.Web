namespace GCMS.Web.Areas.Admin.Models.ViewModel.News
{
    public class NewsShowViewModel
    {
        public int NewsID { get; set; }
        public string NewsTitle { get; set; } = string.Empty;
        public string NewsDescription { get; set; } = string.Empty;
        public DateTime NewsCreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? NewsModifiedDate { get; set; }
        public DateTime? NewsPublishedDate { get; set; }
        public bool NewsIsPublished { get; set; } = true;
        public string NewsImageName { get; set; }
        public IFormFile NewsImage { get; set; }
        public IFormFile NewsVideo { get; set; }
        public string NewsCreationDateToShamsi { get; set; }
        public string NewsCategoryName { get; set; } = string.Empty;
        public string NewsVideoName { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
