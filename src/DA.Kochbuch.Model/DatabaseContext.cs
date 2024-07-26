//using Microsoft.EntityFrameworkCore;

using System.Data.Entity;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class DatabaseContext : DbContext
	{
		public DatabaseContext() : base("Kochbuch") // TODO DA: from cfg see: https://www.entityframeworktutorial.net/code-first/database-initialization-in-code-first.aspx
		{
			Database.SetInitializer<DatabaseContext>(new KochbuchDBInitializer());
		}

		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UnitsTypes.IngredientUnit> Units { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// TODO: see here: https://github.com/entityframeworktutorial/EF6-Code-First-Demo/blob/master/EF6CodeFirstDemo/SchoolContext.cs
			base.OnModelCreating(modelBuilder);
		}
	}
}