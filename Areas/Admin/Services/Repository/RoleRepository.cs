using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Data.Context;
using GCMS.Web.Models.Entities.Roles;
using Microsoft.EntityFrameworkCore;

namespace GCMS.Web.Areas.Admin.Services.Repository
{
    public class RoleRepository : IRoloeRepository
    {
        private readonly GCMSContext _db;
        public RoleRepository(GCMSContext GCMSContext)
        {
            _db = GCMSContext;
        }
        public void DeleteRole(Role role)
        {
            role.Deleted = true;
        }

        public List<Role> GetAllRoles()
        {
            return _db.Roles.Where(r => !r.Deleted).ToList();
        }

        public Role GetRoleByID(int roleID)
        {
            return _db.Roles.FirstOrDefault(r => !r.Deleted && r.RoleID == roleID);
        }

        public void InsertRole(Role role)
        {
            _db.Roles.Add(role);
        }

        public bool RoleActivation(int roleID)
        {
            var rl = GetRoleByID(roleID);
            rl.RoleIsActived = true;
            return true;
        }

        public bool RoleDeactivation(int roleID)
        {
            var rl = GetRoleByID(roleID);
            rl.RoleIsActived = false;
            return false;
        }

        public bool RoleExist(int roleID)
        {
            return _db.Users.Any(r => !r.Deleted && r.UserID == roleID);
        }

        public void UpdateRole(Role role)
        {
            _db.Entry(role).State = EntityState.Modified;
        }

        public void  Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveInAsync()
        {
           await _db.SaveChangesAsync();
        }
    }
}
