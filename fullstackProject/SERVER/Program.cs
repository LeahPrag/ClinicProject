using BL.API;
using BL.service;
using DAL.service;
using DAL.API;
using DAL.Models;
using AutoMapper;
using SERVER.Middleware;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddSingleton<IManagerBL, ManagerBL>();
//builder.Services.AddSingleton<DB_Manager>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Mapper).Assembly));

//add it for mapper profil 
builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Mapper).Assembly));
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
