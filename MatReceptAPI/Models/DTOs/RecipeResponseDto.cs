namespace MatReceptAPI.Models.DTOs;

public class RecipeResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int PrepTimeMinutes { get; set; }
    public int CookTimeMinutes { get; set; }
    public int TotalTimeMinutes { get; set; }
    public int Servings { get; set; }
    public string Difficulty { get; set; } = string.Empty;

    public List<IngredientResponseDto> Ingredients { get; set; } = new();
    // Relation med IngredientResponseDto: En RecipeResponseDto har många IngredientResponseDto, och varje IngredientResponseDto tillhör en RecipeResponseDto
    public List<string> Instructions { get; set; } = new();
    // Eftersom EF Core inte kan mappa List<string> direkt, kan vi använda en separat entity för instruktioner eller spara dem som en JSON-sträng. Här väljer vi att spara dem som en JSON-sträng i databasen.
    public DateTime CreatedAt { get; set; }
}
