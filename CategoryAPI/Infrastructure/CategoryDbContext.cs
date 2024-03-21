using CategoryAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace CategoryAPI.Infrastructure
{
    public class CategoryDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("CategoryDatabase");
        }
    }
}
