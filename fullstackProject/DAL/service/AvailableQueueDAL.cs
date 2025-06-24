using DAL.API;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.service
{
	public class AvailableQueueDAL : IAvailableQueueDAL
	{
		public DB_Manager db;
		public AvailableQueueDAL(DB_Manager db)
		{
			this.db = db;
		}
		public async Task<bool> MakeAnAppointment(int doctorId, Client client, DateTime dateTime)
		{
			var queue = await db.AvailableQueues
			 .FirstOrDefaultAsync(t =>
					 t.DoctorId == doctorId &&
					 t.AppointmentDate.Year == dateTime.Year &&
					 t.AppointmentDate.Month == dateTime.Month &&
					 t.AppointmentDate.Day == dateTime.Day &&
					 t.AppointmentDate.Hour == dateTime.Hour &&
					 t.AppointmentDate.Minute == dateTime.Minute
				  );
			if (queue == null)
			{
				return false;
			}
			db.AvailableQueues.Remove(queue);
			ClinicQueue s = new ClinicQueue();
			s.DoctorId = queue.DoctorId;
			s.AppointmentDate = queue.AppointmentDate;
			s.ClientId = client.ClientId;
			s.Client = client;
			db.ClinicQueues.Add(s);
			await db.SaveChangesAsync();
			return true;
		}
		public async Task<List<AvailableQueue>> GetDoctorAvailableQueueForASpesificDay(int doctorId, DateOnly day)
		{
			return await db.AvailableQueues.Where(t => t.DoctorId == doctorId &&
													 t.AppointmentDate.Date == day.ToDateTime(TimeOnly.MinValue).Date)
													 .ToListAsync();

		}
		public async Task<List<AvailableQueue>> GetAvailableQueueForASpesificDay(DateOnly day)
		{
			return await db.AvailableQueues.Include(q => q.Doctor).Where(t => t.AppointmentDate.Date == day.ToDateTime(TimeOnly.MinValue).Date)
													 .ToListAsync();
		}
		public async Task<List<AvailableQueue>> AvailableQueuesForASpezesilation(string specialization)
		{
			return await db.AvailableQueues
						.Include(q => q.Doctor)
						.Where(q => q.Doctor.Specialization == specialization)
						.ToListAsync();
		}
		public async Task<AvailableQueue> GetDoctorAvailableQueueForASpecificHour(int doctorId, DateTime AppointmentDate)
		{
			return await db.AvailableQueues
				.FirstOrDefaultAsync(t => t.DoctorId == doctorId && t.AppointmentDate == AppointmentDate);
		}
		public async Task<bool> AddAvailableQueue(AvailableQueue availableQueue)
		{
			db.Add(availableQueue);
			await db.SaveChangesAsync();
			return true;
		}
	}

}

