using DAL.API;
using BL.API;
using BL.Exceptions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Models;

namespace BL.service
{
    public class ClinicQueueBL : IClinicQueueBL
    {

        private readonly IManagerDAL _managerDal;


        public ClinicQueueBL(IManagerDAL managerBL)
        {

            _managerDal = managerBL;
        }

        public Boolean DeleteAnApointment(string doctorFirstname, string doctorLastname, string idNumber, DateOnly date)
        {
            int doctorID = _managerDal.GetDoctorDAL().SearchADoctor(doctorFirstname, doctorLastname);
            //int clientID = _managerBL.GetClientDAL().SearchAClient(idNumber);
            return _managerDal.GetClinicQueueDAL().DeleteAnApointment(doctorID, 11);// clientID
        }
        //קביעת תור
        //public Boolean MakeAnAppointment(string doctorFirstname, string doctorLastname, DateOnly date, string idClient)
        //{
        //    var doctorID = _managerDal.GetDoctorDAL().SearchADoctor(doctorFirstname, doctorLastname);
        //    if(doctorID == null) 
        //        throw new DoctorNotExsistException(doctorFirstname, doctorLastname);
        //    var clientID = _managerDal.GetClientDAL().GetClientById(idClient);
        //    if (clientID == null)
        //        throw new ClientNotExsistException(idClient);
        //    _managerDal.GetClinicQueueDAL().

        //}
        //עדכון תור


    }
}
