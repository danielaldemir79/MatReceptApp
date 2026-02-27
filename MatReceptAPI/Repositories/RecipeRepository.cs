using MatReceptAPI.Models;

namespace MatReceptAPI.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        public Task<Recipe> AddAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> GetByDifficultyAsync(string level)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Recipe>> SearchAsync(string term)
        {
            throw new NotImplementedException();
        }

        public Task<Recipe?> UpdateAsync(Recipe recipe)
        {
            throw new NotImplementedException();
        }
    }
}
