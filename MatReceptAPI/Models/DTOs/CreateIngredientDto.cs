using System.ComponentModel.DataAnnotations;

namespace MatReceptAPI.Models.DTOs;

public class CreateIngredientDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 10000)]
    public decimal Quantity { get; set; }

    [Required]
    public string Unit { get; set; } = string.Empty;
}
