using DAL.API;
using BL.API;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.service
{
    public class ClinicQueueBL: IClinicQueueBL
    {

        private readonly IManagerDAL _managerBL;


        public ClinicQueueBL(IManagerDAL managerBL)
        {

            _managerBL = managerBL;
        }

        public Boolean DeleteAnApointment(string doctorFirstname, string doctorLastname, string idNumber, DateOnly date)
        {
            int doctorID = _managerBL.GetDoctorDAL().SearchADoctor(doctorFirstname, doctorLastname);
            //int clientID = _managerBL.GetClientDAL().SearchAClient(idNumber);
            return _managerBL.GetClinicQueueDAL().DeleteAnApointment(doctorID,11 );// clientID
        }


    }
}
