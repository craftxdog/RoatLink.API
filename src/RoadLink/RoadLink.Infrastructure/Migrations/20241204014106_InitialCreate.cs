using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoadLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    apellido = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehiculos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    modelo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    vin = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    precio_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    mantenimiento_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    mantenimiento_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    fecha_ultimo_alquiler = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    accesorio = table.Column<int[]>(type: "integer[]", nullable: false),
                    direccion_pais = table.Column<string>(type: "text", nullable: true),
                    direccion_departamento = table.Column<string>(type: "text", nullable: true),
                    direccion_provincia = table.Column<string>(type: "text", nullable: true),
                    direccion_ciudad = table.Column<string>(type: "text", nullable: true),
                    direccion_calle = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehiculos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alquileres",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    precio_por_periodo_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_por_periodo_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    precio_mantenimiento_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_mantenimiento_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    accesorios_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    accesorios_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    precio_total_monto = table.Column<decimal>(type: "numeric", nullable: true),
                    precio_total_tipo_moneda = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    duracion_alquiler_inicio = table.Column<DateOnly>(type: "date", nullable: false),
                    duracion_alquiler_termino = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_creacion_alquiler = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_confirmacion_alquiler = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_de_negacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_completo_alquiler = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_cancelacion_alquiler = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_alquileres", x => x.id);
                    table.ForeignKey(
                        name: "fk_alquileres_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_alquileres_vehiculo_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    vehiculo_id = table.Column<Guid>(type: "uuid", nullable: false),
                    alquiler_id = table.Column<Guid>(type: "uuid", nullable: false),
                    usuario_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    fecha_hora_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comentarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_comentarios_alquileres_alquiler_id",
                        column: x => x.alquiler_id,
                        principalTable: "alquileres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_comentarios_vehiculo_vehiculo_id",
                        column: x => x.vehiculo_id,
                        principalTable: "vehiculos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_alquileres_usuario_id",
                table: "alquileres",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_alquileres_vehiculo_id",
                table: "alquileres",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_alquiler_id",
                table: "comentarios",
                column: "alquiler_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_usuario_id",
                table: "comentarios",
                column: "usuario_id");

            migrationBuilder.CreateIndex(
                name: "ix_comentarios_vehiculo_id",
                table: "comentarios",
                column: "vehiculo_id");

            migrationBuilder.CreateIndex(
                name: "ix_usuario_email",
                table: "usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentarios");

            migrationBuilder.DropTable(
                name: "alquileres");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "vehiculos");
        }
    }
}
