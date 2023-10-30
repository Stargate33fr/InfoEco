using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class paiement_assigner_a_locatireAppartement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_Locataire_LocataireId",
                table: "Paiement");

            migrationBuilder.RenameColumn(
                name: "LocataireId",
                table: "Paiement",
                newName: "LocataireAppartementId");

            migrationBuilder.RenameIndex(
                name: "IX_Paiement_LocataireId",
                table: "Paiement",
                newName: "IX_Paiement_LocataireAppartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_LocataireAppartement_LocataireAppartementId",
                table: "Paiement",
                column: "LocataireAppartementId",
                principalTable: "LocataireAppartement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_LocataireAppartement_LocataireAppartementId",
                table: "Paiement");

            migrationBuilder.RenameColumn(
                name: "LocataireAppartementId",
                table: "Paiement",
                newName: "LocataireId");

            migrationBuilder.RenameIndex(
                name: "IX_Paiement_LocataireAppartementId",
                table: "Paiement",
                newName: "IX_Paiement_LocataireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_Locataire_LocataireId",
                table: "Paiement",
                column: "LocataireId",
                principalTable: "Locataire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
