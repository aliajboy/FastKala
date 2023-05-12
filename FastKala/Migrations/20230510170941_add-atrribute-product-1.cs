using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastKala.Migrations
{
    /// <inheritdoc />
    public partial class addatrributeproduct1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProductAttributes");
        }
    }
}
