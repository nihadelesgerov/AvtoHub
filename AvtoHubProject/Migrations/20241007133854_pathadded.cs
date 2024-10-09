using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvtoHubProject.Migrations
{
    /// <inheritdoc />
    public partial class pathadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AvtoHubProducts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AvtoHubProducts");
        }
    }
}
