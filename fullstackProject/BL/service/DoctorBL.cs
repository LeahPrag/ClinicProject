
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.API;





namespace BL.service
{
    internal class DoctorBL
    {
        private readonly IDoctorDAL _doctorDal;

        public DoctorBL(IDoctorDAL doctorDal)
        {
            _doctorDal=doctorDal;
        }

        public int GetNumOfClientForToday(string firstName, string lastName, DateOnly day)
        {
            int doctorId = _doctorDal.SearchADoctor(firstName, lastName);
            return _doctorDal.GetDoctorQueesForToday(doctorId,day).Count;
        }

        // להחזיר רשימה של מאמנים כפי שרוצים לראות אותם במסך
        //public List<ModelTrainerBL> GetList()
        //{
        //    var previous = _trainerDal.GetList();
        //    List<ModelTrainerBL> updated = new();
        //    previous.ForEach(t => updated.Add
        //        (new ModelTrainerBL()
        //        {
        //            FirstName = t.FirstName,
        //            LastName = t.LastName,
        //            Level = t.Level,
        //            NumOfStudioClasses = t.StudioClasses.Count
        //        }));
        //    return updated;
        //    // נלך לדל
        //    // נביא נתוני מאמנים
        //    // נערוך אותם למבנה הרצוי
        //    //ונחזיר

        //}

    }
}
