using BL.API;
using AutoMapper;
using SERVER.Middleware;
using DAL.Models;
using BL.service;
using DAL.API;
using DAL.service;
using BL.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// DAL
builder.Services.AddScoped<IClientDAL, ClientDAL>();
builder.Services.AddScoped<IDoctorDAL, DoctorDAL>();
builder.Services.AddScoped<IAvailableQueueDAL, AvailableQueueDAL>();
builder.Services.AddScoped<IManagerDAL, ManagerDAL>();
builder.Services.AddScoped<DB_Manager>();

// BL
builder.Services.AddScoped<IClientBL, ClientBL>();
builder.Services.AddScoped<IDoctorBL, DoctorBL>();
builder.Services.AddScoped<IManagerBL, ManagerBL>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(ClinicQueueMappingProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowReactApp");
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();




