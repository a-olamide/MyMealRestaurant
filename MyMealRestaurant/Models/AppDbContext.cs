using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyMealRestaurant.Models
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        { }
        //public DbSet<Meal> Meal { get; set; }
        //public DbSet<Savory> Savory { get; set; }
        //public DbSet<Sweet> Sweet { get; set; }
    
    }
}
