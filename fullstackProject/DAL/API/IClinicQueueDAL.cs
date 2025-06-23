using DAL.Models;


namespace DAL.API
{
    public interface IClinicQueueDAL
    {
        Task<List<int>> ClientsNames(int doctorID);
        //Task<List<ClinicQueue>> GetDoctorQueuesForToday(int doctorId, DateOnly day);
        Task<bool> DeleteAnApointment(ClinicQueue queue);
        Task<bool> DeleteAnApointment(string doctorID, string clientID, DateTime date);
        Task<List<ClinicQueue>> GetList();
    }
}