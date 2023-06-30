using HISDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.Services.BillingSystem
{
    public class NonHealthInsurance : Billing
    {
        private readonly HisdbContext _context;

        private PartialServices _partialServices { get; set; }
        public NonHealthInsurance(string PatientId) : base(PatientId)
        {
            _context = new HisdbContext();


            if (ChackPatient())
            {
                _partialServices = new PartialServices(PatientId);
            }
            else
            {
                throw new Exception("Patient not found");
            }

        }

        public override Decimal DrugFee()
        {
            return 0;
        }

        public override Decimal DiagnosticFee()
        {
            return 500;
        }
    }
}
