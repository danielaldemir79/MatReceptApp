# MatReceptAPI

Ett REST API i ASP.NET Core för att skapa, läsa, uppdatera, söka och ta bort recept med ingredienser.

## Teknik

- .NET `10.0`
- ASP.NET Core Web API
- Entity Framework Core (`SqlServer`)
- Swagger / OpenAPI
- xUnit + Moq (enhetstester)

## Projektstruktur

```text
MatReceptApp/
├── MatReceptAPI/
│   ├── Controllers/
│   │   └── RecipesController.cs
│   ├── Data/
│   │   └── AppDbContext.cs
│   ├── Models/
│   │   ├── Recipe.cs
│   │   ├── Ingredient.cs
│   │   └── DTOs/
│   ├── Repositories/
│   │   ├── IRecipeRepository.cs
│   │   └── RecipeRepository.cs
│   ├── Services/
│   │   ├── IRecipeService.cs
│   │   ├── RecipeService.cs
│   │   └── ServiceHelpers.cs
│   └── Program.cs
└── MatReceptAPI.Tests/
    ├── Controller.Tests/
    └── Service.Tests/
```

## Kom igång

### 1. Klona och gå till projektet

```bash
git clone <repo-url>
cd MatReceptApp
```

### 2. Konfigurera databas

Connection string finns i `MatReceptAPI/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ReceptDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
}
```

Byt `Server` till din SQL Server-instans.

### 3. Installera beroenden

```bash
dotnet restore
```

### 4. Kör migrationer

```bash
dotnet ef database update --project MatReceptAPI
```

> Projektet innehåller en initial migration och seed-data (3 recept + ingredienser).

### 5. Starta API

```bash
dotnet run --project MatReceptAPI
```

Swagger finns i development-läge på:

- `https://localhost:<port>/swagger`

## API-endpoints

Basroute: `api/recipes`

- `GET /api/recipes` - Hämta alla recept
- `GET /api/recipes/{id}` - Hämta recept via id
- `GET /api/recipes/search?q={term}` - Sök på namn/beskrivning
- `GET /api/recipes/difficulty/{level}` - Filtrera på svårighetsgrad (`Easy`, `Medium`, `Hard`)
- `POST /api/recipes` - Skapa nytt recept
- `PUT /api/recipes/{id}` - Uppdatera befintligt recept
- `DELETE /api/recipes/{id}` - Ta bort recept

## DTO och validering

`CreateRecipeDto` och `UpdateRecipeDto` validerar bland annat:

- `Name`: required, 3-100 tecken
- `Description`: max 500 tecken
- `PrepTimeMinutes`: 1-480
- `CookTimeMinutes`: 0-480
- `Servings`: 1-100
- `Difficulty`: required (`Easy`, `Medium`, `Hard` normaliseras i service)
- `Ingredients`: minst 1 ingrediens
- `Instructions`: minst 1 steg

## Arkitektur

Flöde:

1. `RecipesController` hanterar HTTP och statuskoder.
2. `RecipeService` hanterar affärslogik, validering och mapping mellan model/DTO.
3. `RecipeRepository` hanterar databasåtkomst via EF Core.
4. `AppDbContext` konfigurerar relationer, JSON-konvertering för `Instructions` och seed-data.

## Tester

Kör tester:

```bash
dotnet test
```

Nuvarande status (verifierat):

- Totalt: `8`
- Passed: `8`
- Failed: `0`
- Skipped: `0`

## Statuskoder

Controllern returnerar:

- `200 OK` vid lyckade GET/PUT
- `201 Created` vid POST (med `CreatedAtAction`)
- `204 No Content` vid lyckad DELETE
- `404 Not Found` när recept saknas

## Kommentarer / Kända förbättringar

- `Instructions` lagras som JSON i databasen.
- Swagger/OpenAPI är aktivt i development-miljö.
- För produktion kan ni lägga till global exception handling och tydligare felresponsformat.
