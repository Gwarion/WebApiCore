using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlaceHolder.DrivenAdapter.SQLServer.Migrations
{
    public partial class CREATE_ADDRESS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressGuid",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AddressGuid",
                table: "Consumers",
                column: "AddressGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Address_AddressGuid",
                table: "Consumers",
                column: "AddressGuid",
                principalTable: "Address",
                principalColumn: "Guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Address_AddressGuid",
                table: "Consumers");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AddressGuid",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AddressGuid",
                table: "Consumers");
        }
    }
}
