using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IServices;

namespace NJM_Proyecto2_Progra_NetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicService _context;

        public ClinicsController(IClinicService context)
        {
            _context = context;
        }

        [HttpGet]
        public Task<List<Clinic>> Get()
        {
            return _context.Get();
        }
    }
}