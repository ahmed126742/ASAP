using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class Alter_ContractItem_to_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCalls_contractItems_ContractItemId",
                table: "ServiceCalls");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractItemId",
                table: "ServiceCalls",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCalls_contractItems_ContractItemId",
                table: "ServiceCalls",
                column: "ContractItemId",
                principalTable: "contractItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceCalls_contractItems_ContractItemId",
                table: "ServiceCalls");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContractItemId",
                table: "ServiceCalls",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceCalls_contractItems_ContractItemId",
                table: "ServiceCalls",
                column: "ContractItemId",
                principalTable: "contractItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
