using DA.Kochbuch.Blazor.Server.Interfaces;
using DA.Kochbuch.Blazor.Server.Model;

namespace DA.Kochbuch.Blazor.Server.GraphQL
{
	/// <ChangeLog>
	/// <Create Datum="24.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public sealed class KochbuchQueryResolver(IKochbuch kochbuch)
	{
		private readonly IKochbuch kochbuchService = kochbuch;
		[GraphQLDescription("Gets all recipes.")]
		public async Task<List<Recipe>> GetRecipesAsync()
			=> await kochbuchService.GetRecipesAsync();

		[GraphQLDescription("Gets all units.")]
		public async Task<List<Unit>> GetUnitsAsync()
			=> await kochbuchService.GetUnitsAsync();

		[GraphQLDescription("Gets all ingredients.")]
		public async Task<List<Ingredient>> GetIngredientsAsync()
			=> await kochbuchService.GetIngredientsAsync();

		[GraphQLDescription("Gets all users.")]
		public async Task<List<User>> GetUsersAsync()
			=> await kochbuchService.GetUsersAsync();
	}
}