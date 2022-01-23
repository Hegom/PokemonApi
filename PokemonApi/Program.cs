using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokemonApi.Core.Interface;
using PokemonApi.Core.Model;
using PokemonApi.Infrastructure;
using PokemonApi.Infrastructure.Respositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
var appSettingsSection = configuration.GetSection("AppSettings");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //c.SwaggerDoc("v1", new OpenApiInfo
    //{
    //    Title = "Pokemon API",
    //    Version = "v1"
    //});
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer",
        Description = "Please insert JWT token into field"
    });
    //  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    // {
    //    {
    //      new OpenApiSecurityScheme
    //      {
    //          Reference = new OpenApiReference
    //          {
    //              Type = ReferenceType.SecurityScheme,
    //              Id = "Bearer"
    //          }
    //      },
    //      new string[] { }
    //  }
    //});
});

builder.Services.AddResponseCaching();
builder.Services.AddCors();

builder.Services.AddDbContext<PokemonApiContext>(opt => opt.UseInMemoryDatabase("InMemoryPokemonDb"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPokemonRepository, UserRepository>();
builder.Services.AddSingleton<IPokemonRepository, UserRepository>();
builder.Services.Configure<AppSettings>(appSettingsSection);

var key = Encoding.ASCII.GetBytes(configuration["AppSettings:Secret"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();