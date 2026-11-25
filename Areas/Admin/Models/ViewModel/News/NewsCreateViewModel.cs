using GCMS.Web.Models.Entities.News;
using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Areas.Admin.Models.ViewModel.News
{
    public class NewsCreateViewModel
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
        public string NewsVideoName { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
