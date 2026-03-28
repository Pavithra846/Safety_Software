using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotifyHub.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddStackTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stack",
                columns: table => new
                {
                    StackID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CallID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StkNbr = table.Column<long>(type: "bigint", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDttm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDttm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stack", x => x.StackID);
                    table.ForeignKey(
                        name: "FK_Stack_Calls_CallID",
                        column: x => x.CallID,
                        principalTable: "Calls",
                        principalColumn: "CallID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stack_CallID",
                table: "Stack",
                column: "CallID");

            migrationBuilder.CreateIndex(
                name: "IX_Stack_StkNbr",
                table: "Stack",
                column: "StkNbr",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stack");
        }
    }
}
