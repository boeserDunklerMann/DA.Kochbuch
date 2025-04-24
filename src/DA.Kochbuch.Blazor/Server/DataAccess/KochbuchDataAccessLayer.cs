using DA.Kochbuch.Blazor.Server.Interfaces;
using DA.Kochbuch.Blazor.Server.Model;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.Blazor.Server.DataAccess
{
	/// <ChangeLog>
	/// <Create Datum="24.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public sealed class KochbuchDataAccessLayer(IDbContextFactory<KochbuchContext> contextFactory) : IKochbuch
	{
		private readonly KochbuchContext dbContext = contextFactory.CreateDbContext();
		public async Task<List<Recipe>> GetRecipesAsync()
		{
			return await dbContext.Recipes.AsNoTracking().ToListAsync();
		}

		public async Task<List<Unit>> GetUnitsAsync()
			=> await dbContext.Units.AsNoTracking().ToListAsync();

		public async Task<List<Ingredient>> GetIngredientsAsync()
			=> await dbContext.Ingredients.AsNoTracking().ToListAsync();

		public async Task AddRecipeAsync(Recipe recipe)
		{
			try
			{
				await dbContext.Recipes.AddAsync(recipe);
				await dbContext.SaveChangesAsync();
			}
			catch (Exception e)
			{
				//logger.LogError($"Exception was thrown: {e.Message}");
				throw;
			}
		}
	}
}