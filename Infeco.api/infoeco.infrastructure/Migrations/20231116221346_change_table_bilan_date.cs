using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infoeco.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class change_table_bilan_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoisEnvoi",
                table: "Bilan");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Bilan",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Bilan");

            migrationBuilder.AddColumn<int>(
                name: "MoisEnvoi",
                table: "Bilan",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
