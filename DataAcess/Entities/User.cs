using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Entities
{
  public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }          // Nombre del usuario
        public string Email { get; set; }         // Email del usuario
        public string PhoneNumber { get; set; }   // Teléfono del usuario
        public string PasswordHash { get; set; }  // Hash de la contraseña del usuario
        public int RoleId { get; set; }           // Clave foránea a la entidad Role
        public Role Role { get; set; }            // Relación con la entidad Role
        public ICollection<Appointment> Appointments { get; set; }  // Relación con las citas
    }
}
