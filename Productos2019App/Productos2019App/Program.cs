using Microsoft.EntityFrameworkCore; 
using Productos2019App.Models; 

// Crea un constructor de aplicaci�n web (WebApplicationBuilder).
var builder = WebApplication.CreateBuilder(args);

// Agrega soporte para controladores y vistas (MVC) al contenedor de servicios.
builder.Services.AddControllersWithViews();

// Configura el contexto de base de datos para que utilice SQL Server.
// Usa la cadena de conexi�n definida en appsettings.json bajo la clave "CadenaSQL".
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

// Agrega servicios de logging al contenedor (�til para registrar informaci�n, advertencias, errores, etc.).
builder.Services.AddLogging();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define la ruta por defecto para las solicitudes HTTP.
// Si no se especifica un controlador o acci�n, se usar� "ProductosController" y su acci�n "Index".
// La parte {id?} es opcional y representa un par�metro de ruta que puede pasarse a la acci�n.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productos}/{action=Index}/{id?}");

app.Run();
