using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<List<Appointment>> GetAll()
        {
            List<Appointment> result = await _context.GetAll();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Appointment> GetById(int id)
        {
            var result = await _context.GetById(id);

            return result;
        }

        [HttpPut]
        public async Task<IActionResult> Update(DtoUpdateAppointment appointment)
        {
            await _context.Update(appointment);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DtoAppointment>> Create(DtoAppointment appointment)
        {
            var result = await _context.Create(appointment);

            return Ok(result);
        }
        [HttpPatch("cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _context.Cancel(id);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _context.Delete(id);
            return NoContent();
        }
    }
}