using System;
using BL.API;
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
        [HttpGet("/id")]
        public async Task<List<Doctor>> GetDoctors()
        {

            return await managerBL._doctorBL.GetDoctors();

        }

    }
}
