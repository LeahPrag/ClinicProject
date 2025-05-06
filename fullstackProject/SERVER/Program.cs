using BL.API;
using BL.service;
using BL.Models;
using DAL.service;
using DAL.API;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//////

builder.Services.AddControllers();
builder.Services.AddSingleton<IDoctorBL, DoctorBL>();
builder.Services.AddSingleton<IClinicQueueBL, ClinicQueueBL>();
//builder.Services.AddSingleton<IClientBL, ClientBL>();
builder.Services.AddControllers();
builder.Services.AddSingleton<IDoctorDAL, DoctorDAL>();
builder.Services.AddSingleton<IClinicQueueDAL, ClinicQueueDAL>();
//builder.Services.AddSingleton<IClientDaL, ClientDAL>();
builder.Services.AddSingleton<IManagerBL, ManagerBL>();
builder.Services.AddSingleton<IManagerDAL, ManagerDAL>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
