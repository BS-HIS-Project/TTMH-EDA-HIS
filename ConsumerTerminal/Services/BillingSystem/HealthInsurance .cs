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

        public override Decimal DiagnosticFee()
        {
            return 0;
        }

        public override decimal DrugPartialPayment()
        {
            var cost = DrugFee();


            switch((int)((cost + 1) / 100))
            {
                case 0:
                    return 0;
                case 1:
                    return 20;
                case 2:
                    return 40;
                case 3:
                    return 60;
                case 4:
                    return 80;
                case 5:
                    return 100;
                case 6:
                    return 120;
                case 7:
                    return 140;
                case 8:
                    return 160;
                case 9:
                    return 180;
                default:
                    return 200;
            }
        }
    }
}
