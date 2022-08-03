using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainCarAPI.Migrations
{
    public partial class AddDisposalDateToRollingStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollingStock_Company_OwnerId",
                table: "RollingStock");

            migrationBuilder.DropForeignKey(
                name: "FK_RollingStock_Site_SiteId",
                table: "RollingStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Company_OwnerId",
                table: "Site");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Site",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "DisposalDate",
                table: "RollingStock",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999));

            migrationBuilder.AddForeignKey(
                name: "FK_RollingStock_Company_OwnerId",
                table: "RollingStock",
                column: "OwnerId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RollingStock_Site_SiteId",
                table: "RollingStock",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Company_OwnerId",
                table: "Site",
                column: "OwnerId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RollingStock_Company_OwnerId",
                table: "RollingStock");

            migrationBuilder.DropForeignKey(
                name: "FK_RollingStock_Site_SiteId",
                table: "RollingStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Company_OwnerId",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "DisposalDate",
                table: "RollingStock");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Site",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RollingStock_Company_OwnerId",
                table: "RollingStock",
                column: "OwnerId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RollingStock_Site_SiteId",
                table: "RollingStock",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Company_OwnerId",
                table: "Site",
                column: "OwnerId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
