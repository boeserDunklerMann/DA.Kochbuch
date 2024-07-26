using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="26.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class KochbuchDBInitializer : DropCreateDatabaseAlways<DatabaseContext>
	{
		protected override void Seed(DatabaseContext context)
		{
			IList<UnitsTypes.IngredientUnit> units =
			[
				new UnitsTypes.IngredientUnit() { Name = "kg", ID=Guid.NewGuid().ToString() }, new UnitsTypes.IngredientUnit() { Name = "g", ID=Guid.NewGuid().ToString() },
				new UnitsTypes.IngredientUnit(){Name="Stk", ID=Guid.NewGuid().ToString()}, new UnitsTypes.IngredientUnit(){Name="Msp", ID=Guid.NewGuid().ToString()},
				new UnitsTypes.IngredientUnit(){Name="n.B.", ID=Guid.NewGuid().ToString()}
			];
			context.Units.AddRange(units);

			base.Seed(context);
		}
	}
}