using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Entities
{
    public class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }          // Nombre de la clínica
        public string Address { get; set; }       // Dirección de la clínica
        public ICollection<Appointment> Appointments { get; set; }  // Relación con las citas
    }
}
