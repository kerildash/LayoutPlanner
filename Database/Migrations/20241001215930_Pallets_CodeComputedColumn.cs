using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class Pallets_CodeComputedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodeWithoutId",
                table: "Pallets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Pallets",
                type: "text",
                nullable: true,
                computedColumnSql: "CASE WHEN \"CodeWithoutId\" IS NULL THEN \"Id\"::varchar(10) WHEN \"Id\"::varchar(10)  IS NULL THEN \"CodeWithoutId\" ELSE \"CodeWithoutId\" || \"Id\"::varchar(10) END",
                stored: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeWithoutId",
                table: "Pallets");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Pallets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldComputedColumnSql: "CASE WHEN \"CodeWithoutId\" IS NULL THEN \"Id\"::varchar(10) WHEN \"Id\"::varchar(10)  IS NULL THEN \"CodeWithoutId\" ELSE \"CodeWithoutId\" || \"Id\"::varchar(10) END");
        }
    }
}
