using technical_tests_backend_ssr.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using technical_tests_backend_ssr.Repositories;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("appsettings.json configuration:");
Console.WriteLine($"ConnectionString: {builder.Configuration.GetConnectionString("DefaultConnection")}");
Console.WriteLine($"Logging Default Level: {builder.Configuration["Logging:LogLevel:Default"]}");
Console.WriteLine($"Logging Microsoft.AspNetCore Level: {builder.Configuration["Logging:LogLevel:Microsoft.AspNetCore"]}");
Console.WriteLine($"AllowedHosts: {builder.Configuration["AllowedHosts"]}");

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<ProductoDTOValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API SSR Motors Concesionaria",
        Version = "v1",
        Description = "Venta y Distribución de Autos",
        Contact = new OpenApiContact
        {
            Name = "Nicolás Jimenez",
            Email = "nicolasdanijimenez@hotmail.com"
        }
    });

    // Incluir comentarios XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21))
    )
);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Habilitar Swagger

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SSR Motors API v1");
    });


app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();


app.MapOpenApi();

//app.UseHttpsRedirection();
app.Run();

