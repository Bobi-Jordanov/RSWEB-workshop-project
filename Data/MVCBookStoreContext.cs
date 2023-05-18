using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCBookStore.Areas.Identity.Data;
using MVCBookStore.Models;

namespace MVCBookStore.Data
{
    public class MVCBookStoreContext : IdentityDbContext<MVCBookStoreUser>
    {
        public MVCBookStoreContext (DbContextOptions<MVCBookStoreContext> options)
            : base(options)
        {
        }

        public DbSet<MVCBookStore.Models.Author> Author { get; set; } = default!;

        public DbSet<MVCBookStore.Models.BookGenre>? BookGenre { get; set; }

        public DbSet<MVCBookStore.Models.Books>? Books { get; set; }

        public DbSet<MVCBookStore.Models.Genre>? Genre { get; set; }

        public DbSet<MVCBookStore.Models.Review>? Review { get; set; }

        public DbSet<MVCBookStore.Models.UserBooks>? UserBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
