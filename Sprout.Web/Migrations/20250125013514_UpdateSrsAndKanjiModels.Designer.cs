﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sprout.Web.Data;

#nullable disable

namespace Sprout.Web.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20250125013514_UpdateSrsAndKanjiModels")]
    partial class UpdateSrsAndKanjiModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.Property<int>("CardsId")
                        .HasColumnType("int");

                    b.Property<int>("DecksId")
                        .HasColumnType("int");

                    b.HasKey("CardsId", "DecksId");

                    b.HasIndex("DecksId");

                    b.ToTable("CardDeck");
                });

            modelBuilder.Entity("Sprout.Web.Data.Entities.Kanji.Kanji", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Frequency")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int?>("JLPTLevel")
                        .HasColumnType("int");

                    b.PrimitiveCollection<string>("KunReadings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Literal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("Meanings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("NanoriReadings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.PrimitiveCollection<string>("OnReadings")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StrokeCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Kanji");
                });

            modelBuilder.Entity("Sprout.Web.Data.Entities.Review.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Kanji")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Kanji", "UserId")
                        .IsUnique();

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Sprout.Web.Data.Entities.Review.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Decks");
                });

            modelBuilder.Entity("Sprout.Web.Data.Entities.Srs.SrsData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FirstReview")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsMastered")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastReview")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NextReview")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProgressLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId")
                        .IsUnique();

                    b.ToTable("SrsData");
                });

            modelBuilder.Entity("CardDeck", b =>
                {
                    b.HasOne("Sprout.Web.Data.Entities.Review.Card", null)
                        .WithMany()
                        .HasForeignKey("CardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sprout.Web.Data.Entities.Review.Deck", null)
                        .WithMany()
                        .HasForeignKey("DecksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sprout.Web.Data.Entities.Srs.SrsData", b =>
                {
                    b.HasOne("Sprout.Web.Data.Entities.Review.Card", "Card")
                        .WithOne("SrsData")
                        .HasForeignKey("Sprout.Web.Data.Entities.Srs.SrsData", "CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("Sprout.Web.Data.Entities.Review.Card", b =>
                {
                    b.Navigation("SrsData")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
