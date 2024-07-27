using DA.Kochbuch.Model;
using DA.Kochbuch.Model.UnitsTypes;
using MySql.Data.EntityFramework;
using System.Data.Entity;

namespace DA.Kochbuch.Api
{
	/// <ChangeLog>
	/// <Create Datum="27.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	[DbConfigurationType(typeof(MySqlEFConfiguration))]
	public class DatabaseContext : DbContext
	{
		public DatabaseContext() : base("Kochbuch") // TODO DA: from cfg see: https://www.entityframeworktutorial.net/code-first/database-initialization-in-code-first.aspx
		{
		}
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<IngredientUnit> Units { get; set; }
	}
}