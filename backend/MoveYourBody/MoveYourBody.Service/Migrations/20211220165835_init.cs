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

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CategoryName = table.Column<string>(nullable: false),
                    TrainerId = table.Column<int>(nullable: false),
                    Min_member = table.Column<int>(nullable: false),
                    Max_member = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Contact_phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Category_CategoryName",
                        column: x => x.CategoryName,
                        principalTable: "Category",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Training_User_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagTraining",
                columns: table => new
                {
                    TrainingId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TagTraining_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTraining_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Training_sessionId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
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
                columns: new[] { "Name", "Img_src" },
                values: new object[,]
                {
                    { "Box", "box.jpg" },
                    { "Jóga", "yoga.jpg" },
                    { "Lovaglás", "riding.jpg" },
                    { "TRX", "trx.jpg" },
                    { "Tenisz", "tennis.jpg" },
                    { "Spartan", "spartan.jpg" },
                    { "Úszás", "swimming.jpg" },
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
                    { 230, "Pacsa", "Zala" },
                    { 236, "Pécel", "Pest" },
                    { 235, "Pásztó", "Nógrád" },
                    { 234, "Pápa", "Veszprém" },
                    { 233, "Pannonhalma", "Győr-Moson-Sopron" },
                    { 232, "Pálháza", "Borsod-Abaúj-Zemplén" },
                    { 231, "Paks", "Tolna" },
                    { 229, "Örkény", "Pest" },
                    { 223, "Onga", "Borsod-Abaúj-Zemplén" },
                    { 227, "Őrbottyán", "Pest" },
                    { 226, "Ózd", "Borsod-Abaúj-Zemplén" },
                    { 225, "Oroszlány", "Komárom-Esztergom" },
                    { 224, "Orosháza", "Békés" },
                    { 237, "Pécs", "Baranya" },
                    { 222, "Ócsa", "Pest" },
                    { 221, "Nyírtelek", "Szabolcs-Szatmár-Bereg" },
                    { 220, "Nyírmada", "Szabolcs-Szatmár-Bereg" },
                    { 228, "Őriszentpéter", "Vas" },
                    { 238, "Pécsvárad", "Baranya" },
                    { 244, "Polgárdi", "Fejér" },
                    { 240, "Pilis", "Pest" },
                    { 258, "Salgótarján", "Nógrád" },
                    { 257, "Sajószentpéter", "Borsod-Abaúj-Zemplén" },
                    { 256, "Sajóbábony", "Borsod-Abaúj-Zemplén" },
                    { 255, "Rudabánya", "Borsod-Abaúj-Zemplén" },
                    { 254, "Rétság", "Nógrád" },
                    { 253, "Répcelak", "Vas" },
                    { 252, "Rákóczifalva", "Jász-Nagykun-Szolnok" },
                    { 251, "Rakamaz", "Szabolcs-Szatmár-Bereg" },
                    { 239, "Pétervására", "Heves" },
                    { 250, "Ráckeve", "Pest" },
                    { 248, "Püspökladány", "Hajdú-Bihar" },
                    { 247, "Putnok", "Borsod-Abaúj-Zemplén" },
                    { 246, "Pusztaszabolcs", "Fejér" },
                    { 245, "Pomáz", "Pest" },
                    { 219, "Nyírlugos", "Szabolcs-Szatmár-Bereg" },
                    { 243, "Polgár", "Hajdú-Bihar" },
                    { 242, "Pilisvörösvár", "Pest" },
                    { 241, "Piliscsaba", "Pest" },
                    { 249, "Rácalmás", "Fejér" },
                    { 218, "Nyíregyháza", "Szabolcs-Szatmár-Bereg" },
                    { 212, "Nagymaros", "Pest" },
                    { 216, "Nyírbátor", "Szabolcs-Szatmár-Bereg" },
                    { 193, "Mezőkövesd", "Borsod-Abaúj-Zemplén" },
                    { 192, "Mezőkovácsháza", "Békés" },
                    { 191, "Mezőkeresztes", "Borsod-Abaúj-Zemplén" },
                    { 190, "Mezőhegyes", "Békés" },
                    { 189, "Mezőcsát", "Borsod-Abaúj-Zemplén" },
                    { 188, "Mezőberény", "Békés" },
                    { 187, "Mélykút", "Bács-Kiskun" },
                    { 186, "Medgyesegyháza", "Békés" },
                    { 194, "Mezőtúr", "Jász-Nagykun-Szolnok" },
                    { 185, "Mátészalka", "Szabolcs-Szatmár-Bereg" },
                    { 183, "Martfű", "Jász-Nagykun-Szolnok" },
                    { 182, "Máriapócs", "Szabolcs-Szatmár-Bereg" },
                    { 181, "Marcali", "Somogy" },
                    { 180, "Mándok", "Szabolcs-Szatmár-Bereg" },
                    { 179, "Makó", "Csongrád-Csanád" },
                    { 178, "Mágocs", "Baranya" },
                    { 177, "Maglód", "Pest" },
                    { 176, "Lőrinci", "Heves" },
                    { 184, "Martonvásár", "Fejér" },
                    { 217, "Nyírbogát", "Szabolcs-Szatmár-Bereg" },
                    { 195, "Mindszent", "Csongrád-Csanád" },
                    { 197, "Mohács", "Baranya" },
                    { 215, "Nyíradony", "Hajdú-Bihar" },
                    { 214, "Nyergesújfalu", "Komárom-Esztergom" },
                    { 213, "Nyékládháza", "Borsod-Abaúj-Zemplén" },
                    { 259, "Sándorfalva", "Csongrád-Csanád" },
                    { 211, "Nagymányok", "Tolna" },
                    { 210, "Nagykőrös", "Pest" },
                    { 209, "Nagykáta", "Pest" },
                    { 208, "Nagykanizsa", "Zala" },
                    { 196, "Miskolc", "Borsod-Abaúj-Zemplén" },
                    { 207, "Nagykálló", "Szabolcs-Szatmár-Bereg" },
                    { 205, "Nagyecsed", "Szabolcs-Szatmár-Bereg" },
                    { 204, "Nagybajom", "Somogy" },
                    { 203, "Nagyatád", "Somogy" },
                    { 202, "Nádudvar", "Hajdú-Bihar" },
                    { 201, "Mosonmagyaróvár", "Győr-Moson-Sopron" },
                    { 200, "Mórahalom", "Csongrád-Csanád" },
                    { 199, "Mór", "Fejér" },
                    { 198, "Monor", "Pest" },
                    { 206, "Nagyhalász", "Szabolcs-Szatmár-Bereg" },
                    { 260, "Sárbogárd", "Fejér" },
                    { 266, "Sellye", "Baranya" },
                    { 262, "Sárospatak", "Borsod-Abaúj-Zemplén" },
                    { 324, "Újszász", "Jász-Nagykun-Szolnok" },
                    { 323, "Újkígyós", "Békés" },
                    { 322, "Újhartyán", "Pest" },
                    { 321, "Újfehértó", "Szabolcs-Szatmár-Bereg" },
                    { 320, "Túrkeve", "Jász-Nagykun-Szolnok" },
                    { 319, "Tura", "Pest" },
                    { 318, "Törökszentmiklós", "Jász-Nagykun-Szolnok" },
                    { 317, "Törökbálint", "Pest" },
                    { 325, "Üllő", "Pest" },
                    { 316, "Tököl", "Pest" },
                    { 314, "Tompa", "Bács-Kiskun" },
                    { 313, "Tolna", "Tolna" },
                    { 312, "Tokaj", "Borsod-Abaúj-Zemplén" },
                    { 311, "Tiszavasvári", "Szabolcs-Szatmár-Bereg" },
                    { 310, "Tiszaújváros", "Borsod-Abaúj-Zemplén" },
                    { 309, "Tiszalök", "Szabolcs-Szatmár-Bereg" },
                    { 308, "Tiszakécske", "Bács-Kiskun" },
                    { 307, "Tiszafüred", "Jász-Nagykun-Szolnok" },
                    { 315, "Tótkomlós", "Békés" },
                    { 306, "Tiszaföldvár", "Jász-Nagykun-Szolnok" },
                    { 326, "Vác", "Pest" },
                    { 328, "Vámospércs", "Hajdú-Bihar" },
                    { 346, "Zamárdi", "Somogy" },
                    { 345, "Zalaszentgrót", "Zala" },
                    { 344, "Zalalövő", "Zala" },
                    { 343, "Zalakaros", "Zala" },
                    { 342, "Zalaegerszeg", "Zala" },
                    { 341, "Záhony", "Szabolcs-Szatmár-Bereg" },
                    { 340, "Visegrád", "Pest" },
                    { 339, "Villány", "Baranya" },
                    { 327, "Vaja", "Szabolcs-Szatmár-Bereg" },
                    { 338, "Vésztő", "Békés" },
                    { 336, "Verpelét", "Heves" },
                    { 335, "Veresegyház", "Pest" },
                    { 334, "Vép", "Vas" },
                    { 333, "Velence", "Fejér" },
                    { 332, "Vecsés", "Pest" },
                    { 331, "Vasvár", "Vas" },
                    { 330, "Vásárosnamény", "Szabolcs-Szatmár-Bereg" },
                    { 329, "Várpalota", "Veszprém" },
                    { 337, "Veszprém", "Veszprém" },
                    { 261, "Sarkad", "Békés" },
                    { 305, "Tiszacsege", "Hajdú-Bihar" },
                    { 303, "Téglás", "Hajdú-Bihar" },
                    { 280, "Szeghalom", "Békés" },
                    { 279, "Szeged", "Csongrád-Csanád" },
                    { 278, "Szécsény", "Nógrád" },
                    { 277, "Százhalombatta", "Pest" },
                    { 276, "Szarvas", "Békés" },
                    { 275, "Szabadszállás", "Bács-Kiskun" },
                    { 274, "Sümeg", "Veszprém" },
                    { 273, "Sülysáp", "Pest" },
                    { 281, "Székesfehérvár", "Fejér" },
                    { 272, "Sopron", "Győr-Moson-Sopron" },
                    { 270, "Solt", "Bács-Kiskun" },
                    { 269, "Siófok", "Somogy" },
                    { 268, "Simontornya", "Tolna" },
                    { 267, "Siklós", "Baranya" },
                    { 175, "Letenye", "Zala" },
                    { 265, "Sátoraljaújhely", "Borsod-Abaúj-Zemplén" },
                    { 264, "Sásd", "Baranya" },
                    { 263, "Sárvár", "Vas" },
                    { 271, "Soltvadkert", "Bács-Kiskun" },
                    { 304, "Tét", "Győr-Moson-Sopron" },
                    { 282, "Szekszárd", "Tolna" },
                    { 284, "Szentendre", "Pest" },
                    { 302, "Tatabánya", "Komárom-Esztergom" },
                    { 301, "Tata", "Komárom-Esztergom" },
                    { 300, "Tát", "Komárom-Esztergom" },
                    { 299, "Tapolca", "Veszprém" },
                    { 298, "Tápiószele", "Pest" },
                    { 297, "Tamási", "Tolna" },
                    { 296, "Tab", "Somogy" },
                    { 295, "Szombathely", "Vas" },
                    { 283, "Szendrő", "Borsod-Abaúj-Zemplén" },
                    { 294, "Szolnok", "Jász-Nagykun-Szolnok" },
                    { 292, "Szikszó", "Borsod-Abaúj-Zemplén" },
                    { 291, "Szigetvár", "Baranya" },
                    { 290, "Szigetszentmiklós", "Pest" },
                    { 289, "Szigethalom", "Pest" },
                    { 288, "Szerencs", "Borsod-Abaúj-Zemplén" },
                    { 287, "Szentlőrinc", "Baranya" },
                    { 286, "Szentgotthárd", "Vas" },
                    { 285, "Szentes", "Csongrád-Csanád" },
                    { 293, "Szob", "Pest" },
                    { 174, "Létavértes", "Hajdú-Bihar" },
                    { 168, "Kunszentmiklós", "Bács-Kiskun" },
                    { 172, "Lengyeltóti", "Somogy" },
                    { 61, "Dabas", "Pest" },
                    { 60, "Csurgó", "Somogy" },
                    { 59, "Csorvás", "Békés" },
                    { 58, "Csorna", "Győr-Moson-Sopron" },
                    { 57, "Csongrád", "Csongrád-Csanád" },
                    { 56, "Csepreg", "Vas" },
                    { 55, "Csenger", "Szabolcs-Szatmár-Bereg" },
                    { 54, "Csanádpalota", "Csongrád-Csanád" },
                    { 53, "Csákvár", "Fejér" },
                    { 52, "Cigánd", "Borsod-Abaúj-Zemplén" },
                    { 51, "Celldömölk", "Vas" },
                    { 50, "Cegléd", "Pest" },
                    { 49, "Bük", "Vas" },
                    { 48, "Budapest", "Budapest" },
                    { 47, "Budaörs", "Pest" },
                    { 46, "Budakeszi", "Pest" },
                    { 45, "Budakalász", "Pest" },
                    { 62, "Debrecen", "Hajdú-Bihar" },
                    { 63, "Demecser", "Szabolcs-Szatmár-Bereg" },
                    { 64, "Derecske", "Hajdú-Bihar" },
                    { 65, "Dévaványa", "Békés" },
                    { 83, "Ercsi", "Fejér" },
                    { 82, "Enying", "Fejér" },
                    { 81, "Encs", "Borsod-Abaúj-Zemplén" },
                    { 80, "Emőd", "Borsod-Abaúj-Zemplén" },
                    { 79, "Elek", "Békés" },
                    { 78, "Eger", "Heves" },
                    { 77, "Edelény", "Borsod-Abaúj-Zemplén" },
                    { 76, "Dunavecse", "Bács-Kiskun" },
                    { 44, "Borsodnádasd", "Borsod-Abaúj-Zemplén" },
                    { 75, "Dunavarsány", "Pest" },
                    { 73, "Dunakeszi", "Pest" },
                    { 72, "Dunaharaszti", "Pest" },
                    { 71, "Dunaföldvár", "Tolna" },
                    { 70, "Dorog", "Komárom-Esztergom" },
                    { 69, "Dombrád", "Szabolcs-Szatmár-Bereg" },
                    { 68, "Dombóvár", "Tolna" },
                    { 67, "Diósd", "Pest" },
                    { 66, "Devecser", "Veszprém" },
                    { 74, "Dunaújváros", "Fejér" },
                    { 43, "Bonyhád", "Tolna" },
                    { 42, "Bóly", "Baranya" },
                    { 41, "Bodajk", "Fejér" },
                    { 18, "Balatonalmádi", "Veszprém" },
                    { 17, "Balassagyarmat", "Nógrád" },
                    { 16, "Baktalórántháza", "Szabolcs-Szatmár-Bereg" },
                    { 15, "Baja", "Bács-Kiskun" },
                    { 14, "Badacsonytomaj", "Veszprém" },
                    { 13, "Bácsalmás", "Bács-Kiskun" },
                    { 12, "Bábolna", "Komárom-Esztergom" },
                    { 11, "Aszód", "Pest" },
                    { 19, "Balatonboglár", "Somogy" },
                    { 10, "Alsózsolca", "Borsod-Abaúj-Zemplén" },
                    { 8, "Ajka", "Veszprém" },
                    { 7, "Ajak", "Szabolcs-Szatmár-Bereg" },
                    { 6, "Adony", "Fejér" },
                    { 5, "Ács", "Komárom-Esztergom" },
                    { 4, "Abony", "Pest" },
                    { 3, "Abaújszántó", "Borsod-Abaúj-Zemplén" },
                    { 2, "Abádszalók", "Jász-Nagykun-Szolnok" },
                    { 1, "Aba", "Fejér" },
                    { 9, "Albertirsa", "Pest" },
                    { 84, "Érd", "Pest" },
                    { 20, "Balatonföldvár", "Somogy" },
                    { 22, "Balatonfűzfő", "Veszprém" },
                    { 40, "Biharkeresztes", "Hajdú-Bihar" },
                    { 39, "Bicske", "Fejér" },
                    { 38, "Biatorbágy", "Pest" },
                    { 37, "Besenyszög", "Jász-Nagykun-Szolnok" },
                    { 36, "Berhida", "Veszprém" },
                    { 35, "Berettyóújfalu", "Hajdú-Bihar" },
                    { 34, "Beled", "Győr-Moson-Sopron" },
                    { 33, "Bélapátfalva", "Heves" },
                    { 21, "Balatonfüred", "Veszprém" },
                    { 32, "Békéscsaba", "Békés" },
                    { 30, "Battonya", "Békés" },
                    { 29, "Bátonyterenye", "Nógrád" },
                    { 28, "Bátaszék", "Tolna" },
                    { 27, "Barcs", "Somogy" },
                    { 26, "Balmazújváros", "Hajdú-Bihar" },
                    { 25, "Balkány", "Szabolcs-Szatmár-Bereg" },
                    { 24, "Balatonlelle", "Somogy" },
                    { 23, "Balatonkenese", "Veszprém" },
                    { 31, "Békés", "Békés" },
                    { 85, "Esztergom", "Komárom-Esztergom" },
                    { 86, "Fegyvernek", "Jász-Nagykun-Szolnok" },
                    { 87, "Fehérgyarmat", "Szabolcs-Szatmár-Bereg" },
                    { 149, "Kiskőrös", "Bács-Kiskun" },
                    { 148, "Kisköre", "Heves" },
                    { 147, "Kisbér", "Komárom-Esztergom" },
                    { 146, "Keszthely", "Zala" },
                    { 145, "Kerepes", "Pest" },
                    { 144, "Kerekegyháza", "Bács-Kiskun" },
                    { 143, "Kenderes", "Jász-Nagykun-Szolnok" },
                    { 142, "Kemecse", "Szabolcs-Szatmár-Bereg" },
                    { 150, "Kiskunfélegyháza", "Bács-Kiskun" },
                    { 141, "Kecskemét", "Bács-Kiskun" },
                    { 139, "Kazincbarcika", "Borsod-Abaúj-Zemplén" },
                    { 138, "Karcag", "Jász-Nagykun-Szolnok" },
                    { 137, "Kapuvár", "Győr-Moson-Sopron" },
                    { 136, "Kaposvár", "Somogy" },
                    { 135, "Kalocsa", "Bács-Kiskun" },
                    { 134, "Kadarkút", "Somogy" },
                    { 133, "Kaba", "Hajdú-Bihar" },
                    { 132, "Jászkisér", "Jász-Nagykun-Szolnok" },
                    { 140, "Kecel", "Bács-Kiskun" },
                    { 131, "Jászfényszaru", "Jász-Nagykun-Szolnok" },
                    { 151, "Kiskunhalas", "Bács-Kiskun" },
                    { 153, "Kiskunmajsa", "Bács-Kiskun" },
                    { 171, "Lébény", "Győr-Moson-Sopron" },
                    { 170, "Lajosmizse", "Bács-Kiskun" },
                    { 169, "Lábatlan", "Komárom-Esztergom" },
                    { 347, "Zirc", "Veszprém" },
                    { 167, "Kunszentmárton", "Jász-Nagykun-Szolnok" },
                    { 166, "Kunhegyes", "Jász-Nagykun-Szolnok" },
                    { 165, "Kőszeg", "Vas" },
                    { 164, "Körösladány", "Békés" },
                    { 152, "Kiskunlacháza", "Pest" },
                    { 163, "Körmend", "Vas" },
                    { 161, "Kondoros", "Békés" },
                    { 160, "Komló", "Baranya" },
                    { 159, "Komárom", "Komárom-Esztergom" },
                    { 158, "Komádi", "Hajdú-Bihar" },
                    { 157, "Kisvárda", "Szabolcs-Szatmár-Bereg" },
                    { 156, "Kisújszállás", "Jász-Nagykun-Szolnok" },
                    { 155, "Kistelek", "Csongrád-Csanád" },
                    { 154, "Kistarcsa", "Pest" },
                    { 162, "Kozármisleny", "Baranya" },
                    { 173, "Lenti", "Zala" },
                    { 130, "Jászberény", "Jász-Nagykun-Szolnok" },
                    { 128, "Jászapáti", "Jász-Nagykun-Szolnok" },
                    { 105, "Győr", "Győr-Moson-Sopron" },
                    { 104, "Gyönk", "Tolna" },
                    { 103, "Gyöngyöspata", "Heves" },
                    { 102, "Gyöngyös", "Heves" },
                    { 101, "Gyömrő", "Pest" },
                    { 100, "Gyomaendrőd", "Békés" },
                    { 99, "Gyál", "Pest" },
                    { 98, "Gönc", "Borsod-Abaúj-Zemplén" },
                    { 106, "Gyula", "Békés" },
                    { 97, "Gödöllő", "Pest" },
                    { 95, "Gárdony", "Fejér" },
                    { 94, "Füzesgyarmat", "Békés" },
                    { 93, "Füzesabony", "Heves" },
                    { 92, "Fót", "Pest" },
                    { 91, "Fonyód", "Somogy" },
                    { 90, "Fertőszentmiklós", "Győr-Moson-Sopron" },
                    { 89, "Fertőd", "Győr-Moson-Sopron" },
                    { 88, "Felsőzsolca", "Borsod-Abaúj-Zemplén" },
                    { 96, "Göd", "Pest" },
                    { 129, "Jászárokszállás", "Jász-Nagykun-Szolnok" },
                    { 107, "Hajdúböszörmény", "Hajdú-Bihar" },
                    { 109, "Hajdúhadház", "Hajdú-Bihar" },
                    { 127, "Jánossomorja", "Győr-Moson-Sopron" },
                    { 126, "Jánosháza", "Vas" },
                    { 125, "Jánoshalma", "Bács-Kiskun" },
                    { 124, "Izsák", "Bács-Kiskun" },
                    { 123, "Isaszeg", "Pest" },
                    { 122, "Igal", "Somogy" },
                    { 121, "Ibrány", "Szabolcs-Szatmár-Bereg" },
                    { 120, "Hódmezővásárhely", "Csongrád-Csanád" },
                    { 108, "Hajdúdorog", "Hajdú-Bihar" },
                    { 119, "Hévíz", "Zala" },
                    { 117, "Herend", "Veszprém" },
                    { 116, "Hatvan", "Heves" },
                    { 115, "Harkány", "Baranya" },
                    { 114, "Halásztelek", "Pest" },
                    { 113, "Hajós", "Bács-Kiskun" },
                    { 112, "Hajdúszoboszló", "Hajdú-Bihar" },
                    { 111, "Hajdúsámson", "Hajdú-Bihar" },
                    { 110, "Hajdúnánás", "Hajdú-Bihar" },
                    { 118, "Heves", "Heves" },
                    { 348, "Zsámbék", "Pest" }
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
                name: "IX_TagTraining_TagId",
                table: "TagTraining",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTraining_TrainingId",
                table: "TagTraining",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_CategoryName",
                table: "Training",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Training_TrainerId",
                table: "Training",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_LocationId",
                table: "TrainingSession",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_TrainingId",
                table: "TrainingSession",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_User_LocationId",
                table: "User",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "TagTraining");

            migrationBuilder.DropTable(
                name: "TrainingSession");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
