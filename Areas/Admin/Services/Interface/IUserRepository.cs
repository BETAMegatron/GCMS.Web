using GCMS.Web.Models.Entities.Users;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GCMS.Web.Areas.Admin.Services.Interface
{
    public interface IUserRepository
    {
        public List<User> GetAllUsers();
        public User GetUserByID(int userID);
        public void InsertUser(User user);
        public void DeleteUser(User user);
        public void UpdateUser(User user);
        public bool UserDeactive(int userID);
        public bool UserActive (int userID);
        public bool UserExist(int userID);
        public void Save();
        public Task SaveInAsync();
    }
}
