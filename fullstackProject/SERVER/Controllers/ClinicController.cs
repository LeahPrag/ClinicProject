using BL.API;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using BL.Models;
using System.Net.Sockets;

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

        [HttpGet("/avalableQeuesForASpesificDay")]
        public async Task<List<M_AvailableQueue>> AvalableQeuesForASpesificDay(string firstName, string lastName, DateOnly day)
        {
            return await _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, day);
        }

        //another one for all qeues for this day

        [HttpGet("/avalableQeuesForDoctorToday")]
        public async Task<List<M_AvailableQueue>> avalableQeuesForDoctorToday(string firstName, string lastName)
        {
            return await _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));
        }
        [HttpPost("/makeAnApointment")]
        public async Task<bool> MakeAnApointment(string firstName, string lastName, string idClient, DateOnly day)
        {
            //    List<AvailableQueue>= _managerBL._doctorBL.IsDoctorAvailable(firstName, lastName, day);
            return await _managerBL._clinicQueueBL.DeleteAnApointment(firstName, lastName, idClient, day);

        }
        [HttpPost("/addQueues")]
        public async Task AddQueues()
        {

            await _managerBL._clinicQueueBL.Appointment();

        }

        [HttpGet("getAll/")]
        public async Task<List<Client>> GetClients()
        {
            return await _managerBL._clientBL.GetAllClients();
        }


        [HttpGet("getClientById/{id}")]
        public async Task<ActionResult<Client>> GetClientById(string id)
        {
            try
            {
                var client = await _managerBL._clientBL.GetClientById(id);
                return Ok(client);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost("add/")]
        public async Task<ActionResult> AddClient([FromBody] Client client)
        {
            try
            {
                await _managerBL._clientBL.AddClient(client);
                return Ok("Client added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteClient(string id)
        {
            try
            {
                await _managerBL._clientBL.RemoveClient(id);
                return Ok("Client deleted successfully");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("update/")]
        public async Task<ActionResult> UpdateClient([FromBody] Client updatedClient)
        {
            try
            {
                var existingClient = await _managerBL._clientBL.GetClientById(updatedClient.IdNumber);
                await _managerBL._clientBL.UpdateClient(updatedClient, existingClient);
                return Ok("Client updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}