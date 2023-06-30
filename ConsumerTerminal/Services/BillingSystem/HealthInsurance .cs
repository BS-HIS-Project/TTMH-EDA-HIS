using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;

namespace ConsumerTerminal.Services.BillingSystem
{
    // 有健保
    public class HealthInsurance : Billing
    {
        private readonly HisdbContext _context;

        private PartialServices _partialServices { get; set; }
        public HealthInsurance(string PatientId) : base(PatientId)
        {
            _context = new HisdbContext();

            if(ChackPatient())
            {
                _partialServices = new PartialServices(PatientId);
            }
            else
            {
                throw new Exception("Patient not found");
            }

        }
        // 藥費
        public override decimal DrugFee()
        {
            return 0;
        }

        // 部分負擔費
        public override Decimal PartialPayment()
        {
            int age = _partialServices.GetAge();

            if (age < 3) return 0;
            else return 240;
        }

        // 掛號費
        public override Decimal RegistrationFee()
        {
            Decimal temp = 0;
            Decimal total = 0;

            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                temp = 30;
            }
            else if (DateTime.Now.Hour >= 18)
            {
                temp = 30;
            }

            if(_partialServices.GetAge() >= 70)
            {
                total = 100;
            } else
            {
                total = 150;
            }
            
            return total - temp;
        }

        public override Decimal DiagnosticFee()
        {
            return 0;
        }
    }
}
