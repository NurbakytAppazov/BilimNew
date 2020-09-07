using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilim.Migrations
{
    public partial class mig555 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetKursWithCategories");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Kurs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId2",
                table: "Kurs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId3",
                table: "Kurs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories2",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories3",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories3", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories2");

            migrationBuilder.DropTable(
                name: "Categories3");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "CategoryId2",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "CategoryId3",
                table: "Kurs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.CreateTable(
                name: "GetKursWithCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(nullable: false),
                    KursId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetKursWithCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GetKursWithCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetKursWithCategories_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GetKursWithCategories_CategoryId",
                table: "GetKursWithCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GetKursWithCategories_KursId",
                table: "GetKursWithCategories",
                column: "KursId");
        }
    }
}
