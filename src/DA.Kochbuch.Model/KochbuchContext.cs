using DA.Kochbuch.Model.Authorization;
using DA.Kochbuch.Model.UnitsTypes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="27.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	/// <see href="https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-example.html"/>
	public class KochbuchContext(string connectionString) : DbContext
	{
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<IngredientUnit> Units { get; set; }
		public DbSet<AccessToken> AccessTokens { get; set; }
		public DbSet<Recipeimage> RecipeImages { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// https://stackoverflow.com/questions/74060289/mysqlconnection-open-system-invalidcastexception-object-cannot-be-cast-from-d
			// MariaDB 11+ doesnt work because of nullable PKs?
			optionsBuilder
				.UseMySQL(connectionString);    // captaintrips with Mariadb 10
			//this.SavingChanges += OnSavingChanges;
			//this.ChangeTracker.StateChanged += OnStateChanged;
		}

		private void OnStateChanged(object? sender, Microsoft.EntityFrameworkCore.ChangeTracking.EntityStateChangedEventArgs e)
		{
			// TODO AD: https://learn.microsoft.com/de-de/ef/core/logging-events-diagnostics/events
		}

		private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
		{
			// TODO AD: https://learn.microsoft.com/de-de/ef/core/logging-events-diagnostics/events
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
				entity.HasOne(r => r.User).WithMany(u => u.OwnRecipes);
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
				entity.HasMany(u => u.OwnRecipes).WithOne(r=>r.User);
			});
			modelBuilder.Entity<AccessToken>(entity =>
			{
				entity.HasKey(at => at.ID);
				entity.Property(at=>at.Lifetime).IsRequired();
			});
		}
	}
}
