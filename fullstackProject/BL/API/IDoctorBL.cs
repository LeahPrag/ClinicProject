using BL.Models;

namespace BL.API
{
    public interface IDoctorBL
    {
        Task<int> GetNumOfClientForToday(string idNumber, DateOnly day);
        Task<List<M_Doctor>> GetDoctors();
        Task<List<M_AvailableQueue>> IsDoctorAvailable(string firstName, string lastName, DateOnly day);
        Task<List<M_ClinicQueue>> GetDoctorQueuesForToday(string idNumber, DateOnly day);
        Task<bool> DeleteADayOfWork(string idNumber, DateOnly day);
        Task<List<M_AvailableQueue>> GetDoctorAvailableQueuesForASpesificday(string firstName, string lastName, DateOnly day);
        Task<List<M_AvailableQueue>> GetAvailableQueuesForASpesificday(DateOnly day);
        Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string specialization);
        Task AddDoctor(M_Doctor doctor);
        Task DeleteADoctor(string id);
        Task UpdateDoctor(UpdateDoctorDto updatedDoctor);
        Task<bool> SearchDoctorById(string idNumber);
        Task<M_Doctor> GetDoctorById(string id);
    }
}
