using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZayShop.Migrations
{
    /// <inheritdoc />
    public partial class CategoryCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Categories",
                newName: "ImageName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Categories",
                newName: "Image");
        }
    }
}
