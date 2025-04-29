using DAL.API;

namespace BL.API
{
    public interface IManagerBL
    {
        IDoctorDAL GetDoctorDAL();
        IClinicQueueDAL GetClinicQueueDAL();
    }
}