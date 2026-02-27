using Microsoft.AspNetCore.Mvc;
using Moq;
using MatReceptAPI.Controllers;
using MatReceptAPI.Services;
using MatReceptAPI.Models.DTOs;

namespace MatReceptAPI.Tests.ControllerTests;

public class RecipesControllerTests
{
    private readonly Mock<IRecipeService> _mockService;
    private readonly RecipesController _controller;

    public RecipesControllerTests()
    {
        _mockService = new Mock<IRecipeService>();
        _controller = new RecipesController(_mockService.Object);
    }

    // TEST 1: GetAll returnerar 200 OK
    [Fact]
    public async Task GetAll_Returns200OK()
    {
        // Arrange
        var fakeRecipes = new List<RecipeResponseDto>
        {
            new RecipeResponseDto { Id = 1, Name = "Pasta" },
            new RecipeResponseDto { Id = 2, Name = "Pizza" }
        };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(fakeRecipes);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
    }

    // TEST 2: GetById med ogiltigt id returnerar 404
    [Fact]
    public async Task GetById_NonExisting_Returns404()
    {
        // Arrange
        _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((RecipeResponseDto?)null);

        // Act
        var result = await _controller.GetRecipeById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    // TEST 3: Create returnerar 201 Created
    [Fact]
    public async Task Create_ValidDto_Returns201Created()
    {
        // Arrange
        var createDto = new CreateRecipeDto
        {
            Name = "Test Recipe",
            Description = "Test",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Difficulty = "Easy",
            Ingredients = new List<CreateIngredientDto>(),
            Instructions = new List<string> { "Step 1" }
        };
        var createdRecipe = new RecipeResponseDto { Id = 1, Name = "Test Recipe" };
        _mockService.Setup(s => s.CreateAsync(It.IsAny<CreateRecipeDto>())).ReturnsAsync(createdRecipe);

        // Act
        var result = await _controller.Create(createDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(201, createdResult.StatusCode);
    }
}