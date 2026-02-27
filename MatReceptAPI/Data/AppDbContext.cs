using Microsoft.EntityFrameworkCore;
using MatReceptAPI.Models;
namespace MatReceptAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
       
    }
}
