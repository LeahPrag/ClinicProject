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


        //public async Task<bool> DeleteAnApointment(string doctorFirstname, string doctorLastname, string IdNumber, DateOnly date)
        //{
        //    int doctorID =await _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname);
        //    //int clientID = _managerBL.GetClientDAL().SearchAClient(IdNumber);
        //    return await _managerDal._clinicQueueDAL.DeleteAnApointment(doctorID, 11);// clientID
        //}
        //קביעת תור
        //public Boolean MakeAnAppointment(string doctorFirstname, string doctorLastname, DateOnly date, string idClient)
        //{
        //    //var doctorID = _managerDal.GetDoctorDAL().SearchADoctor(doctorFirstname, doctorLastname);
        //    //if (doctorID == null)
        //    //    throw new DoctorNotExsistException(doctorFirstname, doctorLastname);
        //    //var clientID = _managerDal.GetClientDAL().GetClientById(idClient);
        //    //if (clientID == null)
        //    //    throw new ClientNotExsistException(idClient);
        //    //_managerDal.GetClinicQueueDAL().
        //    if()

        //    var doctorID = _managerDal._doctorDAL.SearchADoctor()

        //}


        public Boolean DeleteAnApointment(string doctorFirstname, string doctorLastname, string idClient, DateOnly date)
        {
            int doctorID =  _managerDal._doctorDAL.SearchADoctor(doctorFirstname, doctorLastname).Result;

            int clientID = _managerDal._clientDAL.GetClientById(idClient).ClientId;
            return _managerDal._clinicQueueDAL.DeleteAnApointment(doctorID, clientID).Result;// clientID
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
