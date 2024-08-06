using DA.Kochbuch.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.WebAPI.Controllers
{
	/// <ChangeLog>
	/// <Create Datum="06.08.2024" Entwickler="DA" />
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
		public async Task<IActionResult> CreateRecipeAsync(Guid accessTokenID, Recipe newRecipe)
		{
			Logger.LogInformation($"running {nameof(CreateRecipeAsync)}");
			await VerifyAccessToken(accessTokenID, true);

			await KochbuchContext.Recipes.AddAsync(newRecipe);
			await KochbuchContext.SaveChangesAsync();
			return Ok(newRecipe);
		}

		[HttpGet]
		public async Task<IEnumerable<Recipe>> GetAllRecipeAsync(Guid accessTokenID)
		{
			Logger.LogInformation($"running {nameof(GetAllRecipeAsync)}");
			await VerifyAccessToken(accessTokenID, true);

			return await KochbuchContext.Recipes.Where(r => !r.Deleted).ToListAsync();
		}

		[HttpGet]
		[Route("{RecipeID}/{accessTokenID}")]
		public async Task<Recipe> GetRecipeAsync(Guid accessTokenID, int RecipeID)
		{
			Logger.LogInformation($"running {nameof(GetRecipeAsync)}");
			await VerifyAccessToken(accessTokenID, true);

			return await KochbuchContext.Recipes.FirstOrDefaultAsync(r => !r.Deleted && r.ID == RecipeID);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateRecipeAsync(Guid accessTokenID, Recipe recipe)
		{
			Logger.LogInformation($"running {nameof(UpdateRecipeAsync)}");
			await VerifyAccessToken(accessTokenID, true);

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
		public async Task<IActionResult> DeleteRecipeAsync(Guid accessTokenID, Recipe recipe)
		{
			Logger.LogInformation($"running {nameof(DeleteRecipeAsync)}");
			await VerifyAccessToken(accessTokenID, true);

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
