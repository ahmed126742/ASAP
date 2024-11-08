using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class Add_ContractItemId_To_ServiceCall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContractItemId",
                table: "ServiceCalls",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCalls_ContractItemId",
                table: "ServiceCalls",
                column: "ContractItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCalls_contractItems_ContractItemId",
                table: "ServiceCalls",
                column: "ContractItemId",
                principalTable: "contractItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCalls_contractItems_ContractItemId",
                table: "ServiceCalls");

            migrationBuilder.DropIndex(
                name: "IX_ServiceCalls_ContractItemId",
                table: "ServiceCalls");

            migrationBuilder.DropColumn(
                name: "ContractItemId",
                table: "ServiceCalls");
        }
    }
}
