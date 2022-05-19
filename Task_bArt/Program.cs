using Microsoft.EntityFrameworkCore;
using Task_bArt.Data.Context;
using Task_bArt.Service.Services.Abstracts;
using Task_bArt.Service.Services.Implements;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
