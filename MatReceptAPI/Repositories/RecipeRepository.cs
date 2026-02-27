using MatReceptAPI.Data;
using MatReceptAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatReceptAPI.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Recipe> AddAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return recipe;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recipe = await GetByIdAsync(id);

            if (recipe == null)
                return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recipe>> GetByDifficultyAsync(string level)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.Difficulty.ToLower() == level.ToLower())
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {

            return await _context.Recipes
               .Include(r => r.Ingredients)
               .FirstOrDefaultAsync(r => r.Id == id);

        }

        public async Task<IEnumerable<Recipe>> SearchAsync(string term)
        {
            return await _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.Name.ToLower().Contains(term.ToLower()) ||
                            r.Description.ToLower().Contains(term.ToLower()))
                .ToListAsync();
        }

        public async Task<Recipe?> UpdateAsync(Recipe recipe)
        {
            // Rensa gamla ingredienser
            _context.Ingredients.RemoveRange(
                _context.Ingredients.Where(i => i.RecipeId == recipe.Id)
            );

            // Spara ändringarna (EF Core trackar redan objektet)
            await _context.SaveChangesAsync();
            return recipe;

        }
    }
}
