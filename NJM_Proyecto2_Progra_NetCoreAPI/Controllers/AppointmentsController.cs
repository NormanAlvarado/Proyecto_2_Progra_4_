﻿using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.IServices;
using static Services.Extensions.DtoMapping;

namespace NJM_Proyecto2_Progra_NetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _context;

        public AppointmentsController(IAppointmentService context)
        {
            _context = context;
        }

        [HttpGet,  Authorize]

        public async Task<List<Appointment>> GetAll()
        {
            List<Appointment> result = await _context.GetAll();

            return result;
        }
        

        [HttpGet("today"), Authorize]
        public async Task<ActionResult<List<Appointment>>> GetAppointmentsForToday()
        {
            try
            {
                var appointments = await _context.GetAppointmentsForToday();
               

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error");
            }
        }

            [HttpGet("{id}"), Authorize]
        public async Task<Appointment> GetById(int id)
        {
            var result = await _context.GetById(id);

            return result;
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> Update(DtoUpdateAppointment appointment)
        {
            await _context.Update(appointment);
            return NoContent();
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<DtoAppointment>> Create(DtoAppointment appointment)
        {
            var result = await _context.Create(appointment);

            return Ok(result);
        }
        [HttpPatch("cancel/{id}"), Authorize]
        public async Task<IActionResult> Cancel(int id)
        {
            await _context.Cancel(id);
            return NoContent();
        }


        [HttpDelete("{id}"), Authorize(Policy = "RequireAdministratorRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.Delete(id);
            return NoContent();
        }
    }
}