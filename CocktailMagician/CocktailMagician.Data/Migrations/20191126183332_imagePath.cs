using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailMagician.Data.Migrations
{
    public partial class imagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Cocktails",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Bars",
                newName: "ImagePath");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/barImages/Motto.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "/barImages/French75.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "/barImages/OneMoreBar.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/barImages/Raketa.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "/barImages/ModaBarMyPlace.jpeg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "/barImages/MartiniBar.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/barImages/Kriloto.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/cocktiailImage/OldFashioned.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "/cocktiailImage/Margarita.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "/cocktiailImage/Cosmopolitan.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/cocktiailImage/Negroni.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "/cocktiailImage/MoscowMule.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "/cocktiailImage/Mojito.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/cocktiailImage/WhiskeySour.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "/cocktiailImage/Manhattan.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagePath",
                value: "/cocktiailImage/Mimosa.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagePath",
                value: "/cocktiailImage/Gimlet.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Cocktails",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Bars",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "http://mysofiaapartments.com/wp-content/uploads/2015/12/Motto.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://vijmag.bg/service/image?wEckYaFmQsLCeWsoS5ZNo40cnQ8JsnuTGOIWPRSIWSM_");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "http://mysofiaapartments.com/wp-content/uploads/2015/11/One-more-bar.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "http://funkt.eu/wp-content/uploads/2012/12/PAKETA-04.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://d32swnnyen7sbd.cloudfront.net/projects/0001/27/6a4e56f5a0e66ed7a10f3ca4e611de64942568d3.jpeg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "http://martini.bg/wp-content/uploads/2017/01/martini_food_cocktails_varna_bulgaria_interior_7.jpg");

            migrationBuilder.UpdateData(
                table: "Bars",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://lostinplovdiv.com/media/images/4691dddb3a.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-164770405-1-1508961546.jpg?crop=1xw:1xh;center,top&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-516883622-1508961864.jpg?crop=0.44377777777777777xw:1xh;center,top&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/an-alcoholic-cosmopolitan-cocktail-is-on-the-bar-royalty-free-image-890771104-1557247368.jpg?crop=0.447xw:1.00xh;0.446xw,0&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/cocktail-negroni-on-a-old-wooden-board-drink-with-royalty-free-image-922744216-1557251200.jpg?crop=0.447xw:1.00xh;0.434xw,0&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-834848932-1508962243.jpg?crop=0.9998698425094363xw:1xh;center,top&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/close-up-of-mojito-on-table-royalty-free-image-998866018-1557246957.jpg?crop=1xw:1xh;center,top&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-126551868-1-1508962528.jpg?crop=1.00xw:0.949xh;0,0.0259xh&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-89804127-1508971287.jpg?crop=0.864516129032258xw:1xh;center,top&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/homemade-refreshing-orange-mimosa-cocktails-royalty-free-image-538644352-1557251390.jpg?crop=0.447xw:1.00xh;0.111xw,0&resize=980:*");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-86159584-1508963306.jpg?crop=0.785xw:0.785xh;0,0.176xh&resize=980:*");
        }
    }
}
