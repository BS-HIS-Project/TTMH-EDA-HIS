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

        public override Decimal DiagnosticFee()
        {
            return 0;
        }
    }
}
