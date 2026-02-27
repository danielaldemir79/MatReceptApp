using MatReceptAPI.Models.DTOs;
using MatReceptAPI.Repositories;

namespace MatReceptAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }
        public Task<RecipeResponseDto> CreateAsync(CreateRecipeDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeResponseDto>> GetByDifficultyAsync(string level)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeResponseDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeResponseDto>> SearchAsync(string term)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeResponseDto?> UpdateAsync(int id, CreateRecipeDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
