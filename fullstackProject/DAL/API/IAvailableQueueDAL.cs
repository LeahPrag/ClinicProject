using System;
using DAL.Models;
namespace DAL.API
{
    public interface IAvailableQueueDAL
    {
        Task<List<AvailableQueue>> GetDoctorAvailableQueueForASpesificDay(int doctorId, DateOnly day);
        Task<bool> MakeAnAppointment(int doctorId, Client client, DateTime dateTime);
        Task<List<AvailableQueue>> GetAvailableQueueForASpesificDay(DateOnly day);
        Task<List<AvailableQueue>> AvailableQueuesForASpezesilation(string specialization);
        Task<AvailableQueue> GetDoctorAvailableQueueForASpecificHour(int doctorId, DateTime AppointmentDate);
        Task<bool> AddAvailableQueue(AvailableQueue availableQueue);
    }
}