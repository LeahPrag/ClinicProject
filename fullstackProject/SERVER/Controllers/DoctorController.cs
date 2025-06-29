using System;
using BL.API;
using BL.Exceptions;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IManagerBL managerBL;

        public DoctorController(IManagerBL _managerBL)
        {
            managerBL = _managerBL;
        }

        [HttpGet("numOfClientsForToday")]
        public async Task<IActionResult> GetNumOfClientForToday([FromQuery] string firstName, [FromQuery] string lastName)
        {
            int count = await managerBL._doctorBL.GetNumOfClientForToday(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));
            return Ok(count);
        }

        [HttpGet("getAllDoctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var doctors = await managerBL._doctorBL.GetDoctors();
            return Ok(doctors);
        }

        [HttpGet("DoctorQueuesForToday")]
        public async Task<IActionResult> DoctorQueuesForToday([FromQuery] string idNumber)
        {
            var queues = await managerBL._doctorBL.GetDoctorQueuesForToday(idNumber, DateOnly.FromDateTime(DateTime.Now));
            return Ok(queues);
        }

        [HttpGet("availableQueuesForASpezesilation")]
        public async Task<IActionResult> AvailableQueuesForASpezesilation([FromQuery] string spezesilation)
        {
            var queues = await managerBL._doctorBL.AvailableQueuesForASpezesilation(spezesilation);
            return Ok(queues);
        }

        [HttpGet("AvailableQueuesForToday")]
        public async Task<IActionResult> AvailableQueuesForToday()
        {
            var queues = await managerBL._doctorBL.GetAvailableQueuesForASpesificday(DateOnly.FromDateTime(DateTime.Now));
            return Ok(queues);
        }

        [HttpPost("addDoctor")]
        public async Task<IActionResult> AddDoctor([FromBody] Doctor doctor)
        {
            await managerBL._doctorBL.AddDoctor(doctor);
            return Ok("Doctor added successfully");
        }

        [HttpDelete("deleteADayOfWork")]
        public async Task<IActionResult> DeleteADayOfWork([FromQuery] string firstName, [FromQuery] string lastName, [FromQuery] DateOnly day)
        {
            bool deleted = await managerBL._doctorBL.DeleteADayOfWork(firstName, lastName, day);
            return Ok(deleted);
        }

        [HttpDelete("deleteADoctor")]
        public async Task<IActionResult> DeleteADoctor([FromQuery] string id)
        {
            await managerBL._doctorBL.DeleteADoctor(id);
            return Ok("Doctor deleted successfully");
        }

        [HttpPut("updateDoctor")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto updatedDoctor)
        {
            await managerBL._doctorBL.UpdateDoctor(updatedDoctor);
            return Ok("Doctor updated successfully");
        }
    }
}
