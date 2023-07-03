using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HISDB.Data;
using HISDB.Models;
using TTMH_EDA_HIS.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using HISDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace TTMH_EDA_HIS.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly HisdbContext _context;

        public PharmacyController(HisdbContext context, ILogger<PharmacyController> logger)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("PharmacyDetails");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PharmacyDetailsSearch(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return RedirectToAction("PharmacyDetails");
            }
            Prescription prescription = await _context.Prescriptions.FirstOrDefaultAsync(x => x.PresNo == q);
            if(prescription == null)
            {
                return RedirectToAction("PharmacyDetails", "Pharmacy", new { PresNo = "NoResult" });
            }
            if(prescription.DrugDate != null)
            {
                return RedirectToAction("PharmacyDetails", "Pharmacy", new { PresNo = "paid" });
            }

            return RedirectToAction("PharmacyDetails", "Pharmacy", new {PresNo = q});
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PharmacyDetails(PresViewModel presvm)
        {
            if (presvm.content == null || presvm.content == "")
            {
                return RedirectToAction("PharmacyDetails");
            }
            Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.CaseHistory == presvm.content);
            if(patient == null)
            {
                Prescription prescription = await _context.Prescriptions.FirstOrDefaultAsync(pre=>pre.PresNo==presvm.content);
                patient = await _context.Patients.FirstOrDefaultAsync(p=>p.PatientId==prescription.PatientId);
            }


            PresViewModel vm = new PresViewModel()
            {
                Patient= patient ?? new Patient(),
                content = ""
            };
                        
            return View(vm);
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PharmacyDetails(string? PresNo)
        {
            PresViewModel vm = new PresViewModel();

            if (PresNo == null || PresNo=="paid" || PresNo=="NoResult")
            {
                PresViewModel empty = new PresViewModel()
                {
                    Patient = new Patient(),
                    content = "",
                    PresNo = "",
                    age = 0,
                    birthday="",
                    docsName = "",
                    Vdate = "",
                    Drugs = new List<PresViewModel_Drug>(),
                    StatusCode = 0
                };
                switch (PresNo)
                {
                    case "paid":
                        empty.StatusCode = 1;break;
                    case "NoResult":
                        empty.StatusCode = 2;break;
                }
                return View(empty);
            }
            Prescription pres = await _context.Prescriptions.FirstOrDefaultAsync(pre=>pre.PresNo == PresNo);
            if (pres == null)
            {
                return NotFound();
            }
            vm.PresNo = pres.PresNo;
            vm.DrugDate = pres.DrugDate;

            Patient patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == pres.PatientId);
            vm.Patient = patient;
            DateTime birth = patient.BirthDate;
            vm.birthday = birth.ToString("d");

            DoctorsPatientsChart? dpc = await (
                from i in _context.DoctorsPatientsCharts
                where i.PatientId == patient.PatientId
                orderby i.ChaId descending
                select i
            ).FirstOrDefaultAsync();

            string? doctorName = await _context.Employees.Where(x => x.EmployeeId == dpc.DoctorId).Select(x=>x.EmployeeName).FirstOrDefaultAsync();
            vm.docsName = doctorName;

            Chart chart = await _context.Charts.FirstOrDefaultAsync(x => x.ChaId == dpc.ChaId);
            //vm.Vdate = chart.Vdate;
            DateTime dateTimevalue = chart.Vdate;
            vm.Vdate = dateTimevalue.ToString("d");
            
            List<ChartsDrugsDosage> cdd = await (
                from i in _context.ChartsDrugsDosages
                where i.ChaId == chart.ChaId
                select i
            ).ToListAsync();

            vm.Drugs = new List<PresViewModel_Drug>();

            foreach (ChartsDrugsDosage d in cdd)
            {
                PresViewModel_Drug drug = new PresViewModel_Drug();
                drug.DrugID = d.DrugId;
                drug.DosID = d.DosId;
                drug.Days = d.Days;
                drug.Total = d.Total;
                drug.Remark = d.Remark;
                drug.Quantity = d.Quantity;

                string drugName = await _context.Drugs.Where(x=>x.DrugId == d.DrugId).Select(x=>x.DrugName).FirstOrDefaultAsync();
                drug.DrugName = drugName;

                Dosage dosage = await _context.Dosages.FirstOrDefaultAsync(x => x.DosId == d.DosId);
                drug.Freq = dosage?.Freq;

                vm.Drugs.Add(drug);
            }

            DateTime zerotime = new DateTime(1, 1, 1);
            TimeSpan ageSpan = DateTime.Now - patient.BirthDate;
            vm.age = (zerotime + ageSpan).Year - 1;










            return View(vm);
        }


    }
}
