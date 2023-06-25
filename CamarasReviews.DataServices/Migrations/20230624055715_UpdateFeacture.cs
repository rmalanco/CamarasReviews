using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CamarasReviews.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeacture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Features",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Features",
                newName: "Name");
        }
    }
}
