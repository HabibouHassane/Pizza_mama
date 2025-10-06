using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// === Authentification par cookie ===
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/admin"; // Redirection si non authentifié
});

// === Ajout du DbContext avec SQLite ===
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// === Ajout de Razor Pages ===
builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

// === Configuration Culture ===
var cultureInfo = new CultureInfo("fr-FR");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
// =============================

// === Initialisation de la DB ===
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DataContext>();
        context.Database.EnsureCreated();
        // ou context.Database.Migrate(); si tu veux appliquer les migrations
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Une erreur est survenue lors de la création de la DB.");
    }
}

// === Middleware pipeline ===
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // avant UseAuthorization
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
