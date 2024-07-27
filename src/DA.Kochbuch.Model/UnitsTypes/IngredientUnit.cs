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
	public class IngredientUnit : BaseModel
	{
		public virtual ICollection<Ingredient> Ingredients { get; set; }
        public IngredientUnit()
        {
			ChangeDate = DateTime.Now;
        }
        public override void PopulateMyID()
		{
			throw new NotImplementedException();
		}
	}
}
