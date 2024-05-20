using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastKala.Application.Migrations;

/// <inheritdoc />
public partial class addcustomusermodel : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Address",
            table: "Users",
            type: "nvarchar(1000)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "City",
            table: "Users",
            type: "nvarchar(500)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "Users",
            type: "nvarchar(500)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "Users",
            type: "nvarchar(500)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "NationalCode",
            table: "Users",
            type: "nvarchar(11)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Town",
            table: "Users",
            type: "nvarchar(500)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Address",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "City",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "LastName",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "NationalCode",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "Town",
            table: "Users");
    }
}
