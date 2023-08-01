using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle1", new DateTime(2023, 8, 1, 17, 42, 55, 681, DateTimeKind.Local).AddTicks(9671), new DateTime(2023, 8, 1, 17, 42, 55, 681, DateTimeKind.Local).AddTicks(9718), "", 50, "Villa Real", 5, 2000.0 },
                    { 2, "", "Detalle2", new DateTime(2023, 8, 1, 17, 42, 55, 681, DateTimeKind.Local).AddTicks(9722), new DateTime(2023, 8, 1, 17, 42, 55, 681, DateTimeKind.Local).AddTicks(9724), "", 70, "Villa Las Rastras", 5, 3000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
