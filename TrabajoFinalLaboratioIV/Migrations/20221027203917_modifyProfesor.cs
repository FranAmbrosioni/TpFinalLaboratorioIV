using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabajoFinalLaboratioIV.Migrations
{
    public partial class modifyProfesor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnoId",
                table: "profesores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_profesores_TurnoId",
                table: "profesores",
                column: "TurnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_profesores_turnos_TurnoId",
                table: "profesores",
                column: "TurnoId",
                principalTable: "turnos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profesores_turnos_TurnoId",
                table: "profesores");

            migrationBuilder.DropIndex(
                name: "IX_profesores_TurnoId",
                table: "profesores");

            migrationBuilder.DropColumn(
                name: "TurnoId",
                table: "profesores");
        }
    }
}
