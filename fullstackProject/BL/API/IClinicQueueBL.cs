﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.API
{
    public interface IClinicQueueBL
    {
        Task<bool> DeleteAnApointment(string doctorFirstname, string doctorLastname, string idClient, DateOnly date);
        Task Appointment();
    }
}
