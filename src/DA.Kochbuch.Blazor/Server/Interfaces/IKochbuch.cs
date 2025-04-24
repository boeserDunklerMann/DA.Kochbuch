using DA.Kochbuch.Blazor.Server.Model;

namespace DA.Kochbuch.Blazor.Server.Interfaces
{
	/// <ChangeLog>
	/// <Create Datum="24.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public interface IKochbuch
	{
		#region Querying
		Task<List<Ingredient>> GetIngredientsAsync();
		Task<List<Unit>> GetUnitsAsync();
		Task<List<Recipe>> GetRecipesAsync();
		#endregion

		#region Mutation
		Task AddRecipeAsync(Recipe recipe);
		#endregion
	}
}