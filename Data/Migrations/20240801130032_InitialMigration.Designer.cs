﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokedex.Data;

#nullable disable

namespace PokemonApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240801130032_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Pokedex.Data.Model.MasterPokemonModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("MasterPokemons");
                });

            modelBuilder.Entity("Pokedex.Data.Model.MasterPokemonPokemon", b =>
                {
                    b.Property<int>("MasterPokemonModelId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PokemonModelId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MasterPokemonModelId", "PokemonModelId");

                    b.HasIndex("PokemonModelId");

                    b.ToTable("MasterPokemonPokemons");
                });

            modelBuilder.Entity("Pokedex.Data.Model.PokemonModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Evolutions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sprite")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("Pokedex.Data.Model.MasterPokemonPokemon", b =>
                {
                    b.HasOne("Pokedex.Data.Model.MasterPokemonModel", "MasterPokemonModel")
                        .WithMany()
                        .HasForeignKey("MasterPokemonModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pokedex.Data.Model.PokemonModel", "PokemonModel")
                        .WithMany()
                        .HasForeignKey("PokemonModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MasterPokemonModel");

                    b.Navigation("PokemonModel");
                });
#pragma warning restore 612, 618
        }
    }
}