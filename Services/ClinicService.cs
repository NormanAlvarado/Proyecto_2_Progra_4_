using DataAcess.Data;
using DataAcess.Entities;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClinicService
    {
        private readonly MyDbContext _context;

        public ClinicService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Clinic>> Get()
        {
            List<Clinic> list = await _context.Clinics.ToListAsync();
            return list;
        }
    }
}
