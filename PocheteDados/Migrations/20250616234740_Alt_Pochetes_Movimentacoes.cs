using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PocheteAPI.Migrations
{
    /// <inheritdoc />
    public partial class Alt_Pochetes_Movimentacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Pochetes_PocheteId",
                table: "Movimentacoes");

            migrationBuilder.UpdateData(
                table: "Pochetes",
                keyColumn: "IdToken",
                keyValue: null,
                column: "IdToken",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "IdToken",
                table: "Pochetes",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PocheteId",
                table: "Movimentacoes",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Pochetes_IdToken",
                table: "Pochetes",
                column: "IdToken");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Pochetes_PocheteId",
                table: "Movimentacoes",
                column: "PocheteId",
                principalTable: "Pochetes",
                principalColumn: "IdToken",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Pochetes_PocheteId",
                table: "Movimentacoes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Pochetes_IdToken",
                table: "Pochetes");

            migrationBuilder.AlterColumn<string>(
                name: "IdToken",
                table: "Pochetes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<long>(
                name: "PocheteId",
                table: "Movimentacoes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Pochetes_PocheteId",
                table: "Movimentacoes",
                column: "PocheteId",
                principalTable: "Pochetes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
