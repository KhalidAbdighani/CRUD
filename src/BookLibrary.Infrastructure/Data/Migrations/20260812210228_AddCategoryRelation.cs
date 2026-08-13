using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLibrary.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BookStore",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categroies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categroies", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookStore_CategoryId",
                table: "BookStore",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStore_Categroies_CategoryId",
                table: "BookStore",
                column: "CategoryId",
                principalTable: "Categroies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStore_Categroies_CategoryId",
                table: "BookStore");

            migrationBuilder.DropTable(
                name: "Categroies");

            migrationBuilder.DropIndex(
                name: "IX_BookStore_CategoryId",
                table: "BookStore");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BookStore");
        }
    }
}
