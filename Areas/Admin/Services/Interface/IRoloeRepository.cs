using GCMS.Web.Models.Entities.Roles;
using System.Security.Cryptography;

namespace GCMS.Web.Areas.Admin.Services.Interface
{
    public interface IRoloeRepository
    {
        public List<Role> GetAllRoles();
        public Role GetRoleByID(int roleID);
        public void InsertRole(Role role);
        public void DeleteRole(Role role);
        public void UpdateRole(Role role);
        public bool RoleDeactivation(int roleID);
        public bool RoleActivation(int roleID);
        public bool RoleExist(int roleID);
        public void Save();
        public Task SaveInAsync();
    }
}
