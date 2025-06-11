using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsyncProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PorductDetails",
                table: "Products",
                newName: "ProductDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductDetails",
                table: "Products",
                newName: "PorductDetails");
        }
    }
}
