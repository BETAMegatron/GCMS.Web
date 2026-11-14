using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GCMS.Web.Models.Entities.General
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TelegramID { get; set; }
        public string InstagramID { get; set; }
        public string WahtsApp {  get; set; }
    }
}
