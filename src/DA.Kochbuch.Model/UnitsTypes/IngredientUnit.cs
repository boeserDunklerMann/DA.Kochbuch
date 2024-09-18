
using System.Text.Json.Serialization;

namespace DA.Kochbuch.Model.UnitsTypes
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Einheit einer Zutat eines Rezeptes
	/// </summary>
	/// <example>
	/// gram, Stk, Msp, ...
	/// </example>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. AD: Darum kümmert sich EFCore
	public class IngredientUnit : BaseModel
	{
		[JsonIgnore]
		public virtual ICollection<Ingredient> Ingredients { get; set; }
		public IngredientUnit()
		{
			CreationDate = ChangeDate = DateTime.UtcNow;
        }
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
