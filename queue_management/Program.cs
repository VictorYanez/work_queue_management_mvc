using Microsoft.EntityFrameworkCore;
using queue_management.Data;

var builder = WebApplication.CreateBuilder(args);

//Configuraci�n de la conexi�n a SQL Server local (Archivo de contexto)
builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
opciones.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//------------------------------------------------------------------
// Configuraci�n para manejar las pol�ticas de cookies
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Establece la pol�tica SameSite como Lax o None seg�n el entorno
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
    options.Secure = CookieSecurePolicy.SameAsRequest; // Solo aplica `Secure` en HTTPS
});

// Configura cookies antifalsificaci�n con `Secure` solo en HTTPS
builder.Services.AddAntiforgery(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
});
//------------------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();
// (Otra sintaxis)  opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Aplicaci�n de migraciones al iniciar
using (var scope = app.Services.CreateScope())
{ 
    var applicationDBContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    applicationDBContext.Database.Migrate();
}
// Configure the HTTP request pipeline de middleware.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Activa la pol�tica de cookies y autorizaci�n antes de las rutas
app.UseCookiePolicy();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
