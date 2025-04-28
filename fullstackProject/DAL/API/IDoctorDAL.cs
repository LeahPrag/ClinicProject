using DAL.Models;

namespace DAL.API
{
    public interface IDoctorDAL
    {
        List<ClinicQueue> GetDoctorQueesForToday(int doctorId, DateOnly day);
        int SearchADoctor(string doctor_firtsname, string doctor_lastname);
    }
}