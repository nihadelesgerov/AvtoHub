using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AvtoHubProject.Models
{
    public class AvtoHubDbContext : IdentityDbContext<AvtoHubUser>
    {
        public AvtoHubDbContext(DbContextOptions options) : base(options) { }


        public DbSet<Product> AvtoHubProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AvtoHubUser>().HasMany(a=>a.Products).WithOne(a=>a.AvtoHubUser).HasForeignKey(a=>a.UserId);
        }
    }
}
