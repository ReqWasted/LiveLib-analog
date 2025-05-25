using System.Text;
using LiveLib.Application;
using LiveLib.CacheProvider;
using LiveLib.Database;
using LiveLib.JwtProvider;
using LiveLib.PasswordHasher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var services = builder.Services;
var config = builder.Configuration;

services.AddApplication(config);

services.AddContext(config);

services.AddJwtProvider(config);

services.AddPasswordHasher(config);

services.AddCache(config);

var JwtOptions = config.GetSection("JwtOptions");
services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JwtOptions["Issuer"],
        ValidAudience = JwtOptions["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions["SecretKey"]!)),
        ClockSkew = TimeSpan.Zero
    };

    opt.Events = new()
    {
        OnMessageReceived = context =>
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last() ?? string.Empty;

            //var token = context.Request.Cookies[JwtOptions["CookieName"] ?? "RefreshToken"];

            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }

            return Task.CompletedTask;
        }
    };
});

services.AddOpenApi();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LiveLib API ",
        Version = "v1",
        Description = "API for LiveLib",
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", builder =>
    {
        // localhost:5500 - �����, ������� ��������� � ��������� ������� "Origin".
        // � ������� ������������ cookie same-site=lax, cookie ����� ������������ � �����-�������� �������� ������ ��� GET ��������.
        // ��������, ���� ������ ������� �� localhost:5500 � ������ �� localhost:5001, �� cookie ����� ������������ � ��� POST ��������.
        // ���� ������ ������� �� 127.0.0.1:5500 � ������ �� localhost:5001, �� ��������� ������  CORS, �.�. 127.0.0.1:5500 != localhost:5500,
        // ����� ���������� �������� same-site=none, ����� cookie ����� ������������ �� ����� ����� � ��� ���� cookie ������ ����� ���� secure,
        // ���� ������� same-site=none ��� secure, ������� ������������� ����� cookie.
        builder.WithOrigins("http://localhost:5500")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});



var app = builder.Build();

app.UseRouting();
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        if (exception is Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync($"An unexpected error occurred:{exception.Message}");
        }
    });
});

Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
    app.MapOpenApi();
}

app.Run();
