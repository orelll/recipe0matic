using Infrastructure.Postgres.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres;

public class RecipesDbContext : DbContext
{
    public RecipesDbContext (DbContextOptions<RecipesDbContext> options)
        : base(options)
    {
    }

    public DbSet<RecipeModel> Recipes { get; set; }
    public DbSet<TagModel> Tags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeModel>().ToTable("Recipes");
        modelBuilder.Entity<TagModel>().ToTable("Tags");
    }
}