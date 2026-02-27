namespace MatReceptAPI.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string Unit { get; set; } = string.Empty;

    public int RecipeId { get; set; }
    // Fk till Recipe, eftersom varje Ingredient tillhör en Recipe
}
