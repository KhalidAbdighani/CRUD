using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib_app.Migrations
{
    /// <inheritdoc />
    public partial class FirstStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookStore",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Book_name_db = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auth_name_db = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStore", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookStore");
        }
    }
}
