﻿// <auto-generated />
using System;
using LudoEngine.DbModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LudoEngine.Migrations
{
    [DbContext(typeof(StarWarsContext))]
    [Migration("20210330093620_ThirdMigration")]
    partial class ThirdMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "6.0.0-preview.2.21154.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LudoEngine.Models.GameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FirstPlace")
                        .HasColumnType("int");

                    b.Property<int>("ForthPlace")
                        .HasColumnType("int");

                    b.Property<int>("SecondPlace")
                        .HasColumnType("int");

                    b.Property<int>("ThirdPlace")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GameResults");
                });

            modelBuilder.Entity("LudoEngine.Models.GameState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentPlayer")
                        .HasColumnType("int");

                    b.Property<int?>("PawnID")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PawnID");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameStates");
                });

            modelBuilder.Entity("LudoEngine.Models.Pawn", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("XPosition")
                        .HasColumnType("int");

                    b.Property<int>("YPosition")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Pawn");
                });

            modelBuilder.Entity("LudoEngine.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LudoEngine.Models.GameState", b =>
                {
                    b.HasOne("LudoEngine.Models.Pawn", "Pawn")
                        .WithMany("GameStates")
                        .HasForeignKey("PawnID");

                    b.HasOne("LudoEngine.Models.Player", "Player")
                        .WithMany("GameStates")
                        .HasForeignKey("PlayerId");

                    b.Navigation("Pawn");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("LudoEngine.Models.Pawn", b =>
                {
                    b.Navigation("GameStates");
                });

            modelBuilder.Entity("LudoEngine.Models.Player", b =>
                {
                    b.Navigation("GameStates");
                });
#pragma warning restore 612, 618
        }
    }
}
