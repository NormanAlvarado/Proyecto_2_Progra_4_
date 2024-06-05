using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.IService;
using Services.IServices;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Services.Extensions.DtoMapping;


namespace Services
{
    public class UserService : IUserService

    {
        private readonly MyDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserService(MyDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<User> Register(DtoRegister user)
        {
            var isExistsUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (isExistsUser != null)
            {
                throw new NotFoundException("The user already exists!");
            }

            User newUser = new User();

            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.Password = user.Password;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.RoleId = 2;


            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return newUser;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Name", user.Name.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("MobilePhone", user.PhoneNumber.ToString()),
                new Claim("NameIdentifier", user.Id.ToString()),

             //   new Claim(ClaimTypes.Role, user.RoleId.ToString()),

               new Claim("RoleId", user.RoleId.ToString())
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("ApplicationSettings:JWT_Secret").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<List<User>> Get()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("No se a encontrado el usuario");
            }
            return user;
        }

        public async Task<object> Login(DtoLogin request)
        {
       //     var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email);

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return null;
            }

            if (user.Email != request.Email)
            {
                return null;
            }

            if (user.Password != request.Password)
            {
                return null;
            }

            string token = CreateToken(user);


            return token;
        }
    }
}