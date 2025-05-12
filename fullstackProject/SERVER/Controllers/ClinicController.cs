using BL.API;
using Microsoft.AspNetCore.Mvc;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase // Use ControllerBase for API controllers
    {
        private readonly IManagerBL _managerBL;

        // Constructor for dependency injection
        public ClinicController(IManagerBL managerBL)
        {
            _managerBL = managerBL;
        }

        // Example endpoint
        //[HttpGet]
        //public IActionResult Get()
        //{

        //}
    }
}