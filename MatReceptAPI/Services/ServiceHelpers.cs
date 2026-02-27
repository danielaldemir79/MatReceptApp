using MatReceptAPI.Models;
using MatReceptAPI.Models.DTOs;

namespace MatReceptAPI.Services;

public class ServiceHelpers
{
  public static RecipeResponseDto MapToResponseDto(Recipe r)
  {
    return new RecipeResponseDto
    {
      Id = r.Id,
      Name = r.Name,
      Description = r.Description,
      PrepTimeMinutes = r.PrepTimeMinutes,
      CookTimeMinutes = r.CookTimeMinutes,
      TotalTimeMinutes = r.PrepTimeMinutes + r.CookTimeMinutes,
      Servings = r.Servings,
      Difficulty = r.Difficulty,
      Ingredients = r.Ingredients.Select(i => new IngredientResponseDto
      {
        Id = i.Id,
        Name = i.Name,
        Quantity = i.Quantity,
        Unit = i.Unit
      }).ToList(),
      Instructions = r.Instructions.ToList(),
      CreatedAt = r.CreatedAt
    };
  }

  public static Recipe MapToModel(CreateRecipeDto dto)
  {
    return new Recipe
    {
      Name = dto.Name.Trim(),
      Description = dto.Description?.Trim() ?? "",
      PrepTimeMinutes = dto.PrepTimeMinutes,
      CookTimeMinutes = dto.CookTimeMinutes,
      Servings = dto.Servings,
      Difficulty = NormalizeDifficulty(dto.Difficulty),
      Ingredients = dto.Ingredients.Select(i => new Ingredient
      {
        Name = i.Name.Trim(),
        Quantity = i.Quantity,
        Unit = i.Unit.Trim()
      }).ToList(),
      Instructions = dto.Instructions.Select(s => s.Trim()).ToList()
    };
  }

  public static string NormalizeDifficulty(string? difficulty)
  {
    var d = (difficulty ?? "").Trim().ToLowerInvariant();

    return d switch
    {
      "easy" => "Easy",
      "medium" => "Medium",
      "hard" => "Hard",
      _ => throw new ArgumentException("Difficulty must be Easy, Medium or Hard.")
    };
  }

  public static void Validate(CreateRecipeDto dto)
  {
    if (dto is null)
      throw new ArgumentNullException(nameof(dto));

    if (string.IsNullOrWhiteSpace(dto.Name) || dto.Name.Trim().Length < 3)
      throw new ArgumentException("Name must be at least 3 characters.");

    if (dto.Ingredients is null || dto.Ingredients.Count < 1)
      throw new ArgumentException("At least one ingredient is required.");

    if (dto.Instructions is null || dto.Instructions.Count < 1)
      throw new ArgumentException("At least one instruction is required.");

    // kolla difficulty via NormalizeDifficulty (kastar om fel)
    _ = NormalizeDifficulty(dto.Difficulty);
  }
}  