using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookstoreManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class AddGenderToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenderId",
                table: "Books",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Gender_GenderId",
                table: "Books",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Gender_GenderId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_GenderId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Books");
        }
    }
}
