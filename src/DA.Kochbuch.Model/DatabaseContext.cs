﻿using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.Model
{
	/// <ChangeLog>
	/// <Create Datum="24.07.2024" Entwickler="DA" />
	/// </ChangeLog>
	public class DatabaseContext : DbContext
	{
		public DatabaseContext() : base()
		{
		}
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<User> Users { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// TODO: see here: https://github.com/entityframeworktutorial/EF6-Code-First-Demo/blob/master/EF6CodeFirstDemo/SchoolContext.cs
			base.OnModelCreating(modelBuilder);
		}
	}
}