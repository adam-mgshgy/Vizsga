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
                    City_name = table.Column<string>(maxLength: 50, nullable: false),
                    Place_name = table.Column<string>(maxLength: 255, nullable: false),
                    County_name = table.Column<string>(maxLength: 255, nullable: false),
                    Address_name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(maxLength: 320, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    Phone_number = table.Column<string>(maxLength: 12, nullable: false),
                    Trainer = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Location_LocationId",
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
                    { "Crossfit", "crossFitt.jpg" },
                    { "Labdarúgás", "football.jpg" },
                    { "Kosárlabda", "basketball.jpg" },
                    { "Kézilabda", "handball.jpg" },
                    { "Röplabda", "volleyball.jpg" },
                    { "Spartan", "spartan.jpg" },
                    { "Tenisz", "tennis.jpg" },
                    { "TRX", "trx.jpg" },
                    { "Úszás", "swimming.jpg" },
                    { "Lovaglás", "riding.jpg" },
                    { "Jóga", "yoga.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
