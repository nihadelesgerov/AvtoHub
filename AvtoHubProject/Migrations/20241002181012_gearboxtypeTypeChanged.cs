using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvtoHubProject.Migrations
{
    /// <inheritdoc />
    public partial class gearboxtypeTypeChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GearBoxType",
                table: "AvtoHubProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GearBoxType",
                table: "AvtoHubProducts",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
