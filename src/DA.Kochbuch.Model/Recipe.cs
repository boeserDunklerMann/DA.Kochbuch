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
		public string CookInstructon { get; set; } = string.Empty;
        public override void PopulateMyID()
		{
			throw new NotImplementedException();
		}
	}
}