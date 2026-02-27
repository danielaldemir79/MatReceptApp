using MatReceptAPI.Models.DTOs;

namespace MatReceptAPI.Services;

public interface IRecipeService
{
    Task<IEnumerable<RecipeResponseDto>> GetAllAsync();
    Task<RecipeResponseDto?> GetByIdAsync(int id);
    Task<IEnumerable<RecipeResponseDto>> SearchAsync(string term);
    Task<IEnumerable<RecipeResponseDto>> GetByDifficultyAsync(string level);
    Task<RecipeResponseDto> CreateAsync(CreateRecipeDto dto);
    Task<RecipeResponseDto?> UpdateAsync(int id, UpdateRecipeDto dto);
    Task<bool> DeleteAsync(int id);
}
