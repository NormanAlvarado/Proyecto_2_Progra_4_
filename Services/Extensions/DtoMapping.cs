using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public class DtoMapping
    {
        #region User
        public class DtoLogin
        {
            public string Email { get; set; }

            public string Password { get; set; }
        }
        public class DtoRegister
        {
            public string Name { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string PhoneNumber { get; set; }
        }


        #endregion

        #region Appointment
        public class DtoAppointment
        {
            public DateTime AppointmentDate { get; set; }

            public string Location { get; set; }

            public string TypeOfAppointment { get; set; }

            public int UserId { get; set; }

            public int ClinicId { get; set; }
        }

        public class DtoUpdateAppointment
        {
            public int Id { get; set; }
            public DateTime AppointmentDate { get; set; }

            public string Location { get; set; }

            public string TypeOfAppointment { get; set; }

            public int UserId { get; set; }

            public int ClinicId { get; set; }
        }
        #endregion
    }
}
    
