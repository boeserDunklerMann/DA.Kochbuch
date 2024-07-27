using DA.Kochbuch.Model;
using DA.Kochbuch.Model.UnitsTypes;
using System.Data.Entity;

namespace DA.Kochbuch.EFCore.Cons.Test
{
	/// <ChangeLog>
	/// <Create Datum="27.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <see href="https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-example.html"/>
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
			Console.WriteLine("Drop database before running");
			//InsertData();
			ReadData();
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
			
			// add ingredients
			Ingredient i1 = new Ingredient()
			{
				ID = 1,
				Amount = 500,
				Unit = unit,
				Name = "Mehl",
				ChangeDate = DateTime.Now
			};
			ctx.Ingredients.Add(i1);

			// add recipe
			/* this must fail - missing instructions */
			Recipe r = new Recipe()
			{
				ID = 1,
				Name = "Kartoffelsuppe",
				NumberPersons = 3,
				CookInstructon = "abc"
			};
			ctx.Recipes.Add(r);

			// create user
			User user = new User()
			{
				ID = 1,
				Name = "André",
				ChangeDate = DateTime.Now
			};
			ctx.Users.Add(user);

			// assign user to recipe
			r.User = user;
			r.Ingredients.Add(i1);

			// save
			ctx.SaveChanges();
		}

		static void ReadData()
		{
			using (KochbuchContext ctx = new KochbuchContext())
			{
				// TODO: durch das Laen der Rezepte, haben die User dann auch ihre Rezepte bekommen (Z66).
				// unschön, aber ein Workaround, den ich zufällig rausbekommen habe.
				var _ = ctx.Units.ToList();
				ctx.Ingredients.ToList();
				var recipes = ctx.Recipes.Include(r => r.User).ToList();
				var users = ctx.Users.Include("Recipes").ToList();	// jetzt haben die Rezepte auch User

				// TODO: Das ist zwar umständlich aber funktioniert!
				var firstUser = ctx.Users.First();
				ctx.Entry(firstUser)
					.Collection(u => u.Recipes).Load();
				Console.WriteLine(firstUser.Name);
				foreach(var recipe in firstUser.Recipes)
				{
                    Console.WriteLine(recipe.Name);
                }

				//foreach(var user in users)
				//{
				//	Console.WriteLine(user.Name);
				//	foreach(var recipe in user.Recipes)
				//	{
    //                    Console.WriteLine(recipe.Name);
    //                }
				//}

				//var r = ctx.Recipes.Find(1);
				//foreach(Recipe recipe in recipes)
				//{
				//	Console.WriteLine($"title: {recipe.Name}");
				//	Console.WriteLine($"owner: {recipe.User.Name}");
    //            }
			}
		}
	}
}