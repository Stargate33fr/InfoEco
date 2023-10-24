using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init_database_infoeco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locataire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Mail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Telephone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IBAN = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locataire", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Ville",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CodePostal = table.Column<string>(type: "text", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ville", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgenceImmobiliere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Adresse = table.Column<string>(type: "text", nullable: false),
                    ComplementAdresse = table.Column<string>(type: "text", nullable: true),
                    VilleId = table.Column<int>(type: "integer", nullable: false),
                    FraisAgence = table.Column<double>(type: "double precision", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgenceImmobiliere", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgenceImmobiliere_Ville_VilleId",
                        column: x => x.VilleId,
                        principalTable: "Ville",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appartement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Adresse = table.Column<string>(type: "text", nullable: true),
                    ComplementAdresse = table.Column<string>(type: "text", nullable: true),
                    VilleId = table.Column<int>(type: "integer", nullable: false),
                    PrixDesCharges = table.Column<double>(type: "double precision", nullable: false),
                    Loyer = table.Column<double>(type: "double precision", nullable: false),
                    DepotDeGarantie = table.Column<double>(type: "double precision", nullable: false),
                    AgenceImmobiliereId = table.Column<int>(type: "integer", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appartement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appartement_AgenceImmobiliere_AgenceImmobiliereId",
                        column: x => x.AgenceImmobiliereId,
                        principalTable: "AgenceImmobiliere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appartement_Ville_VilleId",
                        column: x => x.VilleId,
                        principalTable: "Ville",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Prenom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Courriel = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Passe = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    EstActif = table.Column<bool>(type: "boolean", nullable: false),
                    AgenceImmobiliereId = table.Column<int>(type: "integer", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilisateur_AgenceImmobiliere_AgenceImmobiliereId",
                        column: x => x.AgenceImmobiliereId,
                        principalTable: "AgenceImmobiliere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocataireAppartement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocataireId = table.Column<int>(type: "integer", nullable: false),
                    AppartementId = table.Column<int>(type: "integer", nullable: false),
                    DepotDeGarantieRegle = table.Column<bool>(type: "boolean", nullable: false),
                    DateEntree = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateSortie = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocataireAppartement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocataireAppartement_Appartement_AppartementId",
                        column: x => x.AppartementId,
                        principalTable: "Appartement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocataireAppartement_Locataire_LocataireId",
                        column: x => x.LocataireId,
                        principalTable: "Locataire",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtatDesLieux",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DateEtatDesLieux = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Remarque = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    LocataireAppartementId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtatDesLieux", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtatDesLieux_LocataireAppartement_LocataireAppartementId",
                        column: x => x.LocataireAppartementId,
                        principalTable: "LocataireAppartement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paiement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocataireAppartementId = table.Column<int>(type: "integer", nullable: false),
                    Montant = table.Column<double>(type: "double precision", nullable: false),
                    ProvenancePaiementId = table.Column<int>(type: "integer", nullable: false),
                    DatePaiement = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paiement_LocataireAppartement_LocataireAppartementId",
                        column: x => x.LocataireAppartementId,
                        principalTable: "LocataireAppartement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paiement_ProvenancePaiement_ProvenancePaiementId",
                        column: x => x.ProvenancePaiementId,
                        principalTable: "ProvenancePaiement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuittanceLoyer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocataireAppartementId = table.Column<int>(type: "integer", nullable: false),
                    Mois = table.Column<int>(type: "integer", nullable: false),
                    Annee = table.Column<int>(type: "integer", nullable: false),
                    Montant = table.Column<double>(type: "double precision", nullable: false),
                    Paye = table.Column<bool>(type: "boolean", nullable: false),
                    CreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifieLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuittanceLoyer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuittanceLoyer_LocataireAppartement_LocataireAppartementId",
                        column: x => x.LocataireAppartementId,
                        principalTable: "LocataireAppartement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgenceImmobiliere_VilleId",
                table: "AgenceImmobiliere",
                column: "VilleId");

            migrationBuilder.CreateIndex(
                name: "IX_Appartement_AgenceImmobiliereId",
                table: "Appartement",
                column: "AgenceImmobiliereId");

            migrationBuilder.CreateIndex(
                name: "IX_Appartement_VilleId",
                table: "Appartement",
                column: "VilleId");

            migrationBuilder.CreateIndex(
                name: "IX_EtatDesLieux_LocataireAppartementId",
                table: "EtatDesLieux",
                column: "LocataireAppartementId");

            migrationBuilder.CreateIndex(
                name: "IX_LocataireAppartement_AppartementId",
                table: "LocataireAppartement",
                column: "AppartementId");

            migrationBuilder.CreateIndex(
                name: "IX_LocataireAppartement_LocataireId",
                table: "LocataireAppartement",
                column: "LocataireId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_LocataireAppartementId",
                table: "Paiement",
                column: "LocataireAppartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiement_ProvenancePaiementId",
                table: "Paiement",
                column: "ProvenancePaiementId");

            migrationBuilder.CreateIndex(
                name: "IX_QuittanceLoyer_LocataireAppartementId",
                table: "QuittanceLoyer",
                column: "LocataireAppartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_AgenceImmobiliereId",
                table: "Utilisateur",
                column: "AgenceImmobiliereId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_Courriel",
                table: "Utilisateur",
                column: "Courriel",
                unique: true);


            //insertion d'une ville avant de jouer le script d'insertion qui serait trop long en migration (voir script insertion_ville.sql)
            migrationBuilder.InsertData(
              table: "Ville",
              columns: new[] { "Id", "CreeLe", "Nom", "CodePostal"},
              values: new object[,]
              {
                  { 1, new DateTime(2023,4, 23, 12 , 0, 0, DateTimeKind.Utc),  "BORDEAUX", "33000"},
              });


            //insertion de l'agence

            migrationBuilder.InsertData(
              table: "AgenceImmobiliere",
              columns: new[] { "Id", "CreeLe", "Nom", "VilleId", "Adresse" , "FraisAgence"},
              values: new object[,]
              {
                  { 1, new DateTime(2023,4, 23, 12 , 0, 0, DateTimeKind.Utc),  "La première Agence", 1 , "3 rue dublanc", 8 },
              });


            //insertion du premier user avec login passe : utilisateur@lapremiereagence.com / utilisateurpremier33?
            migrationBuilder.InsertData(
           table: "Utilisateur",
           columns: new[] { "Id", "CreeLe", "AgenceImmobiliereId", "EstActif", "Passe", "Courriel", "Prenom", "Nom" },
           values: new object[,]
           {
                { 1, new DateTime(2023,4, 23, 12 , 0, 0, DateTimeKind.Utc), 1, true,  "n73UmX6dBtztnIq0TncxNWJxWWpf7LxSxCNZo7/Qy7UXs/OvNw5itjqZMaqYJhfr", "utilisateur@lapremiereagence.com", "Administrateur", "Administrateur"},
           });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EtatDesLieux");

            migrationBuilder.DropTable(
                name: "Paiement");

            migrationBuilder.DropTable(
                name: "QuittanceLoyer");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "ProvenancePaiement");

            migrationBuilder.DropTable(
                name: "LocataireAppartement");

            migrationBuilder.DropTable(
                name: "Appartement");

            migrationBuilder.DropTable(
                name: "Locataire");

            migrationBuilder.DropTable(
                name: "AgenceImmobiliere");

            migrationBuilder.DropTable(
                name: "Ville");
        }
    }
}
