using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.API
{
    public interface IDoctorBL
    {
        Task<int> GetNumOfClientForToday(string firstName, string lastName, DateOnly day);
        Task DeleteADayOfWork(string firstName, string lastName, DateOnly day);
        Task<List<Doctor>> GetDoctors();

    }
}
