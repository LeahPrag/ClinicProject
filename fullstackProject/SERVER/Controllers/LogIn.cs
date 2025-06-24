using BL.API;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogIn : ControllerBase
    {
        private readonly IManagerBL _managerBL;

        // Constructor for dependency injection
        public LogIn(IManagerBL managerBL)
        {
            _managerBL = managerBL;
        }

        [HttpGet("GetUserType")]
        public async Task<IActionResult> GetUserType(string id)
        {
            if (id == "1111")
                return Ok("Secretary");

            if (await _managerBL._doctorBL.SearchDoctorById(id))
                return Ok("Doctor");

            var client = await _managerBL._clientBL.GetClientById(id);
            if (client != null)
                return Ok("client");

            return NotFound("User not found");
        }
    }
}
