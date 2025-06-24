using System;
using BL.API;
using BL.Exceptions;
using BL.Models;
using DAL.Models;
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
        // GET: api/<gradeManagerControllerש
        [HttpGet]
        public async Task<int> GetNumOfClientForToday(string firstName, string lastName)
        {

            return  await managerBL._doctorBL.GetNumOfClientForToday(firstName, lastName, DateOnly.FromDateTime(DateTime.Now));



            //return 5;
        }
        [HttpGet("/getAllDoctors")]
        public async Task<List<M_Doctor>> GetDoctors()
        {

            return await managerBL._doctorBL.GetDoctors();

        }
        //
        [HttpGet("/DoctorqeuesForToday")]
        public async Task<List<M_ClinicQueue>> DoctorQeuesForToday(string firstName, string lastName)
        {

            return await managerBL._doctorBL.GetDoctorQueesForToday( firstName,  lastName, DateOnly.FromDateTime(DateTime.Now));

        }
        [HttpGet("/availableQueuesForASpezesilation")]
        public async Task<List<M_AvailableQueue>> AvailableQueuesForASpezesilation(string spezesilation)
        {

            return await managerBL._doctorBL.AvailableQueuesForASpezesilation(spezesilation);

        }

        [HttpGet("/AvailableQueuesForToday")]
        public async Task<List<M_AvailableQueue>> AvailableQueuesForToday()
        {
    
            return await managerBL._doctorBL.GetAvailableQueesForASpesificday(DateOnly.FromDateTime(DateTime.Now));

        }
        [HttpPost("/addDoctor")]
        public async Task<ActionResult> AddDoctor([FromBody] Doctor doctor)
        {


            try
            {
                await managerBL._doctorBL.AddDoctor(doctor);
                return Ok("Doctor added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/deleteADayOfWork")]
        public async Task<bool> DeleteADayOfWork(string firstName, string lastName, DateOnly day)
        {

            return await managerBL._doctorBL.DeleteADayOfWork(firstName, lastName,  day);

        }
        [HttpDelete("/deleteADoctor")]
        public async Task<ActionResult> DeleteADoctor(string id)
        {

            try
            {
                await managerBL._doctorBL.DeleteADoctor(id);
                return Ok("Doctor deleted successfully");
            }
            catch (DoctorNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // בעיה לא צפויה אחרת
                return StatusCode(500, "Something went wrong");
            }

        }
        [HttpPut("/updateDoctor")]
        public async Task<ActionResult> UpdateDoctor([FromBody] UpdateDoctorDto updatedDoctor)
        {
            try
            {

                await managerBL._doctorBL.UpdateDoctor(updatedDoctor);
                return Ok("Doctor updated successfully");
            }
            catch (DoctorNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
