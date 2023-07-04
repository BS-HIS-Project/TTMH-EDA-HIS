using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;

namespace ConsumerTerminal.Services.BillingSystem
{
    public abstract class Billing
    {
        private readonly HisdbContext _context;
        private PartialServices _partialServices { get; set; }
        private List<CDDsList> _cddsList { get; set; }
        private string PatientId { get; set; }

        public Billing(string PatientId)
        {
            _context = new HisdbContext();

            this.PatientId = PatientId;
            _cddsList = new List<CDDsList>();

            if (ChackPatient())
            {
                _partialServices = new PartialServices(PatientId);
            }
            else
            {
                throw new Exception("Patient not found");
            }

        }
        // 掛號費
        public Decimal RegistrationFee()
        {
            Decimal temp = 0;
            Decimal total;

            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                temp = 30;
            }
            else if (DateTime.Now.Hour >= 18)
            {
                temp = 30;
            }

            if (_partialServices.GetAge() >= 70)
            {
                total = 100;
            }
            else
            {
                total = 150;
            }

            return total - temp;
        }

        // 部分負擔費
        public Decimal PartialPayment()
        {
            int age = _partialServices.GetAge();

            if (age < 3) return 0;
            else return 240;
        }

        public Decimal Total()
        {
            return RegistrationFee() + PartialPayment() + DiagnosticFee() + DrugPartialPayment();
        }

        // 藥費
        public Decimal DrugFee()
        {
            decimal total = 0;
            foreach (var cdd in _cddsList)
            {
                var _drug = _context.Drugs.Where(d => d.DrugId == cdd.DrugId).FirstOrDefault();
                if (_drug == null)
                {
                    throw new Exception("Drug not found");
                }
                else
                {
                    total += _drug.UnitPrice * (decimal)cdd.Total;
                }
            }

            return total;
        }

        // 藥品部分負擔費
        public abstract Decimal DrugPartialPayment();

        // 診察費
        public abstract Decimal DiagnosticFee();

        protected bool ChackPatient()
        {
            var _patient = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            if (_patient == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void close()
        {
            _context.Dispose();
        }

        public void AddCDDsList(string ChaId, string DrugId)
        {
            var CDD = _context.ChartsDrugsDosages.Where(c => c.ChaId == ChaId && c.DrugId == DrugId).FirstOrDefault();
            double Total = CDD.Total;
            
            if (Total == 0)
            {
                throw new Exception("Total is 0");
            }
            else
            {
                _cddsList.Add(new CDDsList { ChaId = ChaId, DrugId = DrugId, Total = Total });
            }

        }
    }
}
