using BL.API;
using DAL.API;
using DAL.Models;
using DAL.service;
using Microsoft.Extensions.DependencyInjection;

public class ManagerDAL : IManagerDAL
{
    public IClinicQueueDAL _clinicQueueDAL { get; set; }
    public IDoctorDAL _doctorDAL { get; set; }
    public IClientDAL _clientDAL { get; set; }
    public DB_Manager _dbManager { get; init; }

    public ManagerDAL()
    {
        ServiceCollection services = new();
        services.AddSingleton<IDoctorDAL, DoctorDAL>();
        services.AddSingleton<IClientDAL, ClientDAL>();
        services.AddSingleton<IClinicQueueDAL, ClinicQueueDAL>();
        services.AddSingleton<DB_Manager>();

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _doctorDAL = serviceProvider.GetService<IDoctorDAL>();
        _clientDAL = serviceProvider.GetService<IClientDAL>();
        _clinicQueueDAL = serviceProvider.GetService<IClinicQueueDAL>();
        _dbManager = serviceProvider.GetService<DB_Manager>();
    }
}