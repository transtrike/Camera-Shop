using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Camera_Shop.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CameraSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MegapixelCountX = table.Column<int>(nullable: false),
                    MegapixelCountY = table.Column<int>(nullable: false),
                    Megapixels = table.Column<int>(nullable: false),
                    BaseISO = table.Column<int>(nullable: false),
                    MaxISO = table.Column<int>(nullable: false),
                    ExtendedIso = table.Column<int>(nullable: false),
                    FastestShutterSpeed = table.Column<int>(nullable: false),
                    ContinuesFPS = table.Column<decimal>(nullable: false),
                    SingleFPS = table.Column<decimal>(nullable: false),
                    VideoQuality = table.Column<string>(nullable: false),
                    VideoMaxFps = table.Column<int>(nullable: false),
                    BatteryType = table.Column<string>(nullable: false),
                    RatedBatteryLife = table.Column<TimeSpan>(nullable: false),
                    SensorSize = table.Column<string>(nullable: true),
                    SensorTechnology = table.Column<string>(nullable: false),
                    Mount = table.Column<string>(nullable: false),
                    SizeX = table.Column<decimal>(nullable: false),
                    SizeY = table.Column<decimal>(nullable: false),
                    SizeZ = table.Column<decimal>(nullable: false),
                    Weight = table.Column<decimal>(nullable: false),
                    Wifi = table.Column<bool>(nullable: false),
                    WifiBand = table.Column<decimal>(nullable: false),
                    Bluetooth = table.Column<bool>(nullable: false),
                    ShutterLag = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CameraSpecifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cameras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    SpecificationsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cameras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cameras_CameraSpecifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "CameraSpecifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cameras_SpecificationsId",
                table: "Cameras",
                column: "SpecificationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cameras");

            migrationBuilder.DropTable(
                name: "CameraSpecifications");
        }
    }
}
