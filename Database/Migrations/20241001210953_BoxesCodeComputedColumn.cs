using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class BoxesCodeComputedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Boxes",
                type: "text",
                nullable: true,
                computedColumnSql: "CASE WHEN \"CodeWithoutId\" IS NULL THEN \"Id\"::varchar(10) WHEN \"Id\"::varchar(10)  IS NULL THEN \"CodeWithoutId\" ELSE \"CodeWithoutId\" || \"Id\"::varchar(10) END",
                stored: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Boxes");
        }
    }
}
