
using System;

namespace GCMS.Web.Areas.Admin.Models.ViewModel
{
    public class NewsCategoryShowViewModel
    {
        public int NewsCategoryID { get; set; }
        public string NewsCategoryTitle { get; set; } = string.Empty;
        public string? NewsCategoryDescription { get; set; }
        public string? NewsCategoryImageName { get; set; }
        public int NewsQuantitiy { get; set; }
        public bool IsActive { get; set; }
        public DateTime NewsCategoryCreationDate { get; set; }
        public string NewsCategoryCreationDateToShamsi { get; set; }
    }
}
