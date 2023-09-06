using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_System.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Permissions_PermissionsId",
                table: "GroupPermissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupPermissions_PermissionsId",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "PermissionsId",
                table: "GroupPermissions");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_PermissionID",
                table: "GroupPermissions",
                column: "PermissionID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Permissions_PermissionID",
                table: "GroupPermissions",
                column: "PermissionID",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermissions_Permissions_PermissionID",
                table: "GroupPermissions");

            migrationBuilder.DropIndex(
                name: "IX_GroupPermissions_PermissionID",
                table: "GroupPermissions");

            migrationBuilder.AddColumn<int>(
                name: "PermissionsId",
                table: "GroupPermissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermissions_PermissionsId",
                table: "GroupPermissions",
                column: "PermissionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermissions_Permissions_PermissionsId",
                table: "GroupPermissions",
                column: "PermissionsId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
