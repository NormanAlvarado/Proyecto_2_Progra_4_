using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }          // Nombre del rol (USER o ADMIN)
        public ICollection<User> Users { get; set; }  // Relación con los usuarios
    }
}
