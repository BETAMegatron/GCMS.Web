using GCMS.Web.Models.Entities.News;
using GCMS.Web.Models.Entities.Permission;
using GCMS.Web.Models.Entities.Roles;
using GCMS.Web.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace GCMS.Web.Data.Context
{
    public class GCMSContext : DbContext
    {
        public GCMSContext(DbContextOptions<GCMSContext> op) : base(op)
        {
        }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasIndex( ur => new { ur.UserID,ur.RoleID }).IsUnique();
            modelBuilder.Entity<RolePermission>().HasIndex(rp => new { rp.PermissionID, rp.RoleID }).IsUnique();

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            // modelBuilder.Entity<NewsCategory>().HasIndex(cn => cn.NewsCategoryTitle).IsUnique().HasFilter("[Deleted] = 0");
            //modelBuilder.Entity<News>().HasOne(n => n.NewsCategory).WithMany(n => n.News).HasForeignKey(n => n.NewsCategoryID);
           //modelBuilder.Entity<User>().HasMany(u => u.UserRoles).WithOne(u => u.user).HasForeignKey(u => u.UserID);

            /*
HasOne
WithMany
HasForeignKey
             */


        }
    }
}
