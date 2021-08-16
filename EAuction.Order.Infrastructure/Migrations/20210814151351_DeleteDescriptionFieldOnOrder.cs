using Microsoft.EntityFrameworkCore.Migrations;

namespace EAuction.Order.Infrastructure.Migrations
{
    public partial class DeleteDescriptionFieldOnOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
