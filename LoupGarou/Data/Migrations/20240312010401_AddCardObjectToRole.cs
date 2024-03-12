using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoupGarou.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCardObjectToRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Roles_CardId",
                table: "Roles",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Cards_CardId",
                table: "Roles",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Cards_CardId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_CardId",
                table: "Roles");
        }
    }
}
