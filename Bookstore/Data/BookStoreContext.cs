using Bookstore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>

    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }

        public DbSet<Books>  Books { get; set; }

        public DbSet<Language> Language { get; set; }

        public DbSet<BookGallery> BookGallary { get; set; }
    }
}
