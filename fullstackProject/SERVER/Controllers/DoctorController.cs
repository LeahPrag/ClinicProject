using BL.API;
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
        public int GetNumOfClientForToday(string firstName, string lastName)
        {

            int num =  managerBL._doctorBL.GetNumOfClientForToday(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));


            return num;
            //return 5;
        }

    }
}
