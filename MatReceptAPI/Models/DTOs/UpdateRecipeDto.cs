namespace MatReceptAPI.Models.DTOs
{
    public class UpdateRecipeDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PreparationTime { get; set; }
        public int Difficulty { get; set; }
        public List<UpdateIngredientDto> Ingredients { get; set; } = new List<UpdateIngredientDto>();
    }
}
