using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MatReceptAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    CookTimeMinutes = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookTimeMinutes", "CreatedAt", "Description", "Difficulty", "Instructions", "Name", "PrepTimeMinutes", "Servings" },
                values: new object[,]
                {
                    { 1, 20, new DateTime(2024, 3, 15, 10, 30, 0, 0, DateTimeKind.Utc), "Klassiska svenska pannkakor", "Easy", "[\"Vispa ihop mj\\u00F6l, mj\\u00F6lk och \\u00E4gg till en sl\\u00E4t smet\",\"L\\u00E5t vila 30 minuter\",\"Stek i sm\\u00F6r i het stekpanna\"]", "Pannkakor", 10, 4 },
                    { 2, 30, new DateTime(2024, 3, 15, 12, 0, 0, 0, DateTimeKind.Utc), "Enkel och god köttfärssås med pasta", "Easy", "[\"Bryn k\\u00F6ttf\\u00E4rsen i en stekpanna\",\"Hacka och fr\\u00E4s l\\u00F6k och vitl\\u00F6k\",\"Tills\\u00E4tt krossade tomater och kryddor\",\"L\\u00E5t sjuda i 20 minuter\",\"Koka pastan enligt f\\u00F6rpackningen\"]", "Köttfärssås", 15, 4 },
                    { 3, 25, new DateTime(2024, 3, 16, 8, 0, 0, 0, DateTimeKind.Utc), "Ugnsrostad lax med citron och dill", "Medium", "[\"S\\u00E4tt ugnen p\\u00E5 200 grader\",\"L\\u00E4gg laxen i en ugnsform\",\"Krydda med salt, peppar, citron och dill\",\"Tillaga i ugnen i ca 25 minuter\"]", "Laxfilé i ugn", 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name", "Quantity", "RecipeId", "Unit" },
                values: new object[,]
                {
                    { 1, "Mjöl", 3m, 1, "dl" },
                    { 2, "Mjölk", 6m, 1, "dl" },
                    { 3, "Ägg", 3m, 1, "st" },
                    { 4, "Salt", 0.5m, 1, "tsk" },
                    { 5, "Köttfärs", 500m, 2, "g" },
                    { 6, "Krossade tomater", 400m, 2, "g" },
                    { 7, "Lök", 1m, 2, "st" },
                    { 8, "Vitlök", 2m, 2, "klyftor" },
                    { 9, "Pasta", 400m, 2, "g" },
                    { 10, "Laxfilé", 400m, 3, "g" },
                    { 11, "Citron", 1m, 3, "st" },
                    { 12, "Dill", 1m, 3, "dl" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
