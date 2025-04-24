using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DA.Kochbuch.Blazor.Server.Model;

public partial class KochbuchContext : DbContext
{
    public KochbuchContext(DbContextOptions<KochbuchContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeImage> RecipeImages { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.RecipeId, "IX_Ingredients_RecipeID");

            entity.HasIndex(e => e.UnitId, "IX_Ingredients_UnitID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangeDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RecipeId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("RecipeID");
            entity.Property(e => e.UnitId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("UnitID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Unit).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.UserId, "IX_Recipes_UserID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangeDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.NumberPersons).HasColumnType("int(11)");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<RecipeImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.RecipeId, "IX_RecipeImages_RecipeID");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangeDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Image).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.RecipeId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("RecipeID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeImages)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangeDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("ID");
            entity.Property(e => e.ChangeDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(6)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.GoogleId).HasColumnName("GoogleID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
