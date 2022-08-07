
using OnlineResturnatManagement.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace OnlineResturnatManagement.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        //: base(options)
        //{
        //}
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }
        public DbSet<NavigationMenu> NavigationMenu { get; set; }
        public DbSet<ActiveModule> ActiveModules { get; set; }
        public DbSet<CompanyProfile> CompanyProfiles { get; set; }

    }
}