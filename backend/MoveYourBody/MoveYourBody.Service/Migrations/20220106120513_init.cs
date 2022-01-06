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
                name: "TrainingSession",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Training_id = table.Column<int>(nullable: false),
                    Location_id = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Minutes = table.Column<int>(nullable: false),
                    Address_name = table.Column<string>(maxLength: 255, nullable: false),
                    Place_name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSession", x => x.Id);
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
                    { 12, "yoga.jpg", "Jóga" },
                    { 10, "swimming.jpg", "Úszás" },
                    { 9, "trx.jpg", "TRX" },
                    { 8, "tennis.jpg", "Tenisz" },
                    { 7, "spartan.jpg", "Spartan" },
                    { 11, "riding.jpg", "Lovaglás" },
                    { 5, "handball.jpg", "Kézilabda" },
                    { 4, "basketball.jpg", "Kosárlabda" },
                    { 3, "football.jpg", "Labdarúgás" },
                    { 2, "crossFitt.jpg", "Crossfit" },
                    { 6, "volleyball.jpg", "Röplabda" }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "City_name", "County_name" },
                values: new object[,]
                {
                    { 231, "Paks", "Tolna" },
                    { 238, "Pécsvárad", "Baranya" },
                    { 237, "Pécs", "Baranya" },
                    { 236, "Pécel", "Pest" },
                    { 235, "Pásztó", "Nógrád" },
                    { 234, "Pápa", "Veszprém" },
                    { 233, "Pannonhalma", "Győr-Moson-Sopron" },
                    { 232, "Pálháza", "Borsod-Abaúj-Zemplén" },
                    { 230, "Pacsa", "Zala" },
                    { 224, "Orosháza", "Békés" },
                    { 228, "Őriszentpéter", "Vas" },
                    { 227, "Őrbottyán", "Pest" },
                    { 226, "Ózd", "Borsod-Abaúj-Zemplén" },
                    { 225, "Oroszlány", "Komárom-Esztergom" },
                    { 239, "Pétervására", "Heves" },
                    { 223, "Onga", "Borsod-Abaúj-Zemplén" },
                    { 222, "Ócsa", "Pest" },
                    { 221, "Nyírtelek", "Szabolcs-Szatmár-Bereg" },
                    { 229, "Örkény", "Pest" },
                    { 240, "Pilis", "Pest" },
                    { 246, "Pusztaszabolcs", "Fejér" },
                    { 242, "Pilisvörösvár", "Pest" },
                    { 260, "Sárbogárd", "Fejér" },
                    { 259, "Sándorfalva", "Csongrád-Csanád" },
                    { 258, "Salgótarján", "Nógrád" },
                    { 257, "Sajószentpéter", "Borsod-Abaúj-Zemplén" },
                    { 256, "Sajóbábony", "Borsod-Abaúj-Zemplén" },
                    { 255, "Rudabánya", "Borsod-Abaúj-Zemplén" },
                    { 254, "Rétság", "Nógrád" },
                    { 253, "Répcelak", "Vas" },
                    { 241, "Piliscsaba", "Pest" },
                    { 252, "Rákóczifalva", "Jász-Nagykun-Szolnok" },
                    { 250, "Ráckeve", "Pest" },
                    { 249, "Rácalmás", "Fejér" },
                    { 248, "Püspökladány", "Hajdú-Bihar" },
                    { 247, "Putnok", "Borsod-Abaúj-Zemplén" },
                    { 220, "Nyírmada", "Szabolcs-Szatmár-Bereg" },
                    { 245, "Pomáz", "Pest" },
                    { 244, "Polgárdi", "Fejér" },
                    { 243, "Polgár", "Hajdú-Bihar" },
                    { 251, "Rakamaz", "Szabolcs-Szatmár-Bereg" },
                    { 219, "Nyírlugos", "Szabolcs-Szatmár-Bereg" },
                    { 213, "Nyékládháza", "Borsod-Abaúj-Zemplén" },
                    { 217, "Nyírbogát", "Szabolcs-Szatmár-Bereg" },
                    { 194, "Mezőtúr", "Jász-Nagykun-Szolnok" },
                    { 193, "Mezőkövesd", "Borsod-Abaúj-Zemplén" },
                    { 192, "Mezőkovácsháza", "Békés" },
                    { 191, "Mezőkeresztes", "Borsod-Abaúj-Zemplén" },
                    { 190, "Mezőhegyes", "Békés" },
                    { 189, "Mezőcsát", "Borsod-Abaúj-Zemplén" },
                    { 188, "Mezőberény", "Békés" },
                    { 187, "Mélykút", "Bács-Kiskun" },
                    { 195, "Mindszent", "Csongrád-Csanád" },
                    { 186, "Medgyesegyháza", "Békés" },
                    { 184, "Martonvásár", "Fejér" },
                    { 183, "Martfű", "Jász-Nagykun-Szolnok" },
                    { 182, "Máriapócs", "Szabolcs-Szatmár-Bereg" },
                    { 181, "Marcali", "Somogy" },
                    { 180, "Mándok", "Szabolcs-Szatmár-Bereg" },
                    { 179, "Makó", "Csongrád-Csanád" },
                    { 178, "Mágocs", "Baranya" },
                    { 177, "Maglód", "Pest" },
                    { 185, "Mátészalka", "Szabolcs-Szatmár-Bereg" },
                    { 218, "Nyíregyháza", "Szabolcs-Szatmár-Bereg" },
                    { 196, "Miskolc", "Borsod-Abaúj-Zemplén" },
                    { 198, "Monor", "Pest" },
                    { 216, "Nyírbátor", "Szabolcs-Szatmár-Bereg" },
                    { 215, "Nyíradony", "Hajdú-Bihar" },
                    { 214, "Nyergesújfalu", "Komárom-Esztergom" },
                    { 261, "Sarkad", "Békés" },
                    { 212, "Nagymaros", "Pest" },
                    { 211, "Nagymányok", "Tolna" },
                    { 210, "Nagykőrös", "Pest" },
                    { 209, "Nagykáta", "Pest" },
                    { 197, "Mohács", "Baranya" },
                    { 208, "Nagykanizsa", "Zala" },
                    { 206, "Nagyhalász", "Szabolcs-Szatmár-Bereg" },
                    { 205, "Nagyecsed", "Szabolcs-Szatmár-Bereg" },
                    { 204, "Nagybajom", "Somogy" },
                    { 203, "Nagyatád", "Somogy" },
                    { 202, "Nádudvar", "Hajdú-Bihar" },
                    { 201, "Mosonmagyaróvár", "Győr-Moson-Sopron" },
                    { 200, "Mórahalom", "Csongrád-Csanád" },
                    { 199, "Mór", "Fejér" },
                    { 207, "Nagykálló", "Szabolcs-Szatmár-Bereg" },
                    { 262, "Sárospatak", "Borsod-Abaúj-Zemplén" },
                    { 268, "Simontornya", "Tolna" },
                    { 264, "Sásd", "Baranya" },
                    { 326, "Vác", "Pest" },
                    { 325, "Üllő", "Pest" },
                    { 324, "Újszász", "Jász-Nagykun-Szolnok" },
                    { 323, "Újkígyós", "Békés" },
                    { 322, "Újhartyán", "Pest" },
                    { 321, "Újfehértó", "Szabolcs-Szatmár-Bereg" },
                    { 320, "Túrkeve", "Jász-Nagykun-Szolnok" },
                    { 319, "Tura", "Pest" },
                    { 327, "Vaja", "Szabolcs-Szatmár-Bereg" },
                    { 318, "Törökszentmiklós", "Jász-Nagykun-Szolnok" },
                    { 316, "Tököl", "Pest" },
                    { 315, "Tótkomlós", "Békés" },
                    { 314, "Tompa", "Bács-Kiskun" },
                    { 313, "Tolna", "Tolna" },
                    { 312, "Tokaj", "Borsod-Abaúj-Zemplén" },
                    { 311, "Tiszavasvári", "Szabolcs-Szatmár-Bereg" },
                    { 310, "Tiszaújváros", "Borsod-Abaúj-Zemplén" },
                    { 309, "Tiszalök", "Szabolcs-Szatmár-Bereg" },
                    { 317, "Törökbálint", "Pest" },
                    { 308, "Tiszakécske", "Bács-Kiskun" },
                    { 328, "Vámospércs", "Hajdú-Bihar" },
                    { 330, "Vásárosnamény", "Szabolcs-Szatmár-Bereg" },
                    { 348, "Zsámbék", "Pest" },
                    { 347, "Zirc", "Veszprém" },
                    { 346, "Zamárdi", "Somogy" },
                    { 345, "Zalaszentgrót", "Zala" },
                    { 344, "Zalalövő", "Zala" },
                    { 343, "Zalakaros", "Zala" },
                    { 342, "Zalaegerszeg", "Zala" },
                    { 341, "Záhony", "Szabolcs-Szatmár-Bereg" },
                    { 329, "Várpalota", "Veszprém" },
                    { 340, "Visegrád", "Pest" },
                    { 338, "Vésztő", "Békés" },
                    { 337, "Veszprém", "Veszprém" },
                    { 336, "Verpelét", "Heves" },
                    { 335, "Veresegyház", "Pest" },
                    { 334, "Vép", "Vas" },
                    { 333, "Velence", "Fejér" },
                    { 332, "Vecsés", "Pest" },
                    { 331, "Vasvár", "Vas" },
                    { 339, "Villány", "Baranya" },
                    { 263, "Sárvár", "Vas" },
                    { 307, "Tiszafüred", "Jász-Nagykun-Szolnok" },
                    { 305, "Tiszacsege", "Hajdú-Bihar" },
                    { 282, "Szekszárd", "Tolna" },
                    { 281, "Székesfehérvár", "Fejér" },
                    { 280, "Szeghalom", "Békés" },
                    { 279, "Szeged", "Csongrád-Csanád" },
                    { 278, "Szécsény", "Nógrád" },
                    { 277, "Százhalombatta", "Pest" },
                    { 276, "Szarvas", "Békés" },
                    { 275, "Szabadszállás", "Bács-Kiskun" },
                    { 283, "Szendrő", "Borsod-Abaúj-Zemplén" },
                    { 274, "Sümeg", "Veszprém" },
                    { 272, "Sopron", "Győr-Moson-Sopron" },
                    { 271, "Soltvadkert", "Bács-Kiskun" },
                    { 270, "Solt", "Bács-Kiskun" },
                    { 269, "Siófok", "Somogy" },
                    { 176, "Lőrinci", "Heves" },
                    { 267, "Siklós", "Baranya" },
                    { 266, "Sellye", "Baranya" },
                    { 265, "Sátoraljaújhely", "Borsod-Abaúj-Zemplén" },
                    { 273, "Sülysáp", "Pest" },
                    { 306, "Tiszaföldvár", "Jász-Nagykun-Szolnok" },
                    { 284, "Szentendre", "Pest" },
                    { 286, "Szentgotthárd", "Vas" },
                    { 304, "Tét", "Győr-Moson-Sopron" },
                    { 303, "Téglás", "Hajdú-Bihar" },
                    { 302, "Tatabánya", "Komárom-Esztergom" },
                    { 301, "Tata", "Komárom-Esztergom" },
                    { 300, "Tát", "Komárom-Esztergom" },
                    { 299, "Tapolca", "Veszprém" },
                    { 298, "Tápiószele", "Pest" },
                    { 297, "Tamási", "Tolna" },
                    { 285, "Szentes", "Csongrád-Csanád" },
                    { 296, "Tab", "Somogy" },
                    { 294, "Szolnok", "Jász-Nagykun-Szolnok" },
                    { 293, "Szob", "Pest" },
                    { 292, "Szikszó", "Borsod-Abaúj-Zemplén" },
                    { 291, "Szigetvár", "Baranya" },
                    { 290, "Szigetszentmiklós", "Pest" },
                    { 289, "Szigethalom", "Pest" },
                    { 288, "Szerencs", "Borsod-Abaúj-Zemplén" },
                    { 287, "Szentlőrinc", "Baranya" },
                    { 295, "Szombathely", "Vas" },
                    { 175, "Letenye", "Zala" },
                    { 174, "Létavértes", "Hajdú-Bihar" },
                    { 173, "Lenti", "Zala" },
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
                    { 84, "Érd", "Pest" },
                    { 83, "Ercsi", "Fejér" },
                    { 82, "Enying", "Fejér" },
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
                    { 85, "Esztergom", "Komárom-Esztergom" },
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
                    { 86, "Fegyvernek", "Jász-Nagykun-Szolnok" },
                    { 81, "Encs", "Borsod-Abaúj-Zemplén" },
                    { 88, "Felsőzsolca", "Borsod-Abaúj-Zemplén" },
                    { 150, "Kiskunfélegyháza", "Bács-Kiskun" },
                    { 149, "Kiskőrös", "Bács-Kiskun" },
                    { 148, "Kisköre", "Heves" },
                    { 147, "Kisbér", "Komárom-Esztergom" },
                    { 146, "Keszthely", "Zala" },
                    { 145, "Kerepes", "Pest" },
                    { 144, "Kerekegyháza", "Bács-Kiskun" },
                    { 143, "Kenderes", "Jász-Nagykun-Szolnok" },
                    { 151, "Kiskunhalas", "Bács-Kiskun" },
                    { 142, "Kemecse", "Szabolcs-Szatmár-Bereg" },
                    { 140, "Kecel", "Bács-Kiskun" },
                    { 139, "Kazincbarcika", "Borsod-Abaúj-Zemplén" },
                    { 138, "Karcag", "Jász-Nagykun-Szolnok" },
                    { 137, "Kapuvár", "Győr-Moson-Sopron" },
                    { 136, "Kaposvár", "Somogy" },
                    { 87, "Fehérgyarmat", "Szabolcs-Szatmár-Bereg" },
                    { 134, "Kadarkút", "Somogy" },
                    { 133, "Kaba", "Hajdú-Bihar" },
                    { 141, "Kecskemét", "Bács-Kiskun" },
                    { 132, "Jászkisér", "Jász-Nagykun-Szolnok" },
                    { 152, "Kiskunlacháza", "Pest" },
                    { 154, "Kistarcsa", "Pest" },
                    { 172, "Lengyeltóti", "Somogy" },
                    { 171, "Lébény", "Győr-Moson-Sopron" },
                    { 170, "Lajosmizse", "Bács-Kiskun" },
                    { 169, "Lábatlan", "Komárom-Esztergom" },
                    { 168, "Kunszentmiklós", "Bács-Kiskun" },
                    { 167, "Kunszentmárton", "Jász-Nagykun-Szolnok" },
                    { 166, "Kunhegyes", "Jász-Nagykun-Szolnok" },
                    { 165, "Kőszeg", "Vas" },
                    { 153, "Kiskunmajsa", "Bács-Kiskun" },
                    { 164, "Körösladány", "Békés" },
                    { 162, "Kozármisleny", "Baranya" },
                    { 161, "Kondoros", "Békés" },
                    { 160, "Komló", "Baranya" },
                    { 159, "Komárom", "Komárom-Esztergom" },
                    { 158, "Komádi", "Hajdú-Bihar" },
                    { 157, "Kisvárda", "Szabolcs-Szatmár-Bereg" },
                    { 156, "Kisújszállás", "Jász-Nagykun-Szolnok" },
                    { 155, "Kistelek", "Csongrád-Csanád" },
                    { 163, "Körmend", "Vas" },
                    { 131, "Jászfényszaru", "Jász-Nagykun-Szolnok" },
                    { 135, "Kalocsa", "Bács-Kiskun" },
                    { 129, "Jászárokszállás", "Jász-Nagykun-Szolnok" },
                    { 106, "Gyula", "Békés" },
                    { 105, "Győr", "Győr-Moson-Sopron" },
                    { 104, "Gyönk", "Tolna" },
                    { 130, "Jászberény", "Jász-Nagykun-Szolnok" },
                    { 102, "Gyöngyös", "Heves" },
                    { 101, "Gyömrő", "Pest" },
                    { 100, "Gyomaendrőd", "Békés" },
                    { 99, "Gyál", "Pest" },
                    { 107, "Hajdúböszörmény", "Hajdú-Bihar" },
                    { 98, "Gönc", "Borsod-Abaúj-Zemplén" },
                    { 96, "Göd", "Pest" },
                    { 95, "Gárdony", "Fejér" },
                    { 94, "Füzesgyarmat", "Békés" },
                    { 93, "Füzesabony", "Heves" },
                    { 92, "Fót", "Pest" },
                    { 91, "Fonyód", "Somogy" },
                    { 90, "Fertőszentmiklós", "Győr-Moson-Sopron" },
                    { 89, "Fertőd", "Győr-Moson-Sopron" },
                    { 97, "Gödöllő", "Pest" },
                    { 108, "Hajdúdorog", "Hajdú-Bihar" },
                    { 103, "Gyöngyöspata", "Heves" },
                    { 110, "Hajdúnánás", "Hajdú-Bihar" },
                    { 128, "Jászapáti", "Jász-Nagykun-Szolnok" },
                    { 127, "Jánossomorja", "Győr-Moson-Sopron" },
                    { 126, "Jánosháza", "Vas" },
                    { 125, "Jánoshalma", "Bács-Kiskun" },
                    { 109, "Hajdúhadház", "Hajdú-Bihar" },
                    { 124, "Izsák", "Bács-Kiskun" },
                    { 123, "Isaszeg", "Pest" },
                    { 121, "Ibrány", "Szabolcs-Szatmár-Bereg" },
                    { 120, "Hódmezővásárhely", "Csongrád-Csanád" },
                    { 122, "Igal", "Somogy" },
                    { 118, "Heves", "Heves" },
                    { 117, "Herend", "Veszprém" },
                    { 116, "Hatvan", "Heves" },
                    { 115, "Harkány", "Baranya" },
                    { 114, "Halásztelek", "Pest" },
                    { 113, "Hajós", "Bács-Kiskun" },
                    { 112, "Hajdúszoboszló", "Hajdú-Bihar" },
                    { 111, "Hajdúsámson", "Hajdú-Bihar" },
                    { 119, "Hévíz", "Zala" }
                });

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "Id", "Colour", "Name" },
                values: new object[,]
                {
                    { 7, "#D7263D", "Erőnléti" },
                    { 10, "#9984D4", "Köredzés" },
                    { 9, "#373F51", "Rehabilitációs" },
                    { 8, "blue", "Aerobic" },
                    { 6, "green", "Személyi edzés" },
                    { 11, "#F17300", "Bemelegítés" },
                    { 4, "black", "Szabadtéri" },
                    { 3, "red", "Edzőterem" },
                    { 2, "#05A8AA", "Saját testsúlyos" },
                    { 1, "#6610f2", "Csoportos" },
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicant");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "TagTraining");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "TrainingSession");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
