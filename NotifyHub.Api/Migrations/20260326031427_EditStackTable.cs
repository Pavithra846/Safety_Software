using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifyHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class EditStackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stack_Calls_CallID",
                table: "Stack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stack",
                table: "Stack");

            migrationBuilder.RenameTable(
                name: "Stack",
                newName: "Stacks");

            migrationBuilder.RenameIndex(
                name: "IX_Stack_StkNbr",
                table: "Stacks",
                newName: "IX_Stacks_StkNbr");

            migrationBuilder.RenameIndex(
                name: "IX_Stack_CallID",
                table: "Stacks",
                newName: "IX_Stacks_CallID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stacks",
                table: "Stacks",
                column: "StackID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stacks_Calls_CallID",
                table: "Stacks",
                column: "CallID",
                principalTable: "Calls",
                principalColumn: "CallID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stacks_Calls_CallID",
                table: "Stacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stacks",
                table: "Stacks");

            migrationBuilder.RenameTable(
                name: "Stacks",
                newName: "Stack");

            migrationBuilder.RenameIndex(
                name: "IX_Stacks_StkNbr",
                table: "Stack",
                newName: "IX_Stack_StkNbr");

            migrationBuilder.RenameIndex(
                name: "IX_Stacks_CallID",
                table: "Stack",
                newName: "IX_Stack_CallID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stack",
                table: "Stack",
                column: "StackID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stack_Calls_CallID",
                table: "Stack",
                column: "CallID",
                principalTable: "Calls",
                principalColumn: "CallID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
