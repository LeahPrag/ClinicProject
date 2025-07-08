using BL.API;
using BL.Exceptions;
using BL.Models;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : ControllerBase
    {
        private readonly IManagerBL _managerBL;
        public ClinicController(IManagerBL managerBL)
        {
            _managerBL = managerBL;
        }
        [HttpGet("availableQueuesForDay")]
        public async Task<IActionResult> GetAvailableQueuesForDay(
        [FromQuery] string firstName,
        [FromQuery] string lastName,
        [FromQuery] string date)
        {
            if (!DateConverter.TryConvertToDateOnly(date, out DateOnly parsedDate))
                return BadRequest("Invalid date format. Use dd.MM.yyyy");
            var result = await _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, parsedDate);
            return Ok(result);
        }

        [HttpGet("availableQueuesForToday")]
        public async Task<IActionResult> GetAvailableQueuesForToday([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var result = await _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, today);
            return Ok(result);
        }

        [HttpPost("makeAppointment")]
        public async Task<IActionResult> MakeAppointment([FromQuery] string idDoctor, [FromQuery] string idClient, [FromQuery] string date,int hour)
        {
            if (!DateConverter.TryConvertToDateOnly(date, out DateOnly appointmentDate))
                return BadRequest("Invalid date format. Use dd.MM.yyyy");

            await _managerBL._clinicQueueBL.MakeAnAppointment(idDoctor.Trim(), idClient.Trim(), appointmentDate, hour);
            return Ok($"Appointment added successfully with doctor:{idDoctor}, client:{idClient}, date:{date},hour:{hour}");
        }
        [HttpPost("addQueues")]
        public async Task<IActionResult> AddQueues()
        {
            await _managerBL._clinicQueueBL.GenerateFutureAvailableQueues();
            return Ok("Queues added successfully");
        }

        [HttpGet("clients")]
        public async Task<ActionResult<List<M_Client>>> GetClients()
        {
            return await _managerBL._clientBL.GetAllClients();  
        }
        [HttpGet("clients/{id}")]
        public async Task<ActionResult<Client>> GetClientById(string id)
        {
            var client = await _managerBL._clientBL.GetClientById(id);
            return Ok(client);
        }

        [HttpPost("clients")]
        public async Task<IActionResult> AddClient([FromBody] M_Client client)
        {
            await _managerBL._clientBL.AddClient(client);
            return Ok("Client added successfully");
        }

        [HttpDelete("clients/{id}")]
        public async Task<IActionResult> DeleteClient(string id)
        {
            await _managerBL._clientBL.RemoveClient(id);
            return Ok("Client deleted successfully");
        }

        [HttpPut("clients")]
        public async Task<IActionResult> UpdateClient([FromBody] M_Client updatedClient)
        {
            var existingClientId = updatedClient.IdNumber;
            await _managerBL._clientBL.UpdateClient(updatedClient, existingClientId);
            return Ok("Client updated successfully");
        }

    }
}
