﻿// <auto-generated />
using Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Postgres.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    partial class RecipesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Postgres.Models.IngredientModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RecipeModelId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipeModelId");

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Postgres.Models.RecipeModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Postgres.Models.TagModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("RecipeModelId")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RecipeModelId");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Postgres.Models.IngredientModel", b =>
                {
                    b.HasOne("Infrastructure.Postgres.Models.RecipeModel", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeModelId");
                });

            modelBuilder.Entity("Infrastructure.Postgres.Models.TagModel", b =>
                {
                    b.HasOne("Infrastructure.Postgres.Models.RecipeModel", null)
                        .WithMany("Tags")
                        .HasForeignKey("RecipeModelId");
                });

            modelBuilder.Entity("Infrastructure.Postgres.Models.RecipeModel", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}