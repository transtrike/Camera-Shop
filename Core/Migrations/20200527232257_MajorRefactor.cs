using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Camera_Shop.Migrations
{
    public partial class MajorRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cameras_CameraSpecifications_SpecificationsId",
                table: "Cameras");

            migrationBuilder.DropTable(
                name: "CameraSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_Cameras_SpecificationsId",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "SpecificationsId",
                table: "Cameras");

            migrationBuilder.AddColumn<int>(
                name: "BaseISO",
                table: "Cameras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxISO",
                table: "Cameras",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Megapixels",
                table: "Cameras",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseISO",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "MaxISO",
                table: "Cameras");

            migrationBuilder.DropColumn(
                name: "Megapixels",
                table: "Cameras");

            migrationBuilder.AddColumn<int>(
                name: "SpecificationsId",
                table: "Cameras",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CameraSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BaseISO = table.Column<int>(type: "integer", nullable: false),
                    MaxISO = table.Column<int>(type: "integer", nullable: false),
                    Megapixels = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraSpecifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_SpecificationsId",
                table: "Cameras",
                column: "SpecificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cameras_CameraSpecifications_SpecificationsId",
                table: "Cameras",
                column: "SpecificationsId",
                principalTable: "CameraSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
