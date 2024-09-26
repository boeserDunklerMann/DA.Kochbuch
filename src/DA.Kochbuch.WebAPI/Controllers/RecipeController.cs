using DA.Kochbuch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="06.08.2024" Entwickler="DA" />
	/// <Change Datum="25.09.2024" Entwickler="DA">made Getter more EF-like with .Include</Change>
	/// </ChangeLog>
	[Route("api/[controller]")]
	[ApiController]
	public class RecipeController(ILogger<RecipeController> logger, IConfiguration cfg) : KochbuchBaseController(logger, cfg)
	{
		#region CRUD ops
#pragma warning disable CS8600 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Dereference of a possibly null reference.
		[HttpPost]
		public async Task<IActionResult> CreateRecipeAsync(string username, string password, Recipe newRecipe)
		{
			Logger.LogInformation($"running {nameof(CreateRecipeAsync)}");
			await VerifyUserAsync(username, password, true);

			await KochbuchContext.Recipes.AddAsync(newRecipe);
			await KochbuchContext.SaveChangesAsync();
			return Ok(newRecipe);
		}

		[HttpGet]
		public async Task<IEnumerable<Recipe>> GetAllRecipeAsync(string username, string password)
		{
			Logger.LogInformation($"running {nameof(GetAllRecipeAsync)}");
			await VerifyUserAsync(username, password, true);

			KochbuchContext.Units.ToList();

			return await KochbuchContext.Recipes
					.Include(nameof(Recipe.Ingredients))
					.Include(nameof(Recipe.Images))
				.Where(r => !r.Deleted).ToListAsync();
		}

		[HttpGet]
		[Route("{RecipeID}/{username}/{password}")]
		public async Task<Recipe> GetRecipeAsync(string username, string password, int RecipeID)
		{
			Logger.LogInformation($"running {nameof(GetRecipeAsync)}");
			await VerifyUserAsync(username, password, true);

			KochbuchContext.Units.ToList();

			return await KochbuchContext.Recipes
					.Include(nameof(Recipe.Ingredients))
					.Include(nameof(Recipe.Images))
				.FirstOrDefaultAsync(r => !r.Deleted && r.ID == RecipeID);
		}

		[HttpGet]
		[Route("user/{UserID}/{username}/{password}")]
		public async Task<IEnumerable<Recipe>> GetRecipesByUserIDAsync(string username, string password,int UserID)
		{
			Logger.LogInformation($"running {nameof(GetRecipesByUserIDAsync)}");
			await VerifyUserAsync(username, password, true);

			KochbuchContext.Units.ToList();
			KochbuchContext.Users.ToList();

			return await KochbuchContext.Recipes
					.Include(nameof(Recipe.Ingredients))
					.Include(nameof(Recipe.Images))
					.Include(nameof(Recipe.User))
				.Where(r => !r.Deleted && r.User.ID == UserID).ToListAsync();
		}

		[HttpPut]
		public async Task<IActionResult> UpdateRecipeAsync(string username, string password, Recipe recipe)
		{
			Logger.LogInformation($"running {nameof(UpdateRecipeAsync)}");
			await VerifyUserAsync(username, password, true);

			Recipe recipeFromDB = await KochbuchContext.Recipes.FirstOrDefaultAsync(r => !r.Deleted && r.ID == recipe.ID);
			if (recipeFromDB == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Recipe), recipe.ID);
			recipeFromDB.Name = recipe.Name;
			recipeFromDB.NumberPersons = recipe.NumberPersons;
			//recipeFromDB.Ingredients = recipe.Ingredients;
			recipeFromDB.User = recipe.User;
			recipeFromDB.CookInstructon = recipe.CookInstructon;
			recipeFromDB.ChangeDate = DateTime.UtcNow;
			await KochbuchContext.SaveChangesAsync();
			return Ok(recipeFromDB);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteRecipeAsync(string username, string password, Recipe recipe)
		{
			Logger.LogInformation($"running {nameof(DeleteRecipeAsync)}");
			await VerifyUserAsync(username, password, true);

			Recipe recipeFromDB = await KochbuchContext.Recipes.FirstOrDefaultAsync(r => !r.Deleted && r.ID == recipe.ID);
			if (recipeFromDB == null)
				throw new Exceptions.ObjectNotFoundException(nameof(Recipe), recipe.ID);
			recipeFromDB.Deleted = true;
			recipeFromDB.ChangeDate = DateTime.UtcNow;
			await KochbuchContext.SaveChangesAsync();
			return Ok(recipeFromDB);
		}

#pragma warning restore CS8600 // Dereference of a possibly null reference.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8603 // Dereference of a possibly null reference.
		#endregion
	}
}