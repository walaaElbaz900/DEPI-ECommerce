using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceProject.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ageadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "AspNetUsers");
        }
    }
}
