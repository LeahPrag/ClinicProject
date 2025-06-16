using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.service
{
    public class AvailableQueueDAL : IAvailableQueueDAL
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
            ClinicQueue newQueue = new ClinicQueue(c, db.Clients.FirstOrDefault(c => c.ClientId == clientId));
            if (c == null)
                return false;
            return true;
        }
        //fgufu
        public Boolean MakeAnAppointment(int doctorId, DateTime dateTime, Client client)
        {
            var c = db.AvailableQueues.FirstOrDefault(t => t.DoctorId == doctorId && t.AppointmentDate == dateTime);

            if (c != null)
            {
                db.AvailableQueues.Remove(c);
                ClinicQueue s = new ClinicQueue();
                s.DoctorId = c.DoctorId;
                s.AppointmentDate = c.AppointmentDate;
                s.ClientId = client.ClientId;
                s.Client = client;
                s.QueueId = c.QueueId;
                db.ClinicQueues.Add(s);
                return true;
            }

            return false;
        }
        public Boolean MakeAnAppointment(DateTime dateTime)
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

        public async Task<List<AvailableQueue>> GetDoctorAvailableQueueForASpesificDay(int doctorId, DateOnly day)
        {
            return await db.AvailableQueues.Where(t => t.DoctorId == doctorId &&
                                                     t.AppointmentDate.Date == day.ToDateTime(TimeOnly.MinValue).Date)
                                                     .ToListAsync();

        }
        public async Task<List<AvailableQueue>> GetAvailableQueueForASpesificDay( DateOnly day)
        {
            return await db.AvailableQueues.Where(t => t.AppointmentDate.Date == day.ToDateTime(TimeOnly.MinValue).Date)
                                                     .ToListAsync();
        }
        public async Task<List<AvailableQueue>> AvailableQueuesForASpezesilation(string specialization)
        {
            return await db.AvailableQueues
                        .Include(q => q.Doctor) // 💥 חובה: טעינת רופא יחד עם התור
                        .Where(q => q.Doctor.Specialization == specialization)
                        .ToListAsync();
        }
        public async Task<AvailableQueue> GetDoctorAvailableQueueForASpecificHour(int doctorId, DateTime appointmentDate)
        {
            return await db.AvailableQueues
                .FirstOrDefaultAsync(t => t.DoctorId == doctorId && t.AppointmentDate == appointmentDate);
        }
        public async Task<bool> AddAvailableQueue(AvailableQueue availableQueue)
        {
            db.Add(availableQueue);
            await db.SaveChangesAsync();
            return true;
        }


    }

}

