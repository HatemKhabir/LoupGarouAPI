﻿// <auto-generated />
using System;
using LoupGarou.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoupGarou.Migrations
{
    [DbContext(typeof(LoupGarouDbContext))]
    [Migration("20240614200732_updatedActionsName")]
    partial class updatedActionsName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LoupGarou.Model.Action", b =>
                {
                    b.Property<Guid>("ActionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ActionType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ActionId");

                    b.HasIndex("GameId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("LoupGarou.Model.Card", b =>
                {
                    b.Property<Guid>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CardId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("LoupGarou.Model.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentPhase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("LoupGarou.Model.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("IsDead")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsLover")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsProtected")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("GameId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LoupGarou.Model.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId");

                    b.HasIndex("CardId");

                    b.HasIndex("GameId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("LoupGarou.Model.Vote", b =>
                {
                    b.Property<Guid>("VoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TargetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VoterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("VotingSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VoteId");

                    b.HasIndex("VotingSessionId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("LoupGarou.Model.VotingSession", b =>
                {
                    b.Property<Guid>("VotingSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExpectedVotesCount")
                        .HasColumnType("int");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("Result")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("VotingSessionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VotingSessionId");

                    b.HasIndex("GameId");

                    b.ToTable("VotingSessions");
                });

            modelBuilder.Entity("LoupGarou.Model.Action", b =>
                {
                    b.HasOne("LoupGarou.Model.Game", null)
                        .WithMany("Actions")
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("LoupGarou.Model.Player", b =>
                {
                    b.HasOne("LoupGarou.Model.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("LoupGarou.Model.Role", b =>
                {
                    b.HasOne("LoupGarou.Model.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoupGarou.Model.Game", "Game")
                        .WithMany("Roles")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("LoupGarou.Model.Vote", b =>
                {
                    b.HasOne("LoupGarou.Model.VotingSession", "VotingSession")
                        .WithMany("Votes")
                        .HasForeignKey("VotingSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VotingSession");
                });

            modelBuilder.Entity("LoupGarou.Model.VotingSession", b =>
                {
                    b.HasOne("LoupGarou.Model.Game", "Game")
                        .WithMany("VotingSessions")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("LoupGarou.Model.Game", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Players");

                    b.Navigation("Roles");

                    b.Navigation("VotingSessions");
                });

            modelBuilder.Entity("LoupGarou.Model.VotingSession", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
