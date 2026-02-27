using MatReceptAPI.Models;

namespace MatReceptAPI.Repositories;

public interface IRecipeRepository
{
    Task<IEnumerable<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(int id);
    Task<IEnumerable<Recipe>> SearchAsync(string term);
    Task<IEnumerable<Recipe>> GetByDifficultyAsync(string level);
    Task<Recipe> AddAsync(Recipe recipe);
    Task<Recipe?> UpdateAsync(Recipe recipe);
    Task<bool> DeleteAsync(int id);
}
