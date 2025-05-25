using DAL.Models;


namespace DAL.API
{
    public interface IClinicQueueDAL
    {
        Task<List<int>> ClientsNames(int doctorID);
        //Task<List<ClinicQueue>> GetDoctorQueuesForToday(int doctorId, DateOnly day);
        Task<bool> DeleteAnApointment(int doctorID, int clientID);
        Task<List<ClinicQueue>> GetList();
    }
}