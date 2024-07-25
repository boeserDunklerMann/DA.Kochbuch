namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="25.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class User : BaseModel
	{
		public ICollection<Recipe> Recipes { get; set; }

		public override void PopulateMyID()
		{
			throw new NotImplementedException();
		}
	}
}