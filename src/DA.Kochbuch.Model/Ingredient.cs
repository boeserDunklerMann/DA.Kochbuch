using DA.Kochbuch.Model.UnitsTypes;

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
		public virtual Recipe Recipe { get; set; }
	}
}
