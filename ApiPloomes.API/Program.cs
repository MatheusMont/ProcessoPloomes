using ApiPloomes.API.Configurations;
using ApiPloomes.DATA.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Injeção de dependência
builder.Services.ResolveDependencies();

//Adição do Contexto do entity framework

builder.Services.AddDbContext<PloomesContext>(options =>
    options
    .UseSqlServer(builder.Configuration.GetConnectionString("DbCOnnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
