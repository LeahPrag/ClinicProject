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
    }
}