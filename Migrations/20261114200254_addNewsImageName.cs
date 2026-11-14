using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class addNewsImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewsImageName",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsImageName",
                table: "News");
        }
    }
}
