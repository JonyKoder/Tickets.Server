using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Tickets.Server.Application;
using Tickets.Server.BL.Repositoryes;
using Tickets.Server.BL.Services;
using Tickets.Server.Contracts.Interfaces;
using Tickets.Server.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



builder.Services.AddAutoMapper(typeof(MappingProfile)); // Регистрация AutoMapper
// Добавляем репозиторий в контейнер зависимостей
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<IMapper, Mapper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
