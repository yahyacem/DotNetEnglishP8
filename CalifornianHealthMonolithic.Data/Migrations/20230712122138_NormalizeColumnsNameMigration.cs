using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalifornianHealthMonolithic.Data.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeColumnsNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Patient",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patient",
                newName: "ID");
        }
    }
}
