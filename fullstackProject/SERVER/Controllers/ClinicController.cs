using BL.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {

        IManagerDAL managerDAL;
        IManagerBL managerBL;
        public ClinicController(IManagerDAL _managerDAL, IManagerBL _managerBL )
        {
            managerBL = _managerBL;
            managerDAL = _managerDAL;
        }
        // GET: api/<gradeManagerController>
        [HttpGet]
        public int GetNumOfClientForToday(string firstName, string lastName)
        {

            int num = managerBL.GetDoctorBL().GetNumOfClientForToday(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));


            return num;
        }

    }
}
