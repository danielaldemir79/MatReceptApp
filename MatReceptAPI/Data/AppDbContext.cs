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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Här skapar vi relationer och konfigurationer för våra entiteter

            base.OnModelCreating(modelBuilder);

            // En Recipe har många Ingredients, varje Ingredient tillhör ett Recipe
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne()
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Instructions (List<string>) lagras som JSON-kolumn
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Instructions)
                .HasConversion(
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions?)null)!
                );

            // Decimal precision för Quantity
            modelBuilder.Entity<Ingredient>()
                .Property(i => i.Quantity)
                .HasPrecision(10, 2);

            // Seed data
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe
                {
                    Id = 1,
                    Name = "Pannkakor",
                    Description = "Klassiska svenska pannkakor",
                    PrepTimeMinutes = 10,
                    CookTimeMinutes = 20,
                    Servings = 4,
                    Difficulty = "Easy",
                    Instructions = new List<string>
                    {
                        "Vispa ihop mjöl, mjölk och ägg till en slät smet",
                        "Låt vila 30 minuter",
                        "Stek i smör i het stekpanna"
                    },
                    CreatedAt = new DateTime(2024, 3, 15, 10, 30, 0, DateTimeKind.Utc)
                },
                new Recipe
                {
                    Id = 2,
                    Name = "Köttfärssås",
                    Description = "Enkel och god köttfärssås med pasta",
                    PrepTimeMinutes = 15,
                    CookTimeMinutes = 30,
                    Servings = 4,
                    Difficulty = "Easy",
                    Instructions = new List<string>
                    {
                        "Bryn köttfärsen i en stekpanna",
                        "Hacka och fräs lök och vitlök",
                        "Tillsätt krossade tomater och kryddor",
                        "Låt sjuda i 20 minuter",
                        "Koka pastan enligt förpackningen"
                    },
                    CreatedAt = new DateTime(2024, 3, 15, 12, 0, 0, DateTimeKind.Utc)
                },
                new Recipe
                {
                    Id = 3,
                    Name = "Laxfilé i ugn",
                    Description = "Ugnsrostad lax med citron och dill",
                    PrepTimeMinutes = 10,
                    CookTimeMinutes = 25,
                    Servings = 2,
                    Difficulty = "Medium",
                    Instructions = new List<string>
                    {
                        "Sätt ugnen på 200 grader",
                        "Lägg laxen i en ugnsform",
                        "Krydda med salt, peppar, citron och dill",
                        "Tillaga i ugnen i ca 25 minuter"
                    },
                    CreatedAt = new DateTime(2024, 3, 16, 8, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                // Pannkakor (RecipeId = 1)
                new Ingredient { Id = 1, Name = "Mjöl", Quantity = 3, Unit = "dl", RecipeId = 1 },
                new Ingredient { Id = 2, Name = "Mjölk", Quantity = 6, Unit = "dl", RecipeId = 1 },
                new Ingredient { Id = 3, Name = "Ägg", Quantity = 3, Unit = "st", RecipeId = 1 },
                new Ingredient { Id = 4, Name = "Salt", Quantity = 0.5m, Unit = "tsk", RecipeId = 1 },

                // Köttfärssås (RecipeId = 2)
                new Ingredient { Id = 5, Name = "Köttfärs", Quantity = 500, Unit = "g", RecipeId = 2 },
                new Ingredient { Id = 6, Name = "Krossade tomater", Quantity = 400, Unit = "g", RecipeId = 2 },
                new Ingredient { Id = 7, Name = "Lök", Quantity = 1, Unit = "st", RecipeId = 2 },
                new Ingredient { Id = 8, Name = "Vitlök", Quantity = 2, Unit = "klyftor", RecipeId = 2 },
                new Ingredient { Id = 9, Name = "Pasta", Quantity = 400, Unit = "g", RecipeId = 2 },

                // Laxfilé (RecipeId = 3)
                new Ingredient { Id = 10, Name = "Laxfilé", Quantity = 400, Unit = "g", RecipeId = 3 },
                new Ingredient { Id = 11, Name = "Citron", Quantity = 1, Unit = "st", RecipeId = 3 },
                new Ingredient { Id = 12, Name = "Dill", Quantity = 1, Unit = "dl", RecipeId = 3 }
            );
        }




    }
}
