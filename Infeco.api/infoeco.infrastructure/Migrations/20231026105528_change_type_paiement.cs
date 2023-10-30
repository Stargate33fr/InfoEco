using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_type_paiement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_ProvenancePaiement_ProvenancePaiementId",
                table: "Paiement");

            migrationBuilder.DropTable(
                name: "ProvenancePaiement");

            migrationBuilder.RenameColumn(
                name: "ProvenancePaiementId",
                table: "Paiement",
                newName: "TypePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_Paiement_ProvenancePaiementId",
                table: "Paiement",
                newName: "IX_Paiement_TypePaiementId");

            migrationBuilder.CreateTable(
                name: "TypePaiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePaiement", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_TypePaiement_TypePaiementId",
                table: "Paiement",
                column: "TypePaiementId",
                principalTable: "TypePaiement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.InsertData(
        table: "TypePaiement",
        columns: new[] { "Id", "Nom" },
        values: new object[,]
        {
              { 1,  "Locataire"},
               { 2,  "CAF"},
                { 3,  "Dépot de garantie"},
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_TypePaiement_TypePaiementId",
                table: "Paiement");

            migrationBuilder.DropTable(
                name: "TypePaiement");

            migrationBuilder.RenameColumn(
                name: "TypePaiementId",
                table: "Paiement",
                newName: "ProvenancePaiementId");

            migrationBuilder.RenameIndex(
                name: "IX_Paiement_TypePaiementId",
                table: "Paiement",
                newName: "IX_Paiement_ProvenancePaiementId");

            migrationBuilder.CreateTable(
                name: "ProvenancePaiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvenancePaiement", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_ProvenancePaiement_ProvenancePaiementId",
                table: "Paiement",
                column: "ProvenancePaiementId",
                principalTable: "ProvenancePaiement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
