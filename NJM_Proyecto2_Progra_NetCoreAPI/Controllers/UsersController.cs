using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.IService;
using static Services.Extensions.DtoMapping;

namespace NJM_Proyecto2_Progra_NetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _service.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _service.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("/register")]
        public async Task<ActionResult<User>> PostUser(DtoRegister user)
        {
            var newUser = await _service.Register(user);

            return Ok(newUser);
        }


        [HttpPost("login")]
        public async Task<Object> Login(DtoLogin request)
        {
            var user = await _service.Login(request);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(user);
        }

    }
}
