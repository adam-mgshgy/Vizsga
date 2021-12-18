﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveYourBody.Service.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Img_src = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    County_name = table.Column<string>(maxLength: 255, nullable: true),
                    City_name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 320, nullable: false),
                    Full_name = table.Column<string>(maxLength: 255, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    Phone_number = table.Column<string>(maxLength: 12, nullable: false),
                    Trainer = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Name", "Img_src" },
                values: new object[,]
                {
                    { "Box", "box.jpg" },
                    { "Jóga", "yoga.jpg" },
                    { "Lovaglás", "riding.jpg" },
                    { "Úszás", "swimming.jpg" },
                    { "Tenisz", "tennis.jpg" },
                    { "Spartan", "spartan.jpg" },
                    { "TRX", "trx.jpg" },
                    { "Kézilabda", "handball.jpg" },
                    { "Kosárlabda", "basketball.jpg" },
                    { "Labdarúgás", "football.jpg" },
                    { "Crossfit", "crossFitt.jpg" },
                    { "Röplabda", "volleyball.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City_name", "County_name" },
                values: new object[,]
                {
                    { 5, "Budapest", "Pest" },
                    { 1, "Győr", "Győr-Moson-Sporon" },
                    { 2, "Sopron", "Győr-Moson-Sporon" },
                    { 3, "Komárom", "Komárom-Esztergom" },
                    { 4, "Esztergom", "Komárom-Esztergom" },
                    { 6, "Pápa", "Veszprém" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_LocationId",
                table: "User",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}