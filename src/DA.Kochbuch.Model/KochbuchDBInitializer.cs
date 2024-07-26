using System.Data.Entity;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="26.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class KochbuchDBInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
	{
		protected override void Seed(DatabaseContext context)
		{
			// TODO DA: remove this list in external xml-config
			IList<UnitsTypes.IngredientUnit> units =
			[
				new UnitsTypes.IngredientUnit() { Name = "kg", ID=1 }, new UnitsTypes.IngredientUnit() { Name = "g", ID=2 },
				new UnitsTypes.IngredientUnit(){Name="Stk", ID=3}, new UnitsTypes.IngredientUnit(){Name="Msp", ID=4},
				new UnitsTypes.IngredientUnit(){Name="n.B.", ID=5}
			];
			context.Units.AddRange(units);

			base.Seed(context);
		}
	}
}