using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifyHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class updateUnitConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "StkNbr",
                table: "Units",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Units_StkNbr",
                table: "Units",
                column: "StkNbr",
                unique: true,
                filter: "[StkNbr] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Units_StkNbr",
                table: "Units");

            migrationBuilder.AlterColumn<long>(
                name: "StkNbr",
                table: "Units",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
