using DAL.API;
using DAL.service;

namespace BL.API
{
    public interface IManagerDAL
    {
        IDoctorDAL GetDoctorDAL();
        IClinicQueueDAL GetClinicQueueDAL();
        IClientDAL GetClientDAL();
    }
}