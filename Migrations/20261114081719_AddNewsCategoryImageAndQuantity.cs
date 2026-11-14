using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsCategoryImageAndQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsCategoryImageName",
                table: "NewsCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NewsQuantitiy",
                table: "NewsCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsCategoryImageName",
                table: "NewsCategories");

            migrationBuilder.DropColumn(
                name: "NewsQuantitiy",
                table: "NewsCategories");
        }
    }
}
