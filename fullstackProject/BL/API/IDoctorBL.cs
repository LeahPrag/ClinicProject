using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;
using DAL.Models;

namespace BL.API
{
    public interface IDoctorBL
    {
        Task<int> GetNumOfClientForToday(string firstName, string lastName, DateOnly day);
        //Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day);
        Task<List<M_Doctor>> GetDoctors();
        Task<List<M_AvailableQueue>> IsDoctorAvailable(string firstName, string lastName, DateOnly day);
        Task<List<M_ClinicQueue>> GetDoctorQueesForToday(string firstName, string lastName, DateOnly day);
        Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day);
        Task<List<M_AvailableQueue>> GetDoctorAvailableQueesForASpesificday(string firstName, string lastName, DateOnly day);
        Task<List<M_AvailableQueue>> GetAvailableQueesForASpesificday(DateOnly day);
        Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string specialization);
        Task AddDoctor(Doctor doctor);
        Task DeleteADoctor(string id);
        Task UpdateDoctor(UpdateDoctorDto updatedDoctor);

    }
}
