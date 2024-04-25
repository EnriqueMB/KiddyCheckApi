
using KiddyCheckApi.Core.Helpers;
using KiddyCheckApi.Core.Services;
using KiddyCheckApi.DataAccess;
using KiddyCheckApi.DataAccess.Entities;
using KiddyCheckApi.DataAccess.IRepositories;
using KiddyCheckApi.DataAccess.Repositories;
using KiddyCheckApi.Core.Middleware;
using KiddyCheckApi.DataAccess.IRepositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJWTTokenServices(builder.Configuration);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

#region <-- Services -->
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PermisosService>();
builder.Services.AddScoped<PersonasService>();
#endregion
#region <-- Repositories -->
builder.Services.AddScoped<IBaseRepository<Permisos>, BaseRepository<Permisos>>();
builder.Services.AddScoped<IBaseRepository<Usuarios>, BaseRepository<Usuarios>>();
builder.Services.AddScoped<IBaseRepository<UsuarioPermisos>, BaseRepository<UsuarioPermisos>>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IBaseRepository<Permisos>, BaseRepository<Permisos>>();
builder.Services.AddScoped<IBaseRepository<Personas>, BaseRepository<Personas>>();
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
#endregion
#region <-- Context -->

builder.Services.AddDbContext<KiddyCheckDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction => sqlServerOptionsAction.CommandTimeout(120));
});

#endregion
builder.Services.AddMvc();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("*");
    });
});

var app = builder.Build();

#region CultureSetup
var supportedCultures = new[]
{
 new CultureInfo("es-MX")
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-MX"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.EnvironmentName.Equals("QA"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
#region MIDDLEWARE FILTER
app.UseLimitMiddleware();
#endregion
app.UseAuthorization();

app.MapControllers();

app.Run();
