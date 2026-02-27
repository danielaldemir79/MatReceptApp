# 🍳 MatReceptAPI – Recepthantering Web API

Ett RESTful Web API byggt med ASP.NET Core för att hantera matrecept med CRUD-operationer.

---

## 📁 Projektstruktur

```
MatReceptAPI/
├── Controllers/
│   └── RecipesController.cs        ← Grupp 3
├── Services/
│   ├── IRecipeService.cs           ✅ Klar
│   └── RecipeService.cs            ← Grupp 2
├── Repositories/
│   ├── IRecipeRepository.cs        ✅ Klar
│   └── RecipeRepository.cs         ← Grupp 1
├── Models/
│   ├── Recipe.cs                   ✅ Klar
│   ├── Ingredient.cs               ✅ Klar
│   └── DTOs/
│       ├── CreateRecipeDto.cs      ✅ Klar
│       ├── CreateIngredientDto.cs  ✅ Klar
│       ├── RecipeResponseDto.cs    ✅ Klar
│       └── IngredientResponseDto.cs ✅ Klar
├── Program.cs                      ✅ Klar (DI + Swagger)
└── appsettings.json                ⚠️  Ändra connection string!

MatReceptAPI.Tests/                  ← Grupp 2 & 3
├── ServiceTests/
│   └── RecipeServiceTests.cs
└── ControllerTests/
    └── RecipesControllerTests.cs
```

---

## 🚀 Kom igång

### 1. Klona repot
```bash
git clone <repo-url>
cd MatReceptAPI
```

### 2. Ändra connection string
Öppna `MatReceptAPI/appsettings.json` och ändra `ÄNDRA_TILL_DIN_SERVER` till din lokala SQL Server-instans:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=DIN_DATOR_NAMN;Database=ReceptDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
}
```
> 💡 Hitta ditt servernamn: Öppna SSMS → servernamnet visas vid inloggning.

### 3. NuGet-paket (redan installerade i .csproj)
Dessa paket finns redan – kör bara restore:
```bash
dotnet restore
```

Paket som ingår:
| Paket | Version | Syfte |
|-------|---------|-------|
| `Microsoft.EntityFrameworkCore` | 10.0.3 | ORM för databasåtkomst |
| `Microsoft.EntityFrameworkCore.SqlServer` | 10.0.3 | SQL Server-provider |
| `Microsoft.EntityFrameworkCore.Design` | 10.0.3 | Migrations |
| `Microsoft.EntityFrameworkCore.Tools` | 10.0.3 | CLI-verktyg för migrations |
| `Swashbuckle.AspNetCore` | 10.1.4 | Swagger/OpenAPI-dokumentation |

**Testprojektet (`MatReceptAPI.Tests`) är redan skapat med:**
- xUnit
- Moq
- Projektreferens till MatReceptAPI

Allt finns – kör bara `dotnet restore`.

### 4. Kör projektet
```bash
dotnet run --project MatReceptAPI
```
Swagger UI: `https://localhost:{port}/swagger`

### 5. Kör tester
```bash
dotnet test
```

---

## 🔌 API-endpoints

| Metod | Route | Beskrivning |
|-------|-------|-------------|
| GET | `/api/recipes` | Hämta alla recept |
| GET | `/api/recipes/{id}` | Hämta specifikt recept |
| GET | `/api/recipes/search?q={term}` | Sök recept |
| GET | `/api/recipes/difficulty/{level}` | Filtrera på svårighetsgrad |
| POST | `/api/recipes` | Skapa nytt recept |
| PUT | `/api/recipes/{id}` | Uppdatera recept |
| DELETE | `/api/recipes/{id}` | Ta bort recept |

---

## 👥 Gruppfördelning

### 🟢 Grupp 1 – Repository (Dataåtkomst)
**Fil:** `Repositories/RecipeRepository.cs`

**Uppgift:** Implementera alla metoder i `RecipeRepository` som pratar med databasen via Entity Framework.

**Att göra:**
- [ ] Skapa `AppDbContext` med `DbSet<Recipe>` och `DbSet<Ingredient>`
- [ ] Konfigurera `OnModelCreating` (relationer, ev. seed data)
- [ ] Registrera `AppDbContext` i `Program.cs`
- [ ] Implementera `GetAllAsync()` – hämta alla recept inkl. ingredienser
- [ ] Implementera `GetByIdAsync(int id)` – hämta med `.Include(r => r.Ingredients)`
- [ ] Implementera `SearchAsync(string term)` – sök på Name/Description
- [ ] Implementera `GetByDifficultyAsync(string level)`
- [ ] Implementera `AddAsync(Recipe recipe)`
- [ ] Implementera `UpdateAsync(Recipe recipe)`
- [ ] Implementera `DeleteAsync(int id)`
- [ ] Skapa och köra migration: `dotnet ef migrations add InitialCreate` + `dotnet ef database update`

**Rör BARA dessa filer:**
- `Repositories/RecipeRepository.cs`
- `Data/AppDbContext.cs` (ny fil)
- `Program.cs` (bara lägg till `AddDbContext`-raden)

---

### 🔵 Grupp 2 – Service + Servicetester (Affärslogik)
**Fil:** `Services/RecipeService.cs` + `MatReceptAPI.Tests/ServiceTests/`

**Uppgift:** Implementera affärslogiken som mappar mellan modeller och DTOs, plus enhetstester.

**Att göra:**
- [ ] Implementera `GetAllAsync()` – hämta från repo, mappa till `RecipeResponseDto`
- [ ] Implementera `GetByIdAsync(int id)` – returnera `null` om ej funnet
- [ ] Implementera `SearchAsync(string term)`
- [ ] Implementera `GetByDifficultyAsync(string level)`
- [ ] Implementera `CreateAsync(CreateRecipeDto dto)` – mappa DTO → Recipe, sätt `CreatedAt`, beräkna `TotalTimeMinutes`
- [ ] Implementera `UpdateAsync(int id, CreateRecipeDto dto)`
- [ ] Implementera `DeleteAsync(int id)`
- [ ] Skapa testprojektet `MatReceptAPI.Tests` (om det inte finns)
- [ ] Skriv minst 5 servicetester med Moq:
  - `GetAllAsync_ReturnsListOfRecipes`
  - `GetByIdAsync_ExistingId_ReturnsRecipe`
  - `GetByIdAsync_NonExistingId_ReturnsNull`
  - `CreateAsync_ValidDto_ReturnsCreatedRecipe`
  - `SearchAsync_MatchingTerm_ReturnsFilteredResults`

**Rör BARA dessa filer:**
- `Services/RecipeService.cs`
- `MatReceptAPI.Tests/ServiceTests/RecipeServiceTests.cs` (ny fil)

---

### 🟡 Grupp 3 – Controller + Controllertester (HTTP-logik)
**Fil:** `Controllers/RecipesController.cs` + `MatReceptAPI.Tests/ControllerTests/`

**Uppgift:** Skapa controllern med alla endpoints och rätt HTTP-statuskoder, plus enhetstester.

**Att göra:**
- [ ] Skapa `RecipesController.cs` med `[ApiController]` och `[Route("api/recipes")]`
- [ ] Injektera `IRecipeService` via konstruktorn
- [ ] Implementera `GetAll()` → 200 OK
- [ ] Implementera `GetById(int id)` → 200 OK / 404 Not Found
- [ ] Implementera `Search(string q)` → 200 OK
- [ ] Implementera `GetByDifficulty(string level)` → 200 OK
- [ ] Implementera `Create(CreateRecipeDto dto)` → 201 Created med `CreatedAtAction`
- [ ] Implementera `Update(int id, CreateRecipeDto dto)` → 200 OK / 404 Not Found
- [ ] Implementera `Delete(int id)` → 204 No Content / 404 Not Found
- [ ] Skriv minst 3 controllertester med Moq:
  - `GetAll_Returns200OK`
  - `GetById_NonExisting_Returns404`
  - `Create_ValidDto_Returns201Created`

**Rör BARA dessa filer:**
- `Controllers/RecipesController.cs` (ny fil)
- `MatReceptAPI.Tests/ControllerTests/RecipesControllerTests.cs` (ny fil)

---

## 🔀 Git-strategi – Undvik merge-konflikter!

### Regler
1. **Varje grupp jobbar på sin egen branch:**
   ```bash
   git checkout -b grupp1/repository
   git checkout -b grupp2/service
   git checkout -b grupp3/controller
   ```

2. **Rör BARA dina filer** – se listan ovan per grupp. Om du behöver ändra en delad fil (t.ex. `Program.cs`), prata med de andra grupperna först.

3. **Commit ofta med tydliga meddelanden:**
   ```bash
   git add .
   git commit -m "feat: implementera GetAllAsync i RecipeRepository"
   ```

4. **Merge-ordning (viktigt!):**
   ```
   1️⃣  Grupp 1 (Repository) mergar FÖRST → main
   2️⃣  Grupp 2 (Service) mergar SEDAN → main
   3️⃣  Grupp 3 (Controller) mergar SIST → main
   ```
   > Detta för att Service beror på Repository, och Controller beror på Service.

5. **Innan merge – dra ner senaste main:**
   ```bash
   git checkout main
   git pull
   git checkout grupp2/service
   git merge main
   # Lös ev. konflikter
   git push
   # Skapa Pull Request
   ```

### Varför denna ordning fungerar
- Varje grupp jobbar i **separata filer** → inga konflikter
- Interfaces (`IRecipeService`, `IRecipeRepository`) är redan klara → alla kan koda mot dem
- `NotImplementedException` i stubs gör att koden kompilerar även om andra grupper inte är klara

---

## ✅ Checklista innan inlämning

- [ ] Alla endpoints fungerar (testa i Swagger)
- [ ] Minst 8 enhetstester passerar
- [ ] DI fungerar korrekt
- [ ] Validering fungerar (testa med felaktig data)
- [ ] Koden kompilerar utan varningar
- [ ] Connection string är uppdaterad
- [ ] README är uppdaterad

---


