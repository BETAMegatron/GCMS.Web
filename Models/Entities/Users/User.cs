using GCMS.Web.Models.Entities.Roles;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GCMS.Web.Models.Entities.Users
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }   
        public string Password { get; set; }
        public string UserProfileImageName { get; set; }
        public bool UserIsActived { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime UserCreationDate { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
