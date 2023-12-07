using Microsoft.EntityFrameworkCore.Migrations;

namespace Sitko.Blockly.Demo.Migrations
{
    public partial class AddSecondaryBlocksToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Blocks",
                table: "Posts",
                type: "jsonb",
                nullable: false,
                defaultValueSql: "'[]'",
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryBlocks",
                table: "Posts",
                type: "jsonb",
                nullable: false,
                defaultValueSql: "'[]'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondaryBlocks",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "Blocks",
                table: "Posts",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldDefaultValueSql: "'[]'");
        }
    }
}
