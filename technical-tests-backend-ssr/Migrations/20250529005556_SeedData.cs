using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace technical_tests_backend_ssr.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Apellido", "Email", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "Pérez", "juan.perez@email.com", "Juan", "1234567890" },
                    { 2, "González", "maria.gonzalez@email.com", "María", "0987654321" },
                    { 3, "Rodríguez", "carlos.rodriguez@email.com", "Carlos", "5555555555" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculos",
                columns: new[] { "Id", "Año", "Marca", "Modelo", "Precio", "Stock" },
                values: new object[,]
                {
                    { 1, 2023, "Toyota", "Corolla", 25000.00m, 5 },
                    { 2, 2023, "Honda", "Civic", 23000.00m, 3 },
                    { 3, 2023, "Ford", "Mustang", 45000.00m, 2 }
                });

            migrationBuilder.InsertData(
                table: "ServiciosPostVenta",
                columns: new[] { "Id", "ClienteId", "Estado", "Fecha", "TipoServicio" },
                values: new object[,]
                {
                    { 1, 1, "Completado", new DateTime(2025, 5, 8, 21, 55, 55, 815, DateTimeKind.Local).AddTicks(6808), "Mantenimiento Regular" },
                    { 2, 2, "En Proceso", new DateTime(2025, 5, 18, 21, 55, 55, 815, DateTimeKind.Local).AddTicks(6811), "Reparación de Frenos" },
                    { 3, 3, "Pendiente", new DateTime(2025, 5, 23, 21, 55, 55, 815, DateTimeKind.Local).AddTicks(6813), "Cambio de Aceite" }
                });

            migrationBuilder.InsertData(
                table: "Ventas",
                columns: new[] { "Id", "ClienteId", "Fecha", "Total", "VehiculoId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 4, 28, 21, 55, 55, 815, DateTimeKind.Local).AddTicks(6742), 25000.00m, 1 },
                    { 2, 2, new DateTime(2025, 5, 13, 21, 55, 55, 815, DateTimeKind.Local).AddTicks(6776), 23000.00m, 2 },
                    { 3, 3, new DateTime(2025, 5, 21, 21, 55, 55, 815, DateTimeKind.Local).AddTicks(6779), 45000.00m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiciosPostVenta",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiciosPostVenta",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiciosPostVenta",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ventas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehiculos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
