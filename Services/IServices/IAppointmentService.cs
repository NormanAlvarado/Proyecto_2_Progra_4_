using DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Extensions.DtoMapping;

namespace Services.IServices
{
    public interface IAppointmentService
    {
        public Task<List<Appointment>> GetAll();

        public Task<Appointment> GetById(int id);

        public Task Update(DtoUpdateAppointment appointment);

        public Task<Appointment> Create(DtoAppointment appointment);

        public Task Cancel(int id);

        public Task Delete(int id);
    }
}
