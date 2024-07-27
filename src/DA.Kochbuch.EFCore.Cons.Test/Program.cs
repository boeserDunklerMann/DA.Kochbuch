using DA.Kochbuch.Model;
using DA.Kochbuch.Model.UnitsTypes;

namespace DA.Kochbuch.EFCore.Cons.Test
{
	/// <ChangeLog>
	/// <Create Datum="27.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			InsertData();
		}

		static void InsertData()
		{
			using KochbuchContext ctx = new KochbuchContext();
			ctx.Database.EnsureCreated();   // create DB if not exists
			
			// add units
			IngredientUnit unit = new IngredientUnit() { ID = 1, Name = "kg" };
			ctx.Units.Add(unit);
			ctx.Units.AddRange([new IngredientUnit() { ID = 2, Name = "g" },
				new IngredientUnit(){ID=3, Name = "Stk"},
				new IngredientUnit(){ID=4, Name="Msp"},
				new IngredientUnit(){ID=5, Name="n.B."}
			]);
			// add recipe
			/* this must fail - missing instructions */
			Recipe r = new Recipe()
				{
					ID = 1,
					Name = "Kartoffelsuppe",
					NumberPersons = 3
				};
			ctx.Recipes.Add(r);
			// create user
			User user = new User()
			{
				ID = 1,
				Name = "André",
				ChangeDate = DateTime.Now
			};
			// assign user to recipe
			r.User = user;
			// save
			ctx.SaveChanges();
		}
	}
}