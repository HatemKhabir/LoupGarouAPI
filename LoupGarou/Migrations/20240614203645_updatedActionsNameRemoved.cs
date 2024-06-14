using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoupGarou.Migrations
{
    /// <inheritdoc />
    public partial class updatedActionsNameRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Actions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Actions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
