using System.Globalization;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using PessoasCrudApi.Data;

var builder = WebApplication.CreateBuilder(args);

//loading .env variables
Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

// Registering Postgres using DbContext
builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Adding needed services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuring Papeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();
app.Run();
