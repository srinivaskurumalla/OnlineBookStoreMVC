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

        public DbSet<Product> Books { get; set; }

        public DbSet<BookType> BookTypes { get; set; }

    }
}