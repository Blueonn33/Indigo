using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Indigo.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Journals",
                type: "nvarchar(700)",
                maxLength: 700,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Journals",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(700)",
                oldMaxLength: 700);
        }
    }
}
