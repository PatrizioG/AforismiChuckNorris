using AforismiChuckNorris.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AforismiChuckNorris.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Aphorism> Aphorisms { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
