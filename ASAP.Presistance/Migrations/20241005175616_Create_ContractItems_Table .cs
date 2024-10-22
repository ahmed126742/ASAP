using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class Create_ContractItems_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "contractItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Requirement = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductionWeek = table.Column<int>(type: "int", nullable: true),
                    InstallationDateFrom = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    InstallationDateTo = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    FitterId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SurveyorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CertesNo = table.Column<int>(type: "int", nullable: true),
                    InvoiceNo = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Frame = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RequestDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    W = table.Column<int>(type: "int", nullable: true),
                    RD = table.Column<int>(type: "int", nullable: true),
                    FD = table.Column<int>(type: "int", nullable: true),
                    W_RD_FD_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    W_RD_FD_Status = table.Column<int>(type: "int", nullable: true),
                    PD = table.Column<int>(type: "int", nullable: true),
                    PD_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    PD_Status = table.Column<int>(type: "int", nullable: true),
                    VS = table.Column<int>(type: "int", nullable: true),
                    VS_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    VS_Status = table.Column<int>(type: "int", nullable: true),
                    FED = table.Column<int>(type: "int", nullable: true),
                    FED_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    FED_Status = table.Column<int>(type: "int", nullable: true),
                    Bifolds = table.Column<int>(type: "int", nullable: true),
                    Bifolds_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Bifolds_Status = table.Column<int>(type: "int", nullable: true),
                    Roofs = table.Column<int>(type: "int", nullable: true),
                    Roofs_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Roofs_Status = table.Column<int>(type: "int", nullable: true),
                    Ancils = table.Column<int>(type: "int", nullable: true),
                    Ancils_SupplierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Ancils_Status = table.Column<int>(type: "int", nullable: true),
                    GlassStatus = table.Column<int>(type: "int", nullable: true),
                    GlassDeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PanelStatus = table.Column<int>(type: "int", nullable: true),
                    PanelDeliveryDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ContractId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
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
                    table.PrimaryKey("PK_contractItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contractItems_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_contractItems_ContractId",
                table: "contractItems",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contractItems");
        }
    }
}
