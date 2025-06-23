using BL.API;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using BL.Models;
using System.Net.Sockets;
using BL.Exceptions;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> MakeAnApointment(string idDoctor, string idClient, DateTime date)
        {
            try
            {
                //await _managerBL._clinicQueueBL.DeleteAnApointment(idDoctor, idClient, date);
                await _managerBL._clinicQueueBL.MakeAnAppointment(idDoctor.Trim(), idClient.Trim(), date);
                return Ok("apointment added successfully");
            }
            catch (DoctorNotExsistException ex)
            {
                return NotFound(new
                {
                    error = "Doctor not found",doctorId = idDoctor,message = ex.Message});
            }
            catch (AvailableQueueNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DayOfQueueIsNotPermission ex)
            {
                return BadRequest(new { error = ex.Message, code = ex.StatusCode });
            }
            catch (ClientNotExsistException ex)
            {
                return NotFound(new
                {
                    error = "Client not found",clientId = idClient,message = ex.Message});
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    error = "Database update failed",
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Internal server error",
                    message = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }

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