using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace GCMS.Web.Areas.Admin.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GCMSContext _db;
        public UserRepository(GCMSContext GCMSContext)
        {
            _db = GCMSContext;
        }
        public void DeleteUser(User user)
        {
            user.Deleted = true;
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.Where(u => !u.Deleted).ToList();
        }

        public User GetUserByID(int userID)
        {
            return _db.Users.FirstOrDefault(u => u.UserID == userID && !u.Deleted);
        }

        public void InsertUser(User user)
        {
            _db.Users.Add(user);
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveInAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void UpdateUser(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
        }

        public bool UserActive(int userID)
        {
            var usr = GetUserByID(userID);
            usr.UserIsActived = true;
            return true;
        }

        public bool UserDeactive(int userID)
        {
            var usr = GetUserByID(userID);
            usr.UserIsActived = false;
            return false;
        }

        public bool UserExist(int userID)
        {
            return _db.Users.Any(u => u.UserID == userID);
        }
    }
}
