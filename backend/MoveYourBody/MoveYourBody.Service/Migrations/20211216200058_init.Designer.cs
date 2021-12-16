﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoveYourBody.Service;

namespace MoveYourBody.Service.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211216200058_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MoveYourBody.Service.Models.Category", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Img_src")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Name");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Name = "Box",
                            Img_src = "box.jpg"
                        },
                        new
                        {
                            Name = "Crossfit",
                            Img_src = "crossFitt.jpg"
                        },
                        new
                        {
                            Name = "Labdarúgás",
                            Img_src = "football.jpg"
                        },
                        new
                        {
                            Name = "Kosárlabda",
                            Img_src = "basketball.jpg"
                        },
                        new
                        {
                            Name = "Kézilabda",
                            Img_src = "handball.jpg"
                        },
                        new
                        {
                            Name = "Röplabda",
                            Img_src = "volleyball.jpg"
                        },
                        new
                        {
                            Name = "Spartan",
                            Img_src = "spartan.jpg"
                        },
                        new
                        {
                            Name = "Tenisz",
                            Img_src = "tennis.jpg"
                        },
                        new
                        {
                            Name = "TRX",
                            Img_src = "trx.jpg"
                        },
                        new
                        {
                            Name = "Úszás",
                            Img_src = "swimming.jpg"
                        },
                        new
                        {
                            Name = "Lovaglás",
                            Img_src = "riding.jpg"
                        },
                        new
                        {
                            Name = "Jóga",
                            Img_src = "yoga.jpg"
                        });
                });

            modelBuilder.Entity("MoveYourBody.Service.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address_name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("City_name")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.Property<string>("County_name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("Place_name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("MoveYourBody.Service.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(320) CHARACTER SET utf8mb4")
                        .HasMaxLength(320);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("Phone_number")
                        .IsRequired()
                        .HasColumnType("varchar(12) CHARACTER SET utf8mb4")
                        .HasMaxLength(12);

                    b.Property<bool>("Trainer")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MoveYourBody.Service.Models.User", b =>
                {
                    b.HasOne("MoveYourBody.Service.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
