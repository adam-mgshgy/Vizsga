using System;
using Microsoft.EntityFrameworkCore.Metadata;
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Img_src = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
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
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Colour = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagTraining",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Training_id = table.Column<int>(nullable: false),
                    Tag_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTraining", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Category_id = table.Column<int>(nullable: false),
                    Trainer_id = table.Column<int>(nullable: false),
                    Min_member = table.Column<int>(nullable: false),
                    Max_member = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Contact_phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
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
                    Location_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSession",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainingId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Minutes = table.Column<int>(nullable: false),
                    Address_name = table.Column<string>(maxLength: 255, nullable: false),
                    Place_name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSession_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingSession_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applicant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Training_sessionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicant_TrainingSession_Training_sessionId",
                        column: x => x.Training_sessionId,
                        principalTable: "TrainingSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applicant_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Img_src", "Name" },
                values: new object[,]
                {
                    { 1, "box.jpg", "Box" },
                    { 11, "riding.jpg", "Lovaglás" },
                    { 10, "swimming.jpg", "Úszás" },
                    { 9, "trx.jpg", "TRX" },
                    { 8, "tennis.jpg", "Tenisz" },
                    { 7, "spartan.jpg", "Spartan" },
                    { 12, "yoga.jpg", "Jóga" },
                    { 5, "handball.jpg", "Kézilabda" },
                    { 4, "basketball.jpg", "Kosárlabda" },
                    { 3, "football.jpg", "Labdarúgás" },
                    { 2, "crossFitt.jpg", "Crossfit" },
                    { 6, "volleyball.jpg", "Röplabda" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Colour", "Name" },
                values: new object[,]
                {
                    { 10, "#9984D4", "Köredzés" },
                    { 9, "#373F51", "Rehabilitációs" },
                    { 8, "blue", "Aerobic" },
                    { 7, "#D7263D", "Erőnléti" },
                    { 6, "green", "Személyi edzés" },
                    { 4, "black", "Szabadtéri" },
                    { 3, "red", "Edzőterem" },
                    { 2, "#05A8AA", "Saját testsúlyos" },
                    { 1, "#6610f2", "Csoportos" },
                    { 11, "#F17300", "Bemelegítés" },
                    { 5, "#0dcaf0", "Zsírégető" },
                    { 12, "#3A405A", "Flexibilitás" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_Training_sessionId",
                table: "Applicant",
                column: "Training_sessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_UserId",
                table: "Applicant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_LocationId",
                table: "TrainingSession",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_TrainingId",
                table: "TrainingSession",
                column: "TrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "TagTraining");

            migrationBuilder.DropTable(
                name: "TrainingSession");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Training");
        }
    }
}
