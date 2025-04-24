using DA.Kochbuch.Blazor.Server.Interfaces;
using DA.Kochbuch.Blazor.Server.Model;

namespace DA.Kochbuch.Blazor.Server.GraphQL
{
	/// <ChangeLog>
	/// <Create Datum="24.04.2025" Entwickler="DA" />
	/// </ChangeLog>
	public sealed class KochbuchMutationResolver(IConfiguration config, IKochbuch kochbuch, IWebHostEnvironment environment)
	{
		public record AddRecipePayload(Recipe Recipe);

		[GraphQLDescription("Adds new recipe data.")]
		public async Task<AddRecipePayload> AddRecipe(Recipe recipe)
		{
			await kochbuch.AddRecipeAsync(recipe);
			return new AddRecipePayload(recipe);
		}
	}
}