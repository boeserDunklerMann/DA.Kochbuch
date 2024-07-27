namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="25.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class User : BaseModel
	{
		public List<Recipe> Recipes { get; set; } = new List<Recipe> { };

		public override void PopulateMyID()
		{
			throw new NotImplementedException();
		}
	}
}