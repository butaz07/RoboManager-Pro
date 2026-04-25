var builder = WebApplication.CreateBuilder(args);

// 1. Agregamos el servicio de MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 2. Archivos estáticos (para que funcione el CSS de Bootstrap si lo agregas luego)
app.UseStaticFiles();
app.UseRouting();

// 3. Configuramos la ruta por defecto para que abra directamente tu tabla de miembros
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Member}/{action=Index}/{id?}");

app.Run();