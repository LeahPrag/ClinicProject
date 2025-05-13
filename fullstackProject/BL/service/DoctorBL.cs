
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.API;
using DAL.API;





namespace BL.service
{
    public class DoctorBL: IDoctorBL
    {
        private readonly IDoctorDAL _doctorDal;

        public DoctorBL(IManagerDAL  doctorDal)
        {
            _doctorDal=doctorDal._doctorDAL;
        }

        public int GetNumOfClientForToday(string firstName, string lastName, DateOnly day)
        {
            int doctorId = _doctorDal.SearchADoctor(firstName, lastName);
            return _doctorDal.GetDoctorQueesForToday(doctorId,day).Count;
        }



    }
}
