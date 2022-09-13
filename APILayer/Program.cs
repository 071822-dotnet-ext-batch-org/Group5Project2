
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using RepoLayer;
using BusinessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<StoreFrontRepoLayer>();
builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<IBus, Bus>();

builder.Services.AddCors(options => 
{
    options.AddPolicy( name: "MyAllowAllOrigins",
    builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var config = builder.Configuration["ConnectionStrings:project2ApiDB"];

Console.WriteLine("This is a connection test: " + builder.Configuration["ConnectionStrings:project2ApiDB"]);


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
