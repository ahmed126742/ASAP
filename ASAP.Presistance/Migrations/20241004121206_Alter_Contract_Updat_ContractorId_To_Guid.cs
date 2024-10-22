using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Contract_Updat_ContractorId_To_Guid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ContractorId",
                table: "Contracts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contracts",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Contractors_ContractorId",
                table: "Contracts",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Contractors_ContractorId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ContractorId",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "ContractorId",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
