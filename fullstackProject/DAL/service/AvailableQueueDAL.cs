using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    public class AvailableQueueDAL
    {
        public DB_Manager db;

        public AvailableQueueDAL(DB_Manager db)
        {
            this.db = db;
        }


        public Boolean MakeAnAppointment(int doctorId, int clientId, DateTime dateTime)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.FirstOrDefault(t => t.DoctorId == doctorId &&
                                                           t.AppointmentDate == dateTime);
            ClinicQueue newQueue = new ClinicQueue(c, db.Clients.FirstOrDefault(c=> c.ClientId== clientId));
            if (c == null)
                return false;
            return true;
        }
        public Boolean MakeAnAppointment(int doctorId, DateTime dateTime,Client client)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.FirstOrDefault(t => t.DoctorId == doctorId && t.AppointmentDate == dateTime);


            if (c != null)
            {
                // Remove it from AvailableQueues
                db.AvailableQueues.Remove(c);


                // Add it to ClinicQueues
                ClinicQueue s = new ClinicQueue();
                s.DoctorId =c.DoctorId;
                s.AppointmentDate = c.AppointmentDate;
                s.ClientId= client.ClientId;
                s.Client = client;
                s.QueueId = c.QueueId;
                

                


                //db.ClinicQueues.Add(c);

                return true;
            }

            return false;
        }
        public Boolean MakeAnAppointment( DateTime dateTime)
        {
            // Check if the appointment exists
            var c = db.AvailableQueues.All(t => t.AppointmentDate == dateTime);

            if (c != null)
            {
                // Remove it from AvailableQueues
                //db.AvailableQueues.Remove(c);

                //// Add it to ClinicQueues
                //db.ClinicQueues.Add(c);

                return true;
            }

            return false;
        }

    }
}
