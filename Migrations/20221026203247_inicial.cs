using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrabajoFinalLaboratioIV.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actividad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actividad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "turnos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turnos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "profesores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    fechaNac = table.Column<DateTime>(nullable: false),
                    ActividadId = table.Column<int>(nullable: false),
                    foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profesores_actividad_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "actividad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Inscripcion = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false),
                    fechaNac = table.Column<DateTime>(nullable: false),
                    actividadId = table.Column<int>(nullable: false),
                    turnoId = table.Column<int>(nullable: false),
                    foto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_alumnos_actividad_actividadId",
                        column: x => x.actividadId,
                        principalTable: "actividad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alumnos_turnos_turnoId",
                        column: x => x.turnoId,
                        principalTable: "turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alumnos_actividadId",
                table: "alumnos",
                column: "actividadId");

            migrationBuilder.CreateIndex(
                name: "IX_alumnos_turnoId",
                table: "alumnos",
                column: "turnoId");

            migrationBuilder.CreateIndex(
                name: "IX_profesores_ActividadId",
                table: "profesores",
                column: "ActividadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alumnos");

            migrationBuilder.DropTable(
                name: "profesores");

            migrationBuilder.DropTable(
                name: "turnos");

            migrationBuilder.DropTable(
                name: "actividad");
        }
    }
}
