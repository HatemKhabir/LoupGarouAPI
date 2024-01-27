﻿// <auto-generated />
using System;
using LoupGarou.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoupGarou.Data.Migrations
{
    [DbContext(typeof(LoupGarouDbContext))]
    [Migration("20240127005256_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoupGarou.Model.Character", b =>
                {
                    b.Property<Guid>("CharacterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CharacterID");

                    b.HasIndex("GameId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("LoupGarou.Model.Game", b =>
                {
                    b.Property<string>("GameId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("LoupGarou.Model.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("GameId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LoupGarou.Model.Character", b =>
                {
                    b.HasOne("LoupGarou.Model.Game", null)
                        .WithMany("Characters")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("LoupGarou.Model.Player", b =>
                {
                    b.HasOne("LoupGarou.Model.Game", null)
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LoupGarou.Model.Game", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}