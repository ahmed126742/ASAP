using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASAP.Presistance.Migrations
{
    /// <inheritdoc />
    public partial class Alter_Survey_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntlHeight",
                table: "Surveys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntlWidth",
                table: "Surveys",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntlHeight",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IntlWidth",
                table: "Surveys");
        }
    }
}
