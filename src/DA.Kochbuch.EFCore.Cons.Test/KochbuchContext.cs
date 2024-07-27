using DA.Kochbuch.Model.UnitsTypes;
using DA.Kochbuch.Model;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace DA.Kochbuch.EFCore.Cons.Test
{
	/// <ChangeLog>
	/// <Create Datum="27.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <see href="https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-example.html"/>
	public class KochbuchContext : DbContext
	{
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<IngredientUnit> Units { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySQL("Server=localhost;Database=Kochbuch_dev;Uid=root;Pwd=only4sus;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<IngredientUnit>(entity =>
			{
				entity.HasKey(iu => iu.ID);
				entity.Property(iu => iu.Name).IsRequired();
			});
			modelBuilder.Entity<Recipe>(entity =>
			{
				entity.HasKey(r => r.ID);
				entity.Property(r => r.CookInstructon).IsRequired();
				entity.HasOne(r => r.User).WithMany(u => u.Recipes);
			});
			modelBuilder.Entity<Ingredient>(entity =>
			{
				entity.HasKey(i => i.ID);
				entity.Property(i => i.Name).IsRequired();
				entity.HasOne(i => i.Recipe).WithMany(r => r.Ingredients);
				entity.HasOne(i => i.Unit).WithMany(iu => iu.Ingredients);
			});
			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey(u => u.ID);
				entity.Property(u => u.Name).IsRequired();
			});
		}
	}
}
