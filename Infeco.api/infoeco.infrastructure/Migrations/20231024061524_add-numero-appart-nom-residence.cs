using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnumeroappartnomresidence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomResidence",
                table: "Appartement",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumeroAppartement",
                table: "Appartement",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomResidence",
                table: "Appartement");

            migrationBuilder.DropColumn(
                name: "NumeroAppartement",
                table: "Appartement");
        }
    }
}
