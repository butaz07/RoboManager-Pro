using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoboManager.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMeetingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "ScheduledAt",
                table: "Meetings",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "Purpose",
                table: "Meetings",
                newName: "Ubicacion");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Meetings",
                newName: "Titulo");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Proposito",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Proposito",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "Meetings",
                newName: "Purpose");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Meetings",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "Meetings",
                newName: "ScheduledAt");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
