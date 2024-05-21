using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }    // Fecha y hora de la cita
        public string Location { get; set; }      // Lugar de la cita (sucursal)
        public string Status { get; set; }        // Estado de la cita (ACTIVA o CANCELADA)
        public string AppointmentType { get; set; } // Tipo de cita (Medicina General, Odontología, etc.)
        public int UserId { get; set; }           // Clave foránea a la entidad User
        public User User { get; set; }            // Relación con la entidad User
        public int ClinicId { get; set; }         // Clave foránea a la entidad Clinic
        public Clinic Clinic { get; set; }        // Relación con la entidad Clinic
    }
}
