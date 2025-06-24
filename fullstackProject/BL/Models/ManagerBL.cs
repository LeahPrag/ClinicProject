using BL.API;
using BL.service;
using DAL.service;
using Microsoft.Extensions.DependencyInjection;

public class ManagerBL : IManagerBL
{
    public IClientBL _clientBL { get; set; }
    public IDoctorBL _doctorBL { get; set; }
    public IClinicQueueBL _clinicQueueBL { get; set; }
    private readonly IManagerDAL _managerDAL;
    public ManagerBL()
    {
        ServiceCollection services = new();
        services.AddSingleton<IManagerDAL, ManagerDAL>();
        services.AddSingleton<IDoctorBL, DoctorBL>();
        services.AddSingleton<IClientBL, ClientBL>();
        services.AddSingleton<IClinicQueueBL, ClinicQueueBL>();
        services.AddAutoMapper(typeof(DoctorBL).Assembly);

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        _managerDAL = serviceProvider.GetService<IManagerDAL>();
        _doctorBL = serviceProvider.GetService<IDoctorBL>();
        _clientBL = serviceProvider.GetService<IClientBL>();
        _clinicQueueBL = serviceProvider.GetService<IClinicQueueBL>();
    }
}