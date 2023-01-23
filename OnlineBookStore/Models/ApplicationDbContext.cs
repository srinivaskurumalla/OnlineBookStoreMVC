using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineBookStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("MyCon")
        {

        }

        //Book Details
        public DbSet<Product> Books { get; set; }

        //Book Types
        public DbSet<BookType> BookTypes { get; set; }


        //For Authentication 
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRolesMapping> UserRolesMappings { get; set; }

    }
}