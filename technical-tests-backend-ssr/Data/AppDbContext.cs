using Microsoft.EntityFrameworkCore;
using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<ServicioPostVenta> ServiciosPostVenta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Apellido).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Telefono).IsRequired().HasMaxLength(15);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("Vehiculos");
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Marca).IsRequired().HasMaxLength(50);
            entity.Property(v => v.Modelo).IsRequired().HasMaxLength(50);
            entity.Property(v => v.Año).IsRequired();
            entity.Property(v => v.Precio).IsRequired().HasColumnType("decimal(10,2)");
            entity.Property(v => v.Stock).IsRequired();
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.ToTable("Ventas");
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Fecha).IsRequired();
            entity.Property(v => v.Total).IsRequired().HasColumnType("decimal(10,2)");
            
            entity.HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(v => v.Vehiculo)
                .WithMany()
                .HasForeignKey(v => v.VehiculoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ServicioPostVenta>(entity =>
        {
            entity.ToTable("ServiciosPostVenta");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.TipoServicio).IsRequired().HasMaxLength(100);
            entity.Property(s => s.Fecha).IsRequired();
            entity.Property(s => s.Estado).IsRequired().HasMaxLength(50);

            entity.HasOne(s => s.Cliente)
                .WithMany()
                .HasForeignKey(s => s.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        AppDbContext.Seed(modelBuilder);
    }

    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Clientes
        modelBuilder.Entity<Cliente>().HasData(
            new Cliente { Id = 1, Nombre = "Juan", Apellido = "Pérez", Email = "juan.perez@email.com", Telefono = "1234567890" },
            new Cliente { Id = 2, Nombre = "María", Apellido = "González", Email = "maria.gonzalez@email.com", Telefono = "0987654321" },
            new Cliente { Id = 3, Nombre = "Carlos", Apellido = "Rodríguez", Email = "carlos.rodriguez@email.com", Telefono = "5555555555" }
        );

        // Seed Vehículos
        modelBuilder.Entity<Vehiculo>().HasData(
            new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla", Año = 2023, Precio = 25000.00m, Stock = 5 },
            new Vehiculo { Id = 2, Marca = "Honda", Modelo = "Civic", Año = 2023, Precio = 23000.00m, Stock = 3 },
            new Vehiculo { Id = 3, Marca = "Ford", Modelo = "Mustang", Año = 2023, Precio = 45000.00m, Stock = 2 }
        );

        // Seed Ventas
        modelBuilder.Entity<Venta>().HasData(
            new Venta { Id = 1, ClienteId = 1, VehiculoId = 1, Fecha = DateTime.Now.AddDays(-30), Total = 25000.00m },
            new Venta { Id = 2, ClienteId = 2, VehiculoId = 2, Fecha = DateTime.Now.AddDays(-15), Total = 23000.00m },
            new Venta { Id = 3, ClienteId = 3, VehiculoId = 3, Fecha = DateTime.Now.AddDays(-7), Total = 45000.00m }
        );

        // Seed ServiciosPostVenta
        modelBuilder.Entity<ServicioPostVenta>().HasData(
            new ServicioPostVenta { Id = 1, ClienteId = 1, TipoServicio = "Mantenimiento Regular", Fecha = DateTime.Now.AddDays(-20), Estado = "Completado" },
            new ServicioPostVenta { Id = 2, ClienteId = 2, TipoServicio = "Reparación de Frenos", Fecha = DateTime.Now.AddDays(-10), Estado = "En Proceso" },
            new ServicioPostVenta { Id = 3, ClienteId = 3, TipoServicio = "Cambio de Aceite", Fecha = DateTime.Now.AddDays(-5), Estado = "Pendiente" }
        );
    }
}