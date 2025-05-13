using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    internal class ApointmentManager
    {
        public DB_Manager db;

        public ApointmentManager(DB_Manager db)
        {
            this.db = db;
        }
        public ClinicQueue CheckFirstApoitmentAvailable(string specialization)
        {
            return db.ClinicQueues
                     .Include(q => q.Doctor) 
                     .Where(q => q.Doctor.Specialization == specialization)
                     .OrderBy(q => q.AppointmentDate) 
                     .FirstOrDefault(); 

        }




        public int SearchAClient(string client_firstname, string client_lastname)
        {

            List<Client> clients = db.Clients.ToList();
            Client c = clients.FirstOrDefault(x => x.FirstName.Equals(client_firstname) && x.LastName.Equals(client_lastname));
            if (c == null)
            {
                return c.ClientId;
            }
            return -1;////////////////

        }
        public int SearchADoctor(string doctor_firtsname, string doctor_lastname)
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
        public int DaysLeft(int clientID, int doctorID)
        {
            var appointment = db.ClinicQueues
                .Include(q => q.Doctor)
                .Include(q => q.Client)
                .Where(q => q.Doctor.DoctorId == doctorID && q.Client.ClientId == clientID)
                .OrderBy(q => q.AppointmentDate)
                .FirstOrDefault();
            if (appointment == null)
            {
                return -1;
            }
            return (appointment.AppointmentDate - DateTime.Now).Days;
        }
    }
}
