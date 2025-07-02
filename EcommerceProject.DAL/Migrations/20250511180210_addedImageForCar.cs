using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedImageForCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagesUrl",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagesUrl",
                table: "Cars");
        }
    }
}
