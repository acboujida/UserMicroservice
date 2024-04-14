using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class FixForBorrowedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_OwnedBooks_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbb22e6b-aaf8-496f-b849-4a1606f3f20f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f97dfd52-1c40-4fa9-881c-04e4f71f3be3");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnedBookId",
                table: "BorrowedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "BorrowedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "879e0b92-3dd2-41a1-9c99-280efa272a23", null, "Admin", "ADMIN" },
                    { "8efd620c-d1f6-419f-993d-5db046cac82b", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_OwnedBookId",
                table: "BorrowedBooks",
                column: "OwnedBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_OwnedBooks_OwnedBookId",
                table: "BorrowedBooks",
                column: "OwnedBookId",
                principalTable: "OwnedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowedBooks_OwnedBooks_OwnedBookId",
                table: "BorrowedBooks");

            migrationBuilder.DropIndex(
                name: "IX_BorrowedBooks_OwnedBookId",
                table: "BorrowedBooks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "879e0b92-3dd2-41a1-9c99-280efa272a23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8efd620c-d1f6-419f-993d-5db046cac82b");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "OwnedBookId",
                table: "BorrowedBooks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "BorrowedBooks");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BorrowedBooks",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dbb22e6b-aaf8-496f-b849-4a1606f3f20f", null, "Admin", "ADMIN" },
                    { "f97dfd52-1c40-4fa9-881c-04e4f71f3be3", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowedBooks_OwnedBooks_BookId",
                table: "BorrowedBooks",
                column: "BookId",
                principalTable: "OwnedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
