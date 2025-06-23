using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.API
{
    public interface IAvailableQueueDAL
    {
        Boolean MakeAnAppointment(string doctorId, string clientId, DateTime dateTime);
        Task<List<AvailableQueue>> GetDoctorAvailableQueueForASpesificDay(int doctorId, DateOnly day);
        Boolean MakeAnAppointment(DateTime dateTime);
        Task<bool> MakeAnAppointment(int doctorId, Client client, DateTime dateTime);
        Task<List<AvailableQueue>> GetAvailableQueueForASpesificDay(DateOnly day);
        Task<List<AvailableQueue>> AvailableQueuesForASpezesilation(string specialization);
        Task<AvailableQueue> GetDoctorAvailableQueueForASpecificHour(int doctorId, DateTime appointmentDate);
        Task<bool> AddAvailableQueue(AvailableQueue availableQueue);
    }
}
