using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.EntityFrameworkCore;
using Services.IServices;
using Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Extensions.DtoMapping;

namespace Services
{
    public class AppointmentService : IAppointmentService

    {
        private readonly MyDbContext _context;

        public AppointmentService(MyDbContext context)
        {
            _context = context;
        }
        public async Task Cancel(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment != null)
            {
                var todayDate = DateTime.Now;
                var appointmentDay = appointment.AppointmentDate;

                var diference = appointmentDay - todayDate;

                if (diference.TotalHours > 24)
                {
                    appointment.Status = false;

                    _context.Attach(appointment);
                    _context.Entry(appointment).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }
                else
                {
                    throw new Exception("Faltan menos de 24 para la cita no se puede cancelar");
                }
            }
            else
            {
                throw new NotFoundException("Appointment not found!");
            }
        }

        public async Task<Appointment> Create(DtoAppointment request)
        {
            var validateAppointments = await _context.Appointments.Where
                                       (a => a.UserId == request.UserId &&
                                       a.AppointmentDate.Date == request.AppointmentDate.Date)
                                       .ToListAsync();

            if (validateAppointments.Count > 0)
            {
                throw new InvalidOperationException("Pacient already have an appointment today");
            }

            Appointment appointment = new Appointment();

            appointment.AppointmentDate = request.AppointmentDate;
            appointment.Location = request.Location;
            appointment.TypeOfAppointment = request.TypeOfAppointment;
            appointment.UserId = request.UserId;
            appointment.ClinicId = request.ClinicId;
            appointment.Status = true;

            _context.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;

        }

        public async Task Delete(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment != null)
            {
                _context.Remove(appointment);
                await _context.SaveChangesAsync();

            }

            if (appointment == null)
            {
                throw new NotFoundException("Appointment not found!");
            }
        }

        public async Task<List<Appointment>> GetAll()
        {
            List<Appointment> list = await _context.Appointments.ToListAsync();

            return list;
        }

        public async Task<Appointment> GetById(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                throw new NotFoundException("Appointment not found!");
            }
            return appointment;
        }

        public async Task Update(DtoUpdateAppointment request)
        {
            DtoUpdateAppointment updateAppointment = new DtoUpdateAppointment();

            updateAppointment.Id = request.Id;
            updateAppointment.AppointmentDate = request.AppointmentDate;
            updateAppointment.Location = request.Location;
            updateAppointment.TypeOfAppointment = request.TypeOfAppointment;
            updateAppointment.UserId = request.UserId;
            updateAppointment.ClinicId = request.ClinicId;

            var current = _context.Appointments.Find(request.Id);

            _context.Entry(current).CurrentValues.SetValues(updateAppointment);

            await _context.SaveChangesAsync();
        }
    }

}
