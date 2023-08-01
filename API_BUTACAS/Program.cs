using API_BUTACAS;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Inyectamos el contexto nuestro (clase que hereda de DbContext) con el metodo AddDbContext() y le pasamos por parametro la ConnectionString definida en el archivo appsettings.json


builder.Services.AddDbContext<EstudioDeDanzasContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

//Dependency Injection
CompositeRoot.DependencyInjection(builder);

//Configuracion de JWT
builder.Configuration.AddJsonFile("appsettings.json");
var secretkey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretkey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
////
//CORS
var myRulesCors = "RulesCors";
builder.Services.AddCors(opt => {
    opt.AddPolicy(name: myRulesCors, builder =>
    {
        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); ;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(myRulesCors);

app.UseHttpsRedirection();
//AGREGAR ESTO PARA EL JWT TMB
app.UseAuthentication();
//

app.UseAuthorization();

app.MapControllers();

app.Run();
