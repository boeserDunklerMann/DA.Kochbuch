namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="25.07.2024" Entwickler="DA" />
	/// </ChangeLog>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. AD: Darum kümmert sich EFCore
	public class User : BaseModel
	{
		/// <summary>
		/// the users own created recipes
		/// </summary>
		public virtual ICollection<Recipe> OwnRecipes { get; set; }

		/// <summary>
		/// the users favorite recipes (from others)
		/// </summary>
		public virtual ICollection<Recipe> Favorites { get; set; }
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
