using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailMagician.Data.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cocktails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Unit = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredients",
                columns: table => new
                {
                    CocktailId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false),
                    Quatity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredients", x => new { x.IngredientId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_CocktailIngredients_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 500, nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    BannId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    AddressId = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bars_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banns",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banns_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CocktailReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    Rating = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CocktailId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailReviews_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BarCocktails",
                columns: table => new
                {
                    BarId = table.Column<int>(nullable: false),
                    CocktailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarCocktails", x => new { x.BarId, x.CocktailId });
                    table.ForeignKey(
                        name: "FK_BarCocktails_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarCocktails_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BarReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 500, nullable: true),
                    Rating = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    BarId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarReviews_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, false, "Sofia" },
                    { 2, false, "Varna" },
                    { 3, false, "Plovdiv" }
                });

            migrationBuilder.InsertData(
                table: "Cocktails",
                columns: new[] { "Id", "ImageUrl", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 10, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-86159584-1508963306.jpg?crop=0.785xw:0.785xh;0,0.176xh&resize=980:*", false, "Gimlet" },
                    { 8, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-89804127-1508971287.jpg?crop=0.864516129032258xw:1xh;center,top&resize=980:*", false, "Manhattan" },
                    { 7, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-126551868-1-1508962528.jpg?crop=1.00xw:0.949xh;0,0.0259xh&resize=980:*", false, "Whiskey Sour" },
                    { 6, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/close-up-of-mojito-on-table-royalty-free-image-998866018-1557246957.jpg?crop=1xw:1xh;center,top&resize=980:*", false, "Mojito" },
                    { 9, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/homemade-refreshing-orange-mimosa-cocktails-royalty-free-image-538644352-1557251390.jpg?crop=0.447xw:1.00xh;0.111xw,0&resize=980:*", false, "Mimosa" },
                    { 4, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/cocktail-negroni-on-a-old-wooden-board-drink-with-royalty-free-image-922744216-1557251200.jpg?crop=0.447xw:1.00xh;0.434xw,0&resize=980:*", false, "Negroni" },
                    { 3, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/an-alcoholic-cosmopolitan-cocktail-is-on-the-bar-royalty-free-image-890771104-1557247368.jpg?crop=0.447xw:1.00xh;0.446xw,0&resize=980:*", false, "Cosmopolitan" },
                    { 2, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-516883622-1508961864.jpg?crop=0.44377777777777777xw:1xh;center,top&resize=980:*", false, "Margarita" },
                    { 1, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-164770405-1-1508961546.jpg?crop=1xw:1xh;center,top&resize=980:*", false, "Old Fashioned" },
                    { 5, "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-834848932-1508962243.jpg?crop=0.9998698425094363xw:1xh;center,top&resize=980:*", false, "Moscow Mule" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IsDeleted", "Name", "Unit" },
                values: new object[,]
                {
                    { 13, false, "Vodka", "oz" },
                    { 22, false, "Orange Juice", "oz" },
                    { 21, false, "Champagne", "oz" },
                    { 20, false, "Angostura bitters", "dashes" },
                    { 19, false, "Rye Whiskey", "oz" },
                    { 18, false, "Lemon Juice", "oz" },
                    { 17, false, "Simple Syrup", "oz" },
                    { 16, false, "White Rum", "oz" },
                    { 15, false, "Mint", "leaves" },
                    { 14, false, "Ginger Beer", "oz" },
                    { 12, false, "Sweet Vermouth", "oz" },
                    { 6, false, "Lime Juice", "oz" },
                    { 10, false, "Gin", "oz" },
                    { 9, false, "Cranberry Juice", "oz" },
                    { 8, false, "Citrus Vodka", "oz" },
                    { 7, false, "Salt", "pinch" },
                    { 5, false, "Cointreau", "oz" },
                    { 4, false, "Silver Tequila", "oz" },
                    { 3, false, "Sugar", "tsp" },
                    { 2, false, "Angostura bitter", "dashes" },
                    { 1, false, "Whiskey", "oz" },
                    { 11, false, "Campari", "oz" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "user" },
                    { 2, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "CityId", "IsDeleted", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, 1, false, 42.693880999999998, 23.330114999999999, "18 str. Aksakov" },
                    { 2, 1, false, 42.701141, 23.324477000000002, "28 str. Serdika" },
                    { 3, 1, false, 42.692791, 23.330869, "12 str. Tsar Shishman" },
                    { 4, 1, false, 42.698543999999998, 23.341951000000002, "17 blvd. Yanko Sakazov" },
                    { 5, 2, false, 43.226818000000002, 27.886537000000001, "201 blvd. Slivnitsa" },
                    { 6, 2, false, 43.203812999999997, 27.908376000000001, "1 str. Batcho Kiro" },
                    { 7, 3, false, 42.144928999999998, 24.755649999999999, "12 str. Knyaginya Maria Luiza" }
                });

            migrationBuilder.InsertData(
                table: "CocktailIngredients",
                columns: new[] { "IngredientId", "CocktailId", "Quatity" },
                values: new object[,]
                {
                    { 12, 4, 1.0 },
                    { 12, 8, 1.0 },
                    { 13, 5, 2.0 },
                    { 13, 10, 2.0 },
                    { 14, 5, 5.0 },
                    { 15, 6, 3.0 },
                    { 18, 7, 1.0 },
                    { 17, 6, 0.5 },
                    { 17, 10, 0.75 },
                    { 11, 4, 1.0 },
                    { 19, 8, 2.0 },
                    { 20, 8, 2.0 },
                    { 21, 9, 2.5 },
                    { 16, 6, 2.0 },
                    { 10, 4, 1.0 },
                    { 6, 10, 0.75 },
                    { 8, 3, 1.5 },
                    { 1, 1, 2.0 },
                    { 1, 7, 2.0 },
                    { 2, 1, 2.0 },
                    { 3, 1, 1.0 },
                    { 3, 7, 1.0 },
                    { 4, 2, 2.0 },
                    { 9, 3, 0.25 },
                    { 5, 2, 1.0 },
                    { 6, 2, 1.0 },
                    { 6, 3, 0.5 },
                    { 6, 5, 0.5 },
                    { 6, 6, 0.75 },
                    { 22, 9, 2.5 },
                    { 7, 2, 1.0 },
                    { 5, 3, 1.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BannId", "IsDeleted", "Password", "RoleId", "UserName" },
                values: new object[] { "f0476104-41a2-48df-8f15-eb8dc9abbc49", null, false, "1283eaf1b5d8f1430e47aeb106f598970762618445a450d575aaba48f85b9b39", 2, "origin" });

            migrationBuilder.InsertData(
                table: "Bars",
                columns: new[] { "Id", "AddressId", "ImageUrl", "IsDeleted", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 1, "http://mysofiaapartments.com/wp-content/uploads/2015/12/Motto.jpg", false, "Motto", "+35929872723" },
                    { 2, 2, "https://vijmag.bg/service/image?wEckYaFmQsLCeWsoS5ZNo40cnQ8JsnuTGOIWPRSIWSM_", false, "French 75", "+359887044557" },
                    { 3, 3, "http://mysofiaapartments.com/wp-content/uploads/2015/11/One-more-bar.jpg", false, "One More Bar", "+359877693735" },
                    { 4, 4, "http://funkt.eu/wp-content/uploads/2012/12/PAKETA-04.jpg", false, "Rakia Raketa Bar", "+35924446111" },
                    { 5, 5, "https://d32swnnyen7sbd.cloudfront.net/projects/0001/27/6a4e56f5a0e66ed7a10f3ca4e611de64942568d3.jpeg", false, "Moda Bar My Place", "+359876000056" },
                    { 6, 6, "http://martini.bg/wp-content/uploads/2017/01/martini_food_cocktails_varna_bulgaria_interior_7.jpg", false, "The Martini Bar", "+359893374437" },
                    { 7, 7, "https://lostinplovdiv.com/media/images/4691dddb3a.jpg", false, "Kriloto", "+359879924799" }
                });

            migrationBuilder.InsertData(
                table: "BarCocktails",
                columns: new[] { "BarId", "CocktailId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 7, 7 },
                    { 7, 6 },
                    { 7, 4 },
                    { 7, 2 },
                    { 6, 4 },
                    { 6, 1 },
                    { 6, 7 },
                    { 6, 5 },
                    { 5, 9 },
                    { 5, 10 },
                    { 5, 2 },
                    { 5, 3 },
                    { 4, 8 },
                    { 4, 7 },
                    { 4, 6 },
                    { 4, 5 },
                    { 4, 1 },
                    { 3, 9 },
                    { 3, 8 },
                    { 3, 6 },
                    { 3, 3 },
                    { 3, 2 },
                    { 2, 5 },
                    { 2, 4 },
                    { 2, 10 },
                    { 2, 1 },
                    { 1, 9 },
                    { 1, 7 },
                    { 1, 3 },
                    { 7, 9 },
                    { 7, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BarCocktails_CocktailId",
                table: "BarCocktails",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_BarReviews_BarId",
                table: "BarReviews",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_BarReviews_UserId",
                table: "BarReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bars_AddressId",
                table: "Bars",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredients_CocktailId",
                table: "CocktailIngredients",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailReviews_CocktailId",
                table: "CocktailReviews",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailReviews_UserId",
                table: "CocktailReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banns");

            migrationBuilder.DropTable(
                name: "BarCocktails");

            migrationBuilder.DropTable(
                name: "BarReviews");

            migrationBuilder.DropTable(
                name: "CocktailIngredients");

            migrationBuilder.DropTable(
                name: "CocktailReviews");

            migrationBuilder.DropTable(
                name: "Bars");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Cocktails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
