//llamar
using MiApi.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System.Text;
using MiApi.Repositories;
using MiApi.Services;
using Microsoft.OpenApi.Models;
using MiApi.Config;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

Env.Load();

var host = Environment.GetEnvironmentVariable("DB_HOST");
var databaseName = Environment.GetEnvironmentVariable("DB_DATABASE_NAME");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var username = Environment.GetEnvironmentVariable("DB_USERNAME");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = $"server={host};port={port};database={databaseName};uid={username};password={password}";

//se usa para construir la aplicacion web
var builder = WebApplication.CreateBuilder(args);

//habilitacion de cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        //policy.WithOrigins("http://localhost:5167")
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()  
              .AllowAnyHeader(); 
    });
});

// vamos a registrar el repositorio y las dependencias-----------------------------
builder.Services.AddScoped<IUserRepositories,UserServices>();
builder.Services.AddScoped<IDoctorRepositories,DoctorServices>();
builder.Services.AddScoped<IAppointmentRepositories,AppointmentServices>();


// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContex>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.20-mysql")));

//usar jwt --------------------------------
builder.Services.AddSingleton<Utilities>();

builder.Services.AddAuthentication(config =>
	{
    		config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    		config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    		config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	}).AddJwtBearer(config =>
		{
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
                ValidateAudience = false, //Si lo tengo en true es por voy habilitar el funcionamiento de programas en especifico por lo que hay que mirar si hay que modificar la variable de JWT_AUDIENCE
                ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!))
            };
		});


//se registra servicio para que la aplicacion responnda a solicitudes http---------------------------------------------
builder.Services.AddControllers();



//Documentacion de la api mediante swagger, expone los endpoins de la api--------------
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c=> 
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API-Users",
        Version = "v1",
        Description = "API para la gestión de información de usuarios",
    });
    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
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
                new string[] {}
            }
        });
});


var app = builder.Build();

// Usar CORS
app.UseCors("AllowAllOrigins");
// app.UseCors("AllowSpecificOrigins");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        //onfigura Swagger UI como la página predeterminada en tu aplicación.
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Mi api v1");
        c.RoutePrefix = string.Empty; 
    });
}

//para redirigir solicituesd http a https ------------------
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



//rutas de los controladores  para que la aplicacion dirijasolicitudes htpp a los controladores
app.MapControllers();

app.Run();

