using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailMagician.Data.Migrations
{
    public partial class seedFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/cocktiailImages/OldFashioned.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "/cocktiailImages/Margarita.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "/cocktiailImages/Cosmopolitan.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/cocktiailImages/Negroni.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImagePath",
                value: "/cocktiailImages/MoscowMule.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "/cocktiailImages/Mojito.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/cocktiailImages/WhiskeySour.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "/cocktiailImages/Manhattan.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagePath",
                value: "/cocktiailImages/Mimosa.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagePath",
                value: "/cocktiailImages/Gimlet.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
