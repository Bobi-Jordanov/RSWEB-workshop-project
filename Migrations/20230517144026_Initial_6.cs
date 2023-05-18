using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBookStore.Migrations
{
    public partial class Initial_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "BookGenre");

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "BookGenre",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre");

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "BookGenre",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "BookGenre",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BooksId",
                table: "BookGenre",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
