using DAL.API;
using DAL.service;

namespace BL.API
{
    public interface IManagerBL
    {
        IDoctorDAL GetDoctorDAL();
        IClinicQueueDAL GetClinicQueueDAL();
        IClientDAL GetClientDAL();
    }
}