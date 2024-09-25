using DA.Kochbuch.Model.UnitsTypes;
using System.Text.Json.Serialization;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. AD: Darum kümmert sich EFCore

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <summary>
	/// Eine einzelne Zutat eines Rezeptes
	/// </summary>
	public class Ingredient : BaseModel
	{
        public float Amount { get; set; }
        public IngredientUnit? Unit { get; set; }
		[JsonIgnore]
		public virtual Recipe? Recipe { get; set; }

		public override string ToString()
		{
			return $"{Amount} {Unit?.Name} {Name}";
		}
		public override bool Equals(object? obj)
		{
			if (obj == null || !(obj is Ingredient)) 
				return false;
			return ID == ((Ingredient)obj).ID;
		}
		public override int GetHashCode()
		{
			return ID.GetHashCode();
		}
	}
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
