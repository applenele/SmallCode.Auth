using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.Auth.Models
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<User> Users { set; get; }

        public DbSet<Department> Departments { set; get; }

        public DbSet<Role> Roles { set; get; }

        public DbSet<UserRole> UserRoles { set; get; }

        public DbSet<Function> Functions { set; get; }

        public DbSet<RoleFunction> RoleFunctions { set; get; }

        public DbSet<Log> Logs { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            base.OnModelCreating(modelBuilder);
        }
    }
}
