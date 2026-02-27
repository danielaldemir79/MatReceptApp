using MatReceptAPI.Models;
using MatReceptAPI.Models.DTOs;
using MatReceptAPI.Repositories;
using static MatReceptAPI.Services.ServiceHelpers;

namespace MatReceptAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;

        public RecipeService(IRecipeRepository repository)
        {
            _repository = repository;
        }
        public async Task<RecipeResponseDto> CreateAsync(CreateRecipeDto dto)
        {
            Validate(dto);
            var recipe = MapToModel(dto);
            recipe.CreatedAt = DateTime.UtcNow;

            var created = await _repository.AddAsync(recipe);
            return MapToResponseDto(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetAllAsync()
        {
            var recipes = await _repository.GetAllAsync();
            return recipes.Select(MapToResponseDto).ToList();
        }

        public async Task<IEnumerable<RecipeResponseDto>> GetByDifficultyAsync(string level)
        {
            var normalized = NormalizeDifficulty(level);
            var results = await _repository.GetByDifficultyAsync(normalized);
            return results.Select(MapToResponseDto).ToList();
        }

        public async Task<RecipeResponseDto?> GetByIdAsync(int id)
        {
            var recipe = await _repository.GetByIdAsync(id);
            return recipe is null ? null : MapToResponseDto(recipe);
        }

        public async Task<IEnumerable<RecipeResponseDto>> SearchAsync(string term)
        {
            term ??= "";
            term = term.Trim();

            if (term.Length == 0)
            {
                var all = await _repository.GetAllAsync();
                return all.Select(MapToResponseDto).ToList();
            }

            var result = await _repository.SearchAsync(term);
            return result.Select(MapToResponseDto).ToList();
        }

        public async Task<RecipeResponseDto?> UpdateAsync(int id, CreateRecipeDto dto)
        {
            Validate(dto);

            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
                return null;

            existing.Name = dto.Name.Trim();
            existing.Description = dto.Description.Trim() ?? "";
            existing.PrepTimeMinutes = dto.PrepTimeMinutes;
            existing.CookTimeMinutes = dto.CookTimeMinutes;
            existing.Servings = dto.Servings;
            existing.Difficulty = NormalizeDifficulty(dto.Difficulty);

            existing.Ingredients = dto.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name.Trim(),
                Quantity = i.Quantity,
                Unit = i.Unit.Trim()
            }).ToList();

            existing.Instructions = dto.Instructions.Select(s => s.Trim()).ToList();

            var updated = await _repository.UpdateAsync(existing);
            return MapToResponseDto(updated!);
        }
    } 
}
