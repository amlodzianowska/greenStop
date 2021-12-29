using Microsoft.EntityFrameworkCore.Migrations;

namespace greenStop.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeUnit",
                table: "Plants");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Plants",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "PetFriendly",
                table: "Plants",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Plants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sun",
                table: "Plants",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Watering",
                table: "Plants",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PetFriendly",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Sun",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Watering",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Plants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "SizeUnit",
                table: "Plants",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");
        }
    }
}
