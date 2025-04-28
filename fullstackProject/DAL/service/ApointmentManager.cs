using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    internal class ApointmentManager
    {
        private DB_Manager db;

        public int SearchAClient( string client_firstname, string client_lastname)
        {

            List<Client> clients = db.Clients.ToList();
            Client c = clients.FirstOrDefault(x => x.FirstName.Equals(client_firstname)&& x.LastName.Equals(client_lastname));
            if (c == null)
            {
                return c.ClientId;
            }
            return -1;////////////////

        }
        public ClinicQueue SearchAnAppointment(DateTime AppointmentDate, string id)
        {

            List<ClinicQueue> clinicQueues = db.ClinicQueues.ToList();
            ClinicQueue c = clinicQueues.FirstOrDefault(x => x.AppointmentDate .Equals(AppointmentDate) && x.IsAvailable.Equals(true));
            if (c == null)
            {
                return c;
            }
            return null;////////////////

        }
        public int SearchADoctor(string doctor_firtsname, string doctor_lastname)///צריכים לקבל רק קליינט ID
        {

            List<Doctor> doctors = db.Doctors.ToList();
            Doctor d = doctors.FirstOrDefault(x => x.FirstName.Equals(doctor_firtsname) && x.FirstName.Equals(doctor_lastname));
            if (d == null)
            {
                return d.DoctorId;
            }
            return -1;////////////////

        }
        public Boolean DeleteAnApointment(string doctor_firtsname, string doctor_lastname, string client_firstname, string client_lastname)
        {

            if (SearchAClient(client_firstname, client_lastname) != -1 && SearchADoctor(doctor_firtsname, doctor_lastname) != -1)
            {
                return true;
            }
            return false;
        }

        public Boolean MakeAnApointment(string doctor_firtsname, string doctor_lastname, string client_firstname, string client_lastname)
        {

            if (SearchAClient(client_firstname, client_lastname) != -1 && SearchADoctor(doctor_firtsname, doctor_lastname) != -1)
            {

                return true;
            }
            return false;
        }
        public List<ClinicQueue> CheckAvailableAppointments(DateTime day)
        {
            List<ClinicQueue> availableAppointments = db.ClinicQueues
                .Where(q => q.AppointmentDate == day && q.IsAvailable)
                .ToList();

            return availableAppointments;
        }
        
        

    }
}
