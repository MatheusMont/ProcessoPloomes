using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPloomes.DATA.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoRelacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CatyegoriaId",
                table: "Produtos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatyegoriaId",
                table: "Produtos");
        }
    }
}
