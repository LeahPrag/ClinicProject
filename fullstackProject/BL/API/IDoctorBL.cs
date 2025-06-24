using BL.Models;
using DAL.Models;

namespace BL.API
{
    public interface IDoctorBL
    {
        Task<int> GetNumOfClientForToday(string firstName, string lastName, DateOnly day);
        Task<List<M_Doctor>> GetDoctors();
        Task<List<M_AvailableQueue>> IsDoctorAvailable(string firstName, string lastName, DateOnly day);
        Task<List<M_ClinicQueue>> GetDoctorQueuesForToday(string idNumber, DateOnly day);
        Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day);
        Task<List<M_AvailableQueue>> GetDoctorAvailableQueuesForASpesificday(string firstName, string lastName, DateOnly day);
        Task<List<M_AvailableQueue>> GetAvailableQueuesForASpesificday(DateOnly day);
        Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string specialization);
        Task AddDoctor(Doctor doctor);
        Task DeleteADoctor(string id);
        Task UpdateDoctor(UpdateDoctorDto updatedDoctor);
        Task<bool> SearchDoctorById(string idNumber);

    }
}
