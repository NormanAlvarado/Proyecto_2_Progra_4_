using DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Extensions.DtoMapping;


namespace Services.IService
{
    public interface IUserService
    {
        public Task<List<User>> Get();

        public Task<User> GetById(int id);

        public Task<User> Register(DtoRegister request);

        public Task<object> Login(DtoLogin request);
    }
}
