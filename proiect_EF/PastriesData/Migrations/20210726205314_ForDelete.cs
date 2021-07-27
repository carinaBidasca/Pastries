using Microsoft.EntityFrameworkCore.Migrations;

namespace PastriesData.Migrations
{
    public partial class ForDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PastriesFactories_PastriesFactoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "PastriesFactories",
                newName: "Size");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PastriesFactories_PastriesFactoryId",
                table: "Products",
                column: "PastriesFactoryId",
                principalTable: "PastriesFactories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PastriesFactories_PastriesFactoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "PastriesFactories",
                newName: "size");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PastriesFactories_PastriesFactoryId",
                table: "Products",
                column: "PastriesFactoryId",
                principalTable: "PastriesFactories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
