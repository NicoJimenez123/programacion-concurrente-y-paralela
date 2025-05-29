using System;
using System.Collections.Generic;
using System.IO;

class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public string Telefono { get; set; }
}

class Vehiculo
{
    public int Id { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Patente { get; set; }
}

class Venta
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int VehiculoId { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Monto { get; set; }
}

class ServicioPostVenta
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }
}

Random rnd = new Random();

string RandomString(int length)
{
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var str = new char[length];
    for (int i = 0; i < length; i++)
        str[i] = chars[rnd.Next(chars.Length)];
    return new string(str);
}

int GetNextId(string filePath)
{
    if (!File.Exists(filePath)) return 1;
    var lines = File.ReadAllLines(filePath);
    if (lines.Length == 0) return 1;
    var lastLine = lines[^1];
    var firstNumber = lastLine.Split(',')[0].Replace("{", "").Trim();
    if (int.TryParse(firstNumber, out int lastId))
        return lastId + 1;
    return 1;
}

// --- Código principal global ---
string clientesFile = "clientes.txt";
string vehiculosFile = "vehiculos.txt";
string ventasFile = "ventas.txt";
string serviciosFile = "servicios.txt";

int nextId = GetNextId(clientesFile);

var clientes = new List<string>();
var vehiculos = new List<string>();
var ventas = new List<string>();
var servicios = new List<string>();

for (int i = 0; i < 100000; i++)
{
    int id = nextId + i;

    var cliente = $"{{ {id}, \"Apellido{id}\", \"email{id}@mail.com\", \"Nombre{id}\", \"11{rnd.Next(10000000, 99999999)}\" }}";
    clientes.Add(cliente);

    var vehiculo = $"{{ {id}, \"Marca{rnd.Next(1, 5)}\", \"Modelo{rnd.Next(1, 10)}\", \"{RandomString(3)}{rnd.Next(100, 999)}\" }}";
    vehiculos.Add(vehiculo);

    var ventaFecha = DateTime.Now.AddDays(-rnd.Next(1, 100));
    var venta = $"{{ {id}, {id}, {id}, \"{ventaFecha:yyyy-MM-dd}\", {rnd.Next(100000, 500000)} }}";
    ventas.Add(venta);

    var servicioFecha = ventaFecha.AddDays(rnd.Next(1, 30));
    var servicio = $"{{ {id}, {id}, \"Servicio realizado {id}\", \"{servicioFecha:yyyy-MM-dd}\" }}";
    servicios.Add(servicio);
}

File.AppendAllLines(clientesFile, clientes);
File.AppendAllLines(vehiculosFile, vehiculos);
File.AppendAllLines(ventasFile, ventas);
File.AppendAllLines(serviciosFile, servicios);

Console.WriteLine("Datos generados y agregados a los archivos.");
