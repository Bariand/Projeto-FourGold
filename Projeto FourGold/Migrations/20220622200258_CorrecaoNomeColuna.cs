using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto_FourGold.Migrations
{
    public partial class CorrecaoNomeColuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataTransferencia",
                table: "Transacoes",
                newName: "DataTransacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataTransacao",
                table: "Transacoes",
                newName: "DataTransferencia");
        }
    }
}
