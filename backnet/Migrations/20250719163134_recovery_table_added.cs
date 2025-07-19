using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backnet.Migrations
{
    /// <inheritdoc />
    public partial class recovery_table_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "recovery",
                columns: table => new
                {
                    RecoveryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecoveryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recovery", x => x.RecoveryId);
                    table.ForeignKey(
                        name: "FK_recovery_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_recovery_UserId",
                table: "recovery",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "recovery");
        }
    }
}
