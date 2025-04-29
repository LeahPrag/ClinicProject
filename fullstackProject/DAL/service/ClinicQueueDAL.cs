using DAL.API;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    public class ClinicQueueDAL:IClinicQueueDAL
    {
        private readonly DB_Manager _dbManager;
        public ClinicQueueDAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        public List<ClinicQueue> GetList()
        {
            return _dbManager.ClinicQueues.ToList();
        }
        public Boolean DeleteAnApointment(int doctorID, int clientID)
        {
            var queue = _dbManager.ClinicQueues.FirstOrDefault(q => q.ClientId == clientID && q.DoctorId == doctorID);

            if (queue == null)
            {
                return false;
            }
            //AvailableQueue aq = new AvailableQueue();//////func shiffi
            //_dbManager.AvailableQueues.Add(aq);
            //_dbManager.ClinicQueues.Remove(queue);

            return true;
        }

        public List<ClinicQueue> GetDoctorQueesForToday(int doctorId, DateOnly day)
        {

            var clinicQueues = _dbManager.ClinicQueues
                .Where(t => t.DoctorId == doctorId && t.AppointmentDate.Day == day.Day && t.AppointmentDate.Month == day.Month && t.AppointmentDate.Year == day.Year)
                .ToList();

            return clinicQueues;
        }
        public List<int> ClientsNamse(int doctorID)
        {
            return _dbManager.ClinicQueues
                             .Where(c => c.DoctorId == doctorID)
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
