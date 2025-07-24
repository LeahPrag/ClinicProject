using DAL.Models;
namespace DAL.API
{
    public interface IDoctorDAL
    {
        public Task<int> SearchADoctor(string doctor_firtsname, string doctor_lastname);
        public Task<List<ClinicQueue>> GetDoctorQueuesForASpesificDay(string doctorId, DateOnly day);
        Task<List<Doctor>> GetDoctors();
        Task<List<Doctor>> GetDoctorsWithDays();
        Task AddADoctor(Doctor doctor);
        Task<bool> SearchADoctorById(string id);
        Task DeleteADoctor(Doctor doctor);
        Task<Doctor> GetADoctorById(string id);
        Task UpdateDoctor(Doctor doctor);
        Task<int> GetDoctorIdByIdNumber(string id);
    }
}