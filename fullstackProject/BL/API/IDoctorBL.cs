using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IDoctorBL
    {
        int GetNumOfClientForToday(string firstName, string lastName, DateOnly day);

    }
}
