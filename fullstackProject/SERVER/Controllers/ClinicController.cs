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
            if (!DateOnly.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out var parsedDate))
                return BadRequest("Invalid date format. Use dd.MM.yyyy");

            var result = await _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, parsedDate);
            return Ok(result);
        }

        [HttpGet("availableQueuesForToday")]
        public async Task<IActionResult> GetAvailableQueuesForToday([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var result = await _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));
            return Ok(result);
        }

        [HttpPost("makeAppointment")]
        public async Task<IActionResult> MakeAppointment([FromQuery] string idDoctor, [FromQuery] string idClient, [FromQuery] DateTime date)
        {
            await _managerBL._clinicQueueBL.MakeAnAppointment(idDoctor.Trim(), idClient.Trim(), date);
            return Ok("Appointment added successfully");
        }

        [HttpPost("addQueues")]
        public async Task<IActionResult> AddQueues()
        {
            await _managerBL._clinicQueueBL.GenerateFutureAvailableQueues();
            return Ok("Queues added successfully");
        }

        [HttpGet("clients")]
        public async Task<ActionResult<List<Client>>> GetClients()
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
        public async Task<IActionResult> AddClient([FromBody] Client client)
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
        public async Task<IActionResult> UpdateClient([FromBody] Client updatedClient)
        {
            var existingClient = await _managerBL._clientBL.GetClientById(updatedClient.IdNumber);
            await _managerBL._clientBL.UpdateClient(updatedClient, existingClient);
            return Ok("Client updated successfully");
        }
    }
}
