using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class civilite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Civilite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Civilite", x => x.Id);
                });

            migrationBuilder.InsertData(
           table: "Civilite",
           columns: new[] { "Id", "Nom" },
           values: new object[,]
           {
                        { 1,  "Monsieur"},
                        { 2,  "Madame"},
                        { 3,  "Mademoiselle"},
           });

            migrationBuilder.AddColumn<int>(
              name: "CiviliteId",
              table: "Locataire",
              type: "integer",
              nullable: false,
              defaultValue: 1);


            migrationBuilder.CreateIndex(
                name: "IX_Locataire_CiviliteId",
                table: "Locataire",
                column: "CiviliteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locataire_Civilite_CiviliteId",
                table: "Locataire",
                column: "CiviliteId",
                principalTable: "Civilite",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locataire_Civilite_CiviliteId",
                table: "Locataire");

            migrationBuilder.DropTable(
                name: "Civilite");

            migrationBuilder.DropIndex(
                name: "IX_Locataire_CiviliteId",
                table: "Locataire");

            migrationBuilder.DropColumn(
                name: "CiviliteId",
                table: "Locataire");
        }
    }
}
