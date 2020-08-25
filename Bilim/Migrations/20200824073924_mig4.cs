using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilim.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kurs_Avtors_AvtorId",
                table: "Kurs");

            migrationBuilder.DropTable(
                name: "Avtors");

            migrationBuilder.DropIndex(
                name: "IX_Kurs_AvtorId",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "AvtorId",
                table: "Kurs");

            migrationBuilder.AddColumn<string>(
                name: "AvtorImgUrl",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvtorInfo",
                table: "Kurs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvtorName",
                table: "Kurs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvtorImgUrl",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "AvtorInfo",
                table: "Kurs");

            migrationBuilder.DropColumn(
                name: "AvtorName",
                table: "Kurs");

            migrationBuilder.AddColumn<int>(
                name: "AvtorId",
                table: "Kurs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Avtors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Info = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avtors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_AvtorId",
                table: "Kurs",
                column: "AvtorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kurs_Avtors_AvtorId",
                table: "Kurs",
                column: "AvtorId",
                principalTable: "Avtors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
