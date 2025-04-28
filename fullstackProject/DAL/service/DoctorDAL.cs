using DAL.API;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    internal class DoctorDAL: IDoctorDAL
    {
        private readonly DB_Manager _dbManager;
        public DoctorDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        public List<Doctor> GetList()
        {
            return _dbManager.Doctors.ToList();
        }

        public List<ClinicQueue> GetDoctorQueesForToday(int doctorId, DateOnly day)
        {

            var clinicQueues = _dbManager.ClinicQueues
                .Where(t => t.DoctorId == doctorId && t.AppointmentDate.Day == day.Day && t.AppointmentDate.Month== day.Month && t.AppointmentDate.Year==day.Year)
                .ToList(); 

            return clinicQueues;
        }
        public List<int> ClientsNamse(int doctorID)
        {
            return _dbManager.ClinicQueues
                             .Where(c => c.DoctorId== doctorID)
                             .Select(c => c.ClientId)
                             .ToList();
        }
        public int SearchADoctor(string doctor_firtsname, string doctor_lastname)
        {

            List<Doctor> doctors = _dbManager.Doctors.ToList();
            Doctor d = doctors.FirstOrDefault(x => x.FirstName.Equals(doctor_firtsname) && x.FirstName.Equals(doctor_lastname));
            if (d == null)
            {
                return d.DoctorId;
            }
            return -1;

        }
    }
}
