using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BulkDataConsole.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "pubacc_em",
                schema: "main",
                columns: table => new
                {
                    record_type = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    unique_system_identifier = table.Column<decimal>(type: "numeric(10,0)", precision: 10, scale: 0, nullable: false),
                    uls_file_number = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    ebf_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    call_sign = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    location_number = table.Column<int>(type: "integer", nullable: true),
                    antenna_number = table.Column<int>(type: "integer", nullable: true),
                    frequency_assigned = table.Column<decimal>(type: "numeric(16,8)", precision: 16, scale: 8, nullable: true),
                    emission_action_performed = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    emission_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    digital_mod_rate = table.Column<decimal>(type: "numeric(8,1)", precision: 8, scale: 1, nullable: true),
                    digital_mod_type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    frequency_number = table.Column<int>(type: "integer", nullable: true),
                    status_code = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    status_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    emission_sequence_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_pubacc_em_unique_system_identifier",
                schema: "main",
                table: "pubacc_em",
                column: "unique_system_identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pubacc_em",
                schema: "main");
        }
    }
}
