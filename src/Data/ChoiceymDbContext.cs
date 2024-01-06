using Choice_Ym.Models;
using Microsoft.EntityFrameworkCore;

namespace Choice_Ym.Data
{
    public class ChoiceymDbContext : DbContext
    {
        public ChoiceymDbContext()
        {

        }

        public ChoiceymDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}