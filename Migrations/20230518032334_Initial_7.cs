using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCBookStore.Migrations
{
    public partial class Initial_7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Books_BooksId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BooksId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Review");

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "UserBooks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Books_BooksId",
                table: "Review",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BooksId",
                table: "UserBooks",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Books_BooksId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_Books_BooksId",
                table: "UserBooks");

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "UserBooks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "UserBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "BooksId",
                table: "Review",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Books_BooksId",
                table: "Review",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_Books_BooksId",
                table: "UserBooks",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
