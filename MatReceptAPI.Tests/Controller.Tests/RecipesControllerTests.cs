using System;
using System.Collections.Generic;
using System.Text;

namespace MatReceptAPI.Tests.Controller.Tests
{
    internal class RecipesControllerTests
    {
        [Fact]
        public async task GetAll_Returns200OK()
        {
            // Arrange
            var mockService = new Mock<IRecipeService>();
            var fakeRecipes = new List<RecipeResponseDto>
            {
                new Recipe { Id = 1, Name = "Pasta" },
                new Recipe { Id = 2, Name = "Pizza" }
            };
            
            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200,okResult.Value);
        }
        [Fact]
        public async task GetById_NonExisting_Returns404()
        {
            // Arrange
            var mockService = new Mock<IRecipeService>();
            mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((RecipeResponseDto?)null);  // null = finns ej
            
            var controller = new RecipesController(mockService.Object);

            // Act
            var result = await controller.GetById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);






        }

    }
}