using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

// --- Leer credenciales desde appsettings.json ---
string appSettingsPath = "technical-tests-backend-ssr/appsettings.json";
string json = File.ReadAllText(appSettingsPath);
var match = Regex.Match(json, "\"DefaultConnection\"\\s*:\\s*\"([^\"]+)\"");
if (!match.Success)
{
    Console.WriteLine("No se pudo encontrar la cadena de conexión en appsettings.json");
    return;
}
string connectionString = match.Groups[1].Value;

// Extraer datos de la cadena de conexión
string mysqlUser = Regex.Match(connectionString, @"user=([^;]+)").Groups[1].Value;
string mysqlPassword = Regex.Match(connectionString, @"password=([^;]+)").Groups[1].Value;
string mysqlDatabase = Regex.Match(connectionString, @"database=([^;]+)").Groups[1].Value;
string containerId = "95d1a804dbf2"; // ID de tu contenedor

Random rnd = new Random();

string RandomString(int length)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var str = new char[length];
    for (int i = 0; i < length; i++)
        str[i] = chars[rnd.Next(chars.Length)];
    return new string(str);
}

// --- Generación de datos e inserción SQL ---
int cantidad = 10000; // Cambia la cantidad si lo deseas

var clientesSql = new StringBuilder();
var vehiculosSql = new StringBuilder();
var ventasSql = new StringBuilder();
var serviciosSql = new StringBuilder();

for (int i = 1; i <= cantidad; i++)
{
    int id = i;

    // Cliente
    string nombre = $"Nombre{id}";
    string apellido = $"Apellido{id}";
    string email = $"email{id}@mail.com";
    string telefono = $"11{rnd.Next(10000000, 99999999)}";
    clientesSql.AppendLine(
        $"INSERT INTO Clientes (Id, Nombre, Apellido, Email, Telefono) VALUES ({id}, '{nombre}', '{apellido}', '{email}', '{telefono}');"
    );

    // Vehiculo
    string marca = $"Marca{rnd.Next(1, 5)}";
    string modelo = $"Modelo{rnd.Next(1, 10)}";
    int año = rnd.Next(2000, 2024);
    decimal precio = rnd.Next(2000000, 10000000) + rnd.Next(0, 99) / 100m;
    int stock = rnd.Next(1, 20);
    vehiculosSql.AppendLine(
        $"INSERT INTO Vehiculos (Id, Marca, Modelo, Año, Precio, Stock) VALUES ({id}, '{marca}', '{modelo}', {año}, {precio.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}, {stock});"
    );

    // Venta
    DateTime ventaFecha = DateTime.Now.AddDays(-rnd.Next(1, 100));
    decimal total = rnd.Next(2000000, 10000000) + rnd.Next(0, 99) / 100m;
    ventasSql.AppendLine(
        $"INSERT INTO Ventas (Id, ClienteId, VehiculoId, Fecha, Total) VALUES ({id}, {id}, {id}, '{ventaFecha:yyyy-MM-dd}', {total.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)});"
    );

    // ServicioPostVenta
    DateTime servicioFecha = ventaFecha.AddDays(rnd.Next(1, 30));
    string tipoServicio = $"Servicio tipo {rnd.Next(1, 5)}";
    string estado = rnd.Next(0, 2) == 0 ? "Pendiente" : "Completado";
    serviciosSql.AppendLine(
        $"INSERT INTO ServiciosPostVenta (Id, ClienteId, TipoServicio, Fecha, Estado) VALUES ({id}, {id}, '{tipoServicio}', '{servicioFecha:yyyy-MM-dd}', '{estado}');"
    );
}

// Guardar el script SQL en un archivo temporal
string scriptFile = "technical-tests-backend-ssr/utils/insertar_datos.sql";
File.WriteAllText(scriptFile, clientesSql.ToString() + vehiculosSql + ventasSql + serviciosSql);

// // Ejecutar el script dentro del contenedor Docker
// string dockerCmd = $"docker exec -i {containerId} mysql -u{mysqlUser} -p{mysqlPassword} {mysqlDatabase} < {scriptFile}";

// // Ejecutar el comando en bash
// var process = new Process();
// process.StartInfo.FileName = "/bin/bash";
// process.StartInfo.Arguments = $"-c \"{dockerCmd}\"";
// process.StartInfo.RedirectStandardOutput = true;
// process.StartInfo.RedirectStandardError = true;
// process.StartInfo.UseShellExecute = false;
// process.Start();

// string output = process.StandardOutput.ReadToEnd();
// string error = process.StandardError.ReadToEnd();
// process.WaitForExit();

// Console.WriteLine("Ejecución finalizada.");
// if (!string.IsNullOrEmpty(output))
//     Console.WriteLine("Salida:\n" + output);
// if (!string.IsNullOrEmpty(error))
//     Console.WriteLine("Errores:\n" + error);
