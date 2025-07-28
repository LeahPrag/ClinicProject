using BL.Models;
using DAL.Models;

namespace BL.API
{
    public interface IClinicQueueBL
    {
        Task DeleteAnApointment(string udDoctor, string idClient, DateOnly date);
        Task GenerateFutureAvailableQueues();
        Task MakeAnAppointment(string idDoctor, string idClient, DateOnly date, int hour);
        Task<List<M_ClinicQueue>> GetClientQueues(string clientId);
        Task<List<int>> ClientsNames(int doctorID);
    }
}
