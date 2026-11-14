using System.ComponentModel.DataAnnotations;

namespace GCMS.Web.Models.Entities.General
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }    
        public string AboutShortDescription { get; set; }
        public string AboutLongDescription { get; set; }
        public string PictureName { get; set; }
    }
}
