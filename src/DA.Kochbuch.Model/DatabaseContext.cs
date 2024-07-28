//using Microsoft.EntityFrameworkCore;

using System.Data.Entity;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class DatabaseContext : DbContext
	{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable. AD: Darum kümmert sich EFCore
		public DatabaseContext() : base("Kochbuch") // TODO DA: from cfg see: https://www.entityframeworktutorial.net/code-first/database-initialization-in-code-first.aspx
		{
			Database.SetInitializer<DatabaseContext>(new KochbuchDBInitializer());
		}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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