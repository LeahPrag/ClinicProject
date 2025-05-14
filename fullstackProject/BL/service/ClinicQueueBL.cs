using DAL.API;
using BL.API;
using BL.Exceptions;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace BL.service
{
    public class ClinicQueueBL : IClinicQueueBL
    {

        private readonly IManagerDAL _managerDal;


        public ClinicQueueBL(IManagerDAL managerDAL)
        {

            _managerDal = managerDAL;
        }


        public async Task<Boolean> DeleteAnApointment(string doctorFirstname, string doctorLastname, string idClient, DateOnly date)
        {
            int doctorID = await _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname);

            int clientID = _managerDal._clientDAL.GetClientById(idClient).ClientId;
            return _managerDal._clinicQueueDAL.DeleteAnApointment(doctorID, clientID);// clientID
        }
        //קביעת תור
        //public async Task<Boolean> MakeAnAppointment(string doctorFirstname, string doctorLastname, DateOnly date, string idClient)
        //{
        //    int doctorID = await _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname);
        //    if (doctorID == null)
        //    {
        //        throw
        //    }
        //    int clientID = _managerDal._clientDAL.GetClientById(idClient).ClientId;
        //    if

        //    return _managerDal._clinicQueueDAL.DeleteAnApointment(doctorID, clientID);// clientID
        //}

        //קביעת תור
    
        //עדכון תור


    }
}
