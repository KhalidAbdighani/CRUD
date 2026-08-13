using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStore_Categroies_CategoryId",
                table: "BookStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categroies",
                table: "Categroies");

            migrationBuilder.RenameTable(
                name: "Categroies",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStore_Categories_CategoryId",
                table: "BookStore",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStore_Categories_CategoryId",
                table: "BookStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categroies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categroies",
                table: "Categroies",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStore_Categroies_CategoryId",
                table: "BookStore",
                column: "CategoryId",
                principalTable: "Categroies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
