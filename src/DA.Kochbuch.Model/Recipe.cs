#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. AD: Darum kümmert sich EFCore

using System.Text.Json.Serialization;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class Recipe : BaseModel
	{
		/// <summary>
		/// Anzahl der Personen, für die das Rezept gedacht ist
		/// </summary>
		public int NumberPersons { get; set; }

		public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
		[JsonIgnore]
		public virtual User User { get; set; }
		public string? CookInstructon { get; set; }// = string.Empty;
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
