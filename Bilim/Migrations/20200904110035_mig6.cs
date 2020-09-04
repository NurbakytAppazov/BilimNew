using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilim.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories3",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories3",
                newName: "CategoryName");
        }
    }
}
