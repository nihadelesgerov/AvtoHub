using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvtoHubProject.Migrations
{
    /// <inheritdoc />
    public partial class TransMitterPropertyDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transmitter",
                table: "AvtoHubProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Transmitter",
                table: "AvtoHubProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
