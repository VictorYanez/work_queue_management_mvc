using Microsoft.EntityFrameworkCore;
using queue_management.Data;

var builder = WebApplication.CreateBuilder(args);

//Configuración de la conexión a SQL Server local (Archivo de contexto)
builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// (Otra sintaxis)  opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{ 
    var applicationDBContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    applicationDBContext.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
