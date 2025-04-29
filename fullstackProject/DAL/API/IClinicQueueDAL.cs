using DAL.Models;

namespace DAL.API
{
    public interface IClinicQueueDAL
    {
        List<ClinicQueue> GetList();
        Boolean DeleteAnApointment(int doctorID, int clientID);
    }
}