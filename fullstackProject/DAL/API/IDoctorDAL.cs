using DAL.Models;

namespace DAL.API
{
    public interface IDoctorDAL
    {
        public Task<int> SearchADoctor(string doctor_firtsname, string doctor_lastname);
        public Task<List<int>> ClientsNamse(int doctorID);
        public Task<int> GetDoctorQueesForToday(int doctorId, DateOnly day);
        public Task<List<Doctor>> GetList();
        public Task<Day?> GetDoctorDay(string doctor_firtsname, string doctor_lastname, int day);
    }
}