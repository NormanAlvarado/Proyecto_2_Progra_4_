using DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IClinicService
    {
        public Task<List<Clinic>> Get();
    }
}
