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
    }
}