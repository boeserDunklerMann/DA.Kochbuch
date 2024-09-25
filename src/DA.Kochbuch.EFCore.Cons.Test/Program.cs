using DA.Kochbuch.Model;
using DA.Kochbuch.Model.Authorization;
using DA.Kochbuch.Model.UnitsTypes;
using System.Text.Json;
using System.Drawing;

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
			InsertData();
			ReadData();
		}

		static byte[] Image2ByteArray(Image image)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				image.Save(ms, image.RawFormat);
				return ms.ToArray();
			}
		}

		static void InsertData()
		{
			using KochbuchContext ctx = new KochbuchContext("Server=192.168.2.108;Database=Kochbuch_dev;Uid=root;Pwd=only4sus;");
			ctx.Database.EnsureCreated();   // create DB if not exists

			// load images...
			Image suppe1 = Image.FromFile(@"D:\git\DA.Kochbuch\temp\einfache-kartoffelsuppe.jpg");
			Image suppe2 = Image.FromFile(@"D:\git\DA.Kochbuch\temp\einfache-kartoffelsuppe (1).jpg");

			// add units
			IngredientUnit unit = BaseModel.Create<IngredientUnit>("kg"); //{ ID = 1, Name = "kg" };
			ctx.Units.Add(unit);
			ctx.Units.AddRange([BaseModel.Create<IngredientUnit>("g"),
				BaseModel.Create<IngredientUnit>("Stk"),
				BaseModel.Create<IngredientUnit>("Msp"),
				BaseModel.Create<IngredientUnit>("n.B.")
			]);
			ctx.SaveChanges();

			// add ingredients
			Ingredient i1 = BaseModel.Create<Ingredient>("Mehl");
			i1.Amount = 500;
			i1.Unit = unit;
			Ingredient i2 = BaseModel.Create<Ingredient>("Ei");
			i2.Amount = 2;
			i2.Unit = unit;
			ctx.Ingredients.Add(i1);
			ctx.Ingredients.Add(i2);
			ctx.SaveChanges();

			// add recipe
			Recipe r = BaseModel.Create<Recipe>("Kartoffelsuppe");
			r.NumberPersons = 3;
			r.CookInstructon = "abc";
			r.ChangeDate = DateTime.UtcNow;
			r.Images.Add(new Recipeimage() { Image = Image2ByteArray(suppe1) });
			r.Images.Add(new Recipeimage() { Image = Image2ByteArray(suppe2) });
			ctx.Recipes.Add(r);
//			ctx.SaveChanges();

			// create user
			User user1 = BaseModel.Create<User>("André");
			User user2 = BaseModel.Create<User>("Die Süße vom Fristo");
			ctx.Users.Add(user1);
			ctx.Users.Add(user2);

			// assign user to recipe
			r.User = user1;
			ctx.SaveChanges();

			// create a number of access Tokens
			Random rand = new Random();
			int max = rand.Next(100);
			for (int i = 0; i < max; i++)
			{
				AccessToken token = BaseModel.Create<AccessToken>($"AT #{i.ToString("000")}");
				ctx.AccessTokens.Add(token);
			}
			// save
			ctx.SaveChanges();
			ctx.Ingredients.ToList().ForEach(r.Ingredients.Add);
			ctx.SaveChanges();
		}

		static void ReadData()
		{
			using (KochbuchContext ctx = new KochbuchContext("Server=192.168.2.108;Database=Kochbuch_dev;Uid=root;Pwd=only4sus;"))
			{
				// TODO: durch das Laden der Rezepte, haben die User dann auch ihre Rezepte bekommen (Z88).
				// unschön, aber ein Workaround, den ich zufällig rausbekommen habe.
				var _ = ctx.Units.ToList();
				ctx.Ingredients.ToList();
				var recipes = ctx.Recipes.ToList();
				var users = ctx.Users/*.Include("Recipes")*/.ToList();  // jetzt haben die Rezepte auch User
				users.ForEach(user =>
				{
					Console.WriteLine(user.Name);
					user.OwnRecipes?.ToList().ForEach(recipe =>
					{
						Console.WriteLine("\t" + recipe.Name);
						recipe.Ingredients.ToList().ForEach(i => Console.WriteLine("\t\t" + i.Name));
						JsonSerializer.Serialize(recipe);
					});
				});
				JsonSerializer.Serialize(recipes);

				// TODO: Das ist zwar umständlich aber funktioniert!
				//var firstUser = ctx.Users.First();
				//ctx.Entry(firstUser)
				//	.Collection(u => u.OwnRecipes).Load();
				//Console.WriteLine(firstUser.Name);
				//foreach(var recipe in firstUser.OwnRecipes)
				//{
    //                Console.WriteLine(recipe.Name);
    //            }
			}
		}
	}
}