using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ajout_type_appartement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeAppartement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAppartement", x => x.Id);
                });

            migrationBuilder.AddColumn<double>(
               name: "NombreDeMetre2",
               table: "Appartement",
               type: "double precision",
               nullable: false,
               defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TypeAppartementId",
                table: "Appartement",
                type: "integer",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Appartement_TypeAppartementId",
                table: "Appartement",
                column: "TypeAppartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appartement_TypeAppartement_TypeAppartementId",
                table: "Appartement",
                column: "TypeAppartementId",
                principalTable: "TypeAppartement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
       table: "TypeAppartement",
       columns: new[] { "Id", "Nom" },
       values: new object[,]
       {
                { 1,  "Studio"},
                { 2,  "T1"},
                { 3,  "T1Bis"},
                { 4,  "T2"},
                { 5,  "T2Bis"},
                { 6,  "T3"},
                { 7,  "T3 Bis"},
                { 8,  "T3 Duplex"},
                { 9,  "T4"},
                { 10,  "T4 Duplex"},
                { 11,  "T5"},
                { 12,  "T5 Duplex"},
                { 13,  "Villa"},
      });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appartement_TypeAppartement_TypeAppartementId",
                table: "Appartement");

            migrationBuilder.DropTable(
                name: "TypeAppartement");

            migrationBuilder.DropIndex(
                name: "IX_Appartement_TypeAppartementId",
                table: "Appartement");

            migrationBuilder.DropColumn(
                name: "NombreDeMetre2",
                table: "Appartement");

            migrationBuilder.DropColumn(
                name: "TypeAppartementId",
                table: "Appartement");
        }
    }
}
