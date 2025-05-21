using LiveLib.Application;
using LiveLib.Database;
using LiveLib.JwtProvider;
using LiveLib.PasswordHasher;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var services = builder.Services;
var config = builder.Configuration;

services.AddApplication(config);

services.AddContext(config);

services.AddJwtProvider(config);

services.AddPasswordHasher(config);

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

services.AddControllers();

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


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

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
