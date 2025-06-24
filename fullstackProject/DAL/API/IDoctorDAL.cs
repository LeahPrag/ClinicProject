using DAL.Models;
namespace DAL.API
{
    public interface IDoctorDAL
    {
        public Task<int> SearchADoctor(string doctor_firtsname, string doctor_lastname);
        public Task<List<int>> ClientsNamse(int doctorID);
        public Task<List<ClinicQueue>> GetDoctorQueesForASpesificDay(int doctorId, DateOnly day);
        public Task<List<Doctor>> GetList();
        public Task<Day?> GetDoctorDay(string doctor_firtsname, string doctor_lastname, int day);
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