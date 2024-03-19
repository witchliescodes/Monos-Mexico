using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using UNAM.PrimatesApi.Entidades;
using UNAM.PrimatesApi.Infrastructure;
using UNAM.PrimatesApi.Interfaces;
using UNAM.PrimatesApi.Models;
using UNAM.PrimatesApi.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin().
            AllowAnyMethod().
            AllowAnyHeader();
}));

builder.Services.AddDbContext<ConservacionDb>(db => db.UseSqlite(builder.Configuration
    .GetConnectionString("DemoDB")),
    ServiceLifetime.Singleton);



// jwtoken fonfig
JwtTokenConfig jwtTokenConfig = builder.Configuration
    .GetSection("jwtTokenConfig").Get<JwtTokenConfig>();

builder.Services.AddSingleton(jwtTokenConfig);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtTokenConfig.Issuer,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
        ValidAudience = jwtTokenConfig.Audience,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(1)
    };
});

builder.Services.AddDbContext<ConservacionDb>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DemoDB")));
// transients
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
builder.Services.AddHostedService<JwtRefreshTokenCache>();

// transients

// Configure Identity
builder.Services.AddIdentity<UserTenant, IdentityRole>(
    options =>
    {
        // Configure identity options here.
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ConservacionDb>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
