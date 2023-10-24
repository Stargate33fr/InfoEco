using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_locatireAppartement_key_alternative : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtatDesLieux_LocataireAppartement_LocataireAppartementLocat~",
                table: "EtatDesLieux");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_LocataireAppartement_LocataireAppartementLocataire~",
                table: "Paiement");

            migrationBuilder.DropForeignKey(
                name: "FK_QuittanceLoyer_LocataireAppartement_LocataireAppartementLoc~",
                table: "QuittanceLoyer");

            migrationBuilder.DropIndex(
                name: "IX_QuittanceLoyer_LocataireAppartementLocataireId_LocataireApp~",
                table: "QuittanceLoyer");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_LocataireAppartementLocataireId_LocataireApparteme~",
                table: "Paiement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocataireAppartement",
                table: "LocataireAppartement");

            migrationBuilder.DropIndex(
                name: "IX_EtatDesLieux_LocataireAppartementLocataireId_LocataireAppar~",
                table: "EtatDesLieux");

            migrationBuilder.DropColumn(
                name: "LocataireAppartementAppartementId",
                table: "QuittanceLoyer");

            migrationBuilder.DropColumn(
                name: "LocataireAppartementLocataireId",
                table: "QuittanceLoyer");

            migrationBuilder.DropColumn(
                name: "LocataireAppartementAppartementId",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "LocataireAppartementLocataireId",
                table: "Paiement");

            migrationBuilder.DropColumn(
                name: "LocataireAppartementAppartementId",
                table: "EtatDesLieux");

            migrationBuilder.DropColumn(
                name: "LocataireAppartementLocataireId",
                table: "EtatDesLieux");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LocataireAppartement",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_LocataireAppartement_LocataireId_AppartementId",
                table: "LocataireAppartement",
                columns: new[] { "LocataireId", "AppartementId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocataireAppartement",
                table: "LocataireAppartement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_QuittanceLoyer_LocataireAppartementId",
                table: "QuittanceLoyer",
                column: "LocataireAppartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_LocataireAppartementId",
                table: "Paiement",
                column: "LocataireAppartementId");

            migrationBuilder.CreateIndex(
                name: "IX_EtatDesLieux_LocataireAppartementId",
                table: "EtatDesLieux",
                column: "LocataireAppartementId");

            migrationBuilder.AddForeignKey(
                name: "FK_EtatDesLieux_LocataireAppartement_LocataireAppartementId",
                table: "EtatDesLieux",
                column: "LocataireAppartementId",
                principalTable: "LocataireAppartement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_LocataireAppartement_LocataireAppartementId",
                table: "Paiement",
                column: "LocataireAppartementId",
                principalTable: "LocataireAppartement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuittanceLoyer_LocataireAppartement_LocataireAppartementId",
                table: "QuittanceLoyer",
                column: "LocataireAppartementId",
                principalTable: "LocataireAppartement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EtatDesLieux_LocataireAppartement_LocataireAppartementId",
                table: "EtatDesLieux");

            migrationBuilder.DropForeignKey(
                name: "FK_Paiement_LocataireAppartement_LocataireAppartementId",
                table: "Paiement");

            migrationBuilder.DropForeignKey(
                name: "FK_QuittanceLoyer_LocataireAppartement_LocataireAppartementId",
                table: "QuittanceLoyer");

            migrationBuilder.DropIndex(
                name: "IX_QuittanceLoyer_LocataireAppartementId",
                table: "QuittanceLoyer");

            migrationBuilder.DropIndex(
                name: "IX_Paiement_LocataireAppartementId",
                table: "Paiement");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_LocataireAppartement_LocataireId_AppartementId",
                table: "LocataireAppartement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocataireAppartement",
                table: "LocataireAppartement");

            migrationBuilder.DropIndex(
                name: "IX_EtatDesLieux_LocataireAppartementId",
                table: "EtatDesLieux");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LocataireAppartement");

            migrationBuilder.AddColumn<int>(
                name: "LocataireAppartementAppartementId",
                table: "QuittanceLoyer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocataireAppartementLocataireId",
                table: "QuittanceLoyer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocataireAppartementAppartementId",
                table: "Paiement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocataireAppartementLocataireId",
                table: "Paiement",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocataireAppartementAppartementId",
                table: "EtatDesLieux",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocataireAppartementLocataireId",
                table: "EtatDesLieux",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocataireAppartement",
                table: "LocataireAppartement",
                columns: new[] { "LocataireId", "AppartementId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuittanceLoyer_LocataireAppartementLocataireId_LocataireApp~",
                table: "QuittanceLoyer",
                columns: new[] { "LocataireAppartementLocataireId", "LocataireAppartementAppartementId" });

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_LocataireAppartementLocataireId_LocataireApparteme~",
                table: "Paiement",
                columns: new[] { "LocataireAppartementLocataireId", "LocataireAppartementAppartementId" });

            migrationBuilder.CreateIndex(
                name: "IX_EtatDesLieux_LocataireAppartementLocataireId_LocataireAppar~",
                table: "EtatDesLieux",
                columns: new[] { "LocataireAppartementLocataireId", "LocataireAppartementAppartementId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EtatDesLieux_LocataireAppartement_LocataireAppartementLocat~",
                table: "EtatDesLieux",
                columns: new[] { "LocataireAppartementLocataireId", "LocataireAppartementAppartementId" },
                principalTable: "LocataireAppartement",
                principalColumns: new[] { "LocataireId", "AppartementId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paiement_LocataireAppartement_LocataireAppartementLocataire~",
                table: "Paiement",
                columns: new[] { "LocataireAppartementLocataireId", "LocataireAppartementAppartementId" },
                principalTable: "LocataireAppartement",
                principalColumns: new[] { "LocataireId", "AppartementId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuittanceLoyer_LocataireAppartement_LocataireAppartementLoc~",
                table: "QuittanceLoyer",
                columns: new[] { "LocataireAppartementLocataireId", "LocataireAppartementAppartementId" },
                principalTable: "LocataireAppartement",
                principalColumns: new[] { "LocataireId", "AppartementId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
