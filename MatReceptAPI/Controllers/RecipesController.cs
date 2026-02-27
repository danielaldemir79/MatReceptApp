using Microsoft.AspNetCore.Mvc;
using MatReceptAPI.Services;
using MatReceptAPI.Models.DTOs;

namespace MatReceptAPI.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    [HttpGet("{id}")] // GET api/recipes/{id}
    public async Task<ActionResult<RecipeResponseDto>> GetRecipeById(int id)
        {
            var recipe = await _recipeService.GetByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
    [HttpGet("search")] // GET api/recipes/search
    public async Task<ActionResult<IEnumerable<RecipeResponseDto>>> Search([FromQuery] string q)
    {
        var recipes = await _recipeService.SearchAsync(q);
        return Ok(recipes);
    }
    [HttpGet("difficulty/{level}")] // GET api/recipes/difficulty/{level}
    public async Task<ActionResult<IEnumerable<RecipeResponseDto>>> GetByDifficulty(string level)
    {
        var recipes = await _recipeService.GetByDifficultyAsync(level);
        return Ok(recipes);
    }
    [HttpPost]
    public async Task<ActionResult<RecipeResponseDto>> Create(CreateRecipeDto dto)
    {
        var recipe = await _recipeService.CreateAsync(dto);

        // 201 Created + Location-header som pekar till det nya receptet
        return CreatedAtAction(nameof(GetById), new { id = recipe.Id }, recipe);
    }
    [HttpPut("{id}")] // PUT api/recipes/{id}
    public async Task<ActionResult<RecipeResponseDto>> Update(int id, UpdateRecipeDto dto)
    {
        var updatedRecipe = await _recipeService.UpdateAsync(id, dto);
        if (updatedRecipe == null)
        {
            return NotFound();
        }
        return Ok(updatedRecipe);
    }
    [HttpDelete("{id}")] // DELETE api/recipes/{id}
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _recipeService.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
