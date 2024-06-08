using DataAcess.Data;
using DataAcess.Entities;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Services.IService;
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

        private readonly IUserService _userService;

        public AppointmentService(MyDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
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


            var user = await _context.Users.FindAsync(request.UserId);
            var userEmail = user?.Email; // Asegúrate de que el usuario tenga un correo electrónico

            // Llamar la función de enviar correo aquí
            if (!string.IsNullOrEmpty(userEmail))
            {
                SendConfirmationEmail(appointment, userEmail);
            }
           
          
            return appointment;

        }

        private void SendConfirmationEmail(Appointment appointment, string userEmail)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Clinica MNJNW", ""));
                message.To.Add(new MailboxAddress("", userEmail));
                message.Subject = "Confirmación de Cita";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #dcdcdc; border-radius: 10px;'>
                    <h2 style='color: #333;'>Confirmación de Cita</h2>
                    <p>Hola,</p>
                    <p>Tienes una nueva cita. Detalles:</p>
                    <table style='width: 100%; border-collapse: collapse;'>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #dcdcdc;'>Fecha y Hora:</td>
                            <td style='padding: 10px; border: 1px solid #dcdcdc;'>{appointment.AppointmentDate.ToString("f")}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #dcdcdc;'>Ubicación:</td>
                            <td style='padding: 10px; border: 1px solid #dcdcdc;'>{appointment.Location}</td>
                        </tr>
                        <tr>
                            <td style='padding: 10px; border: 1px solid #dcdcdc;'>Tipo de Cita:</td>
                            <td style='padding: 10px; border: 1px solid #dcdcdc;'>{appointment.TypeOfAppointment}</td>
                        </tr>
                    </table>
                    <p style='margin-top: 20px;'>Gracias por elegir nuestra clínica.</p>
                    <p>Saludos,</p>
                    <p><strong>Clinica MNJNW</strong></p>
                </div>
            </body>
            </html>";
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    client.Authenticate("dab.elis0616@gmail.com", "lisxjxitqvslecqv");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
            
        

                // Aquí deberías registrar la excepción 'ex'
                // Puedes usar un sistema de logging como Serilog, NLog, etc.
                // También podrías decidir qué hacer a continuación, por ejemplo, reintentar el envío o informar al usuario
                throw; // O manejar la excepción de manera que no se propague como un error 500
            }
        }






        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///
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
        public async Task<List<Appointment>> GetAppointmentsForToday()
        {
           
            var today = DateTime.Today;

            
            var tomorrow = today.AddDays(1);

            
            List<Appointment> todayAppointments = await _context.Appointments
                .Where(a => a.AppointmentDate >= today && a.AppointmentDate < tomorrow)
                .ToListAsync();

            return todayAppointments;
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
