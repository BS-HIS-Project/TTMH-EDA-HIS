using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;

namespace ConsumerTerminal.Services
{
    public class PartialServices
    {
        private readonly HisdbContext _context;
        private Patient _patient { get; set; }

        public PartialServices(string PatientId)
        {
            _context = new HisdbContext();

            _patient = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault() ?? new Patient();
        }

        public int GetAge()
        {
            var birthDate = _patient.BirthDate;
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
