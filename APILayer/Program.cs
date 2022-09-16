
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using RepoLayer;
using BusinessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
        c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Auth0:ApiIdentifier"],
            ValidIssuer = builder.Configuration["Auth0:Domain"]
        };
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("userID:read", p => p.
        RequireAuthenticatedUser()
    );
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("MyAllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
