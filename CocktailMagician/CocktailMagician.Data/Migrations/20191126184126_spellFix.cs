using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailMagician.Data.Migrations
{
    public partial class spellFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/cocktailImages/OldFashioned.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagePath",
                value: "/cocktailImages/Margarita.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImagePath",
                value: "/cocktailImages/Cosmopolitan.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImagePath",
                value: "/cocktailImages/Negroni.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImagePath",
                value: "/cocktailImages/Mojito.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImagePath",
                value: "/cocktailImages/WhiskeySour.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImagePath",
                value: "/cocktailImages/Manhattan.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImagePath",
                value: "/cocktailImages/Mimosa.jpg");

            migrationBuilder.UpdateData(
                table: "Cocktails",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImagePath",
                value: "/cocktailImages/Gimlet.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
