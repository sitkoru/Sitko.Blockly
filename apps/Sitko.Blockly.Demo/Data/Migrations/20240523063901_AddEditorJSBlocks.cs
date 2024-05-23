using Microsoft.EntityFrameworkCore.Migrations;
using Sitko.EditorJS.Data;

#nullable disable

namespace Sitko.Blockly.Demo.Migrations
{
    /// <inheritdoc />
    public partial class AddEditorJSBlocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<EditorJSData>(
                name: "EditorJSBlocks",
                table: "Posts",
                type: "jsonb",
                nullable: false,
                defaultValueSql: "'{}'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditorJSBlocks",
                table: "Posts");
        }
    }
}
