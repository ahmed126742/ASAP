using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class Create_Fiiting_jobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fittings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    IstallerName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AttachementHeaderId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    AllFramesSquarLevelPlumb = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SaftyGlassInstallCorrectly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    InternalAndExternalMakingGoodComplete = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WindowDoorFramesAndGlassCleaned = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PropertyCleanedOfDebrisAndDust = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllFixingsCorrectlyCarriedOut = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PhotosTaken = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ContractItemId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fittings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fittings_contractItems_ContractItemId",
                        column: x => x.ContractItemId,
                        principalTable: "contractItems",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServiceCalls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RequiredDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReportedIssue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    WorkDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PartsRequired = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCustomerHappy = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    WasEngineerOnTime = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    WorkQualityRate = table.Column<int>(type: "int", nullable: true),
                    AttachementHeaderId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCalls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCalls_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Fittings_ContractItemId",
                table: "Fittings",
                column: "ContractItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCalls_UserId",
                table: "ServiceCalls",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fittings");

            migrationBuilder.DropTable(
                name: "ServiceCalls");
        }
    }
}
