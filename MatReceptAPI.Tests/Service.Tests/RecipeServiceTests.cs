using Moq;
using MatReceptAPI.Models;
using MatReceptAPI.Models.DTOs;
using MatReceptAPI.Repositories;
using MatReceptAPI.Services;

namespace MatReceptAPI.Tests.Service.Tests;

public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _mockRepository;
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _mockRepository = new Mock<IRecipeRepository>();
        _service = new RecipeService(_mockRepository.Object);
    }

    #region GetAllAsync Tests

    [Fact]
    public async Task GetAllAsync_ReturnsListOfRecipes()
    {
        // Arrange
        var recipes = new List<Recipe>
        {
            CreateTestRecipe(1, "Pasta Carbonara"),
            CreateTestRecipe(2, "Köttbullar")
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(recipes);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, r => r.Name == "Pasta Carbonara");
        Assert.Contains(result, r => r.Name == "Köttbullar");
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    #endregion

    #region GetByIdAsync Tests

    [Fact]
    public async Task GetByIdAsync_RecipeExists_ReturnsRecipe()
    {
        // Arrange
        var recipe = CreateTestRecipe(1, "Pasta Carbonara");
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(recipe);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Pasta Carbonara", result.Name);
        _mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_RecipeDoesNotExist_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Recipe?)null);

        // Act
        var result = await _service.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
        _mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
    }

    #endregion

    #region CreateAsync Tests

    [Fact]
    public async Task CreateAsync_ValidDto_CreatesAndReturnsRecipe()
    {
        // Arrange
        var createDto = new CreateRecipeDto
        {
            Name = "Pannkakor",
            Description = "Klassiska svenska pannkakor",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 20,
            Servings = 4,
            Difficulty = "Easy",
            Instructions = ["Vispa ihop smet", "Stek i stekpanna"],
            Ingredients =
            [
                new CreateIngredientDto { Name = "Mjöl", Quantity = 3, Unit = "dl" },
                new CreateIngredientDto { Name = "Ägg", Quantity = 3, Unit = "st" }
            ]
        };

        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Recipe>()))
            .ReturnsAsync((Recipe r) =>
            {
                r.Id = 1;
                return r;
            });

        // Act
        var result = await _service.CreateAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Pannkakor", result.Name);
        Assert.Equal(30, result.TotalTimeMinutes); // 10 + 20
        Assert.Equal(2, result.Ingredients.Count);
        _mockRepository.Verify(r => r.AddAsync(It.IsAny<Recipe>()), Times.Once);
    }

    #endregion

    #region SearchAsync Tests

    [Fact]
    public async Task SearchAsync_MatchingTerm_ReturnsFilteredRecipes()
    {
        // Arrange
        var allRecipes = new List<Recipe>
        {
            CreateTestRecipe(1, "Pasta Carbonara"),
            CreateTestRecipe(2, "Pasta Bolognese"),
            CreateTestRecipe(3, "Köttbullar")
        };

        var filteredRecipes = allRecipes.Where(r => r.Name.Contains("Pasta")).ToList();
        _mockRepository.Setup(r => r.SearchAsync("Pasta")).ReturnsAsync(filteredRecipes);

        // Act
        var result = await _service.SearchAsync("Pasta");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, r => Assert.Contains("Pasta", r.Name));
        _mockRepository.Verify(r => r.SearchAsync("Pasta"), Times.Once);
    }

    #endregion

    #region Helper Methods

    private static Recipe CreateTestRecipe(int id, string name)
    {
        return new Recipe
        {
            Id = id,
            Name = name,
            Description = $"Beskrivning för {name}",
            PrepTimeMinutes = 15,
            CookTimeMinutes = 30,
            Servings = 4,
            Difficulty = "Medium",
            CreatedAt = DateTime.UtcNow,
            Instructions = ["Steg 1", "Steg 2"],
            Ingredients =
            [
                new Ingredient { Id = 1, Name = "Ingrediens 1", Quantity = 100, Unit = "g", RecipeId = id }
            ]
        };
    }

    #endregion
}
