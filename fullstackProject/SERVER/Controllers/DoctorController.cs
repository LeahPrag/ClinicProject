using System;
using BL.API;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        IManagerBL managerBL;
        public DoctorController( IManagerBL _managerBL )
        {
            managerBL = _managerBL;
               }
        // GET: api/<gradeManagerController>
        [HttpGet]
        public async Task<int> GetNumOfClientForToday(string firstName, string lastName)
        {

            return  await managerBL._doctorBL.GetNumOfClientForToday(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));



            //return 5;
        }
        [HttpGet("/getAllDoctors")]
        public async Task<List<M_Doctor>> GetDoctors()
        {

            return await managerBL._doctorBL.GetDoctors();

        }
        //
        [HttpGet("/DoctorqeuesForToday")]
        public async Task<List<M_ClinicQueue>> DoctorQeuesForToday(string firstName, string lastName)
        {

            return await managerBL._doctorBL.GetDoctorQueesForToday( firstName,  lastName, DateOnly.FromDateTime(DateTime.Now));

        }
        //[HttpGet("/availableQueuesForASpezesilation")]
        //public async Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string spezesilation)
        //{

        //    return await managerBL._doctorBL.AvailableQueuesForASpezesilation( DateOnly.FromDateTime(DateTime.Now));

        //}

        [HttpGet("/AvailableQueuesForToday")]
        public async Task<List<M_AvailableQueue>> AvailableQueuesForToday()
        {

            return await managerBL._doctorBL.GetAvailableQueesForASpesificday(DateOnly.FromDateTime(DateTime.Now));

        }


        [HttpPost("/deleteADayOfWork")]
        public async Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day)
        {

            return await managerBL._doctorBL.DeleteADayOfWork( firstName,  lastName,  day);

        }

    }
}
