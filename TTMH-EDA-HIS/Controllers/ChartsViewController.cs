using HISDB.Data;
using HISDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMH_EDA_HIS.ViewModels;
using static TTMH_EDA_HIS.ViewModels.ChartsViewModel;

namespace TTMH_EDA_HIS.Controllers
{
    [Authorize(Roles = "Cashier")]
    public class ChartsViewController : Controller
    {
        private readonly HisdbContext _context;

        public ChartsViewController(HisdbContext context)
        {
            _context = context;

        }
        [Authorize(Roles = "Cashier")]
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("ChartsDetails");
        }
        [Authorize(Roles = "Cashier")]
        [HttpGet]
        public async Task<IActionResult> ChartsDetailsSearch(string q)
        {
            if (string.IsNullOrEmpty(q))
            {
                return RedirectToAction("ChartsDetails");
            }
            Detail detail = await _context.Details.FirstOrDefaultAsync(x => x.DetId == q);
            if (detail == null)
            {
                return RedirectToAction("ChartsDetails", "ChartsView", new { DetId = "NoResult"}); 
            }
            if(detail.PaymentTime != null)
            {
                return RedirectToAction("ChartsDetails", "ChartsView", new { DetId = "paid" });
            }

            return RedirectToAction("ChartsDetails", "ChartsView", new { DetId = q }); //Action名字, controller名稱不用加controller
        }


        [Authorize(Roles = "Cashier")]
        [HttpPost]
        public async Task<IActionResult> ChartsDetails(ChartsViewModel chavm)
        {
            Detail? detail = await _context.Details.FirstOrDefaultAsync(det => det.DetId == chavm.DetId);
            if(detail == null)
            {
                return NotFound();
            }
            detail.PaymentTime = DateTime.Now;
            ChartsViewModel vm = new ChartsViewModel()
            {
                Patient = new Patient(),
                DetId = "",
                age = 0,
                birthday = "",
                docsName = "",
                Vdate = "",
                Drugs = new List<ChartsViewModel_Drug>(),
                StatusCode = 3, //Message Success
                PaymentTime = DateTime.Now
            };
            await _context.SaveChangesAsync();

            return View(vm);
        }

        [Authorize(Roles = "Cashier")]
        [HttpGet]
        public async Task<IActionResult> ChartsDetails(string? DetId)
        {
            ChartsViewModel vm = new ChartsViewModel();            

            if (DetId == null || DetId == "paid" || DetId == "NoResult")
            {
                ChartsViewModel empty = new ChartsViewModel()
                {
                    Patient = new Patient(),
                    content = "",
                    DetId = "",
                    age = 0,
                    birthday = "",
                    docsName = "",
                    Vdate = "",
                    StatusCode = 0,
                    PaymentTime = DateTime.Now
                };
                switch (DetId)
                {
                    case "paid":
                        empty.StatusCode = 1; break;
                    case "NoResult":
                        empty.StatusCode = 2; break;
                }


                return View(empty);
            }

            Detail detail = await _context.Details.FirstOrDefaultAsync(det=>det.DetId==DetId);           
            if(detail == null)
            {
                return NotFound();
            }
            vm.DetId = detail.DetId;

            vm.PaymentTime = detail.PaymentTime;
            vm.Registration = detail.Registration;
            vm.PartialPayment = detail.PartialPayment;
            vm.DrugPartialPayment = detail.DrugPartialPayment;
            vm.Diagnostic = detail.Diagnostic;
            vm.MedicalCost = detail.MedicalCost;
            vm.Payable = detail.Payable;



            Patient patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == detail.PatientId);
            vm.Patient = patient;
            DateTime birth = patient.BirthDate;
            vm.birthday = birth.ToString("d");
            
            DoctorsPatientsChart? dpc = await (
                from i in _context.DoctorsPatientsCharts
                where i.PatientId == patient.PatientId
                orderby i.ChaId descending
                select i
            ).FirstOrDefaultAsync();

            string? doctorName = await _context.Employees.Where(x => x.EmployeeId == dpc.DoctorId).Select(x => x.EmployeeName).FirstOrDefaultAsync();
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

            DateTime zerotime = new DateTime(1, 1, 1);
            TimeSpan ageSpan = DateTime.Now - patient.BirthDate;
            vm.age = (zerotime + ageSpan).Year - 1;


            vm.Drugs = new List<ChartsViewModel_Drug>();

            foreach (ChartsDrugsDosage d in cdd)
            {
                ChartsViewModel_Drug drug = new ChartsViewModel_Drug();
                drug.DrugID = d.DrugId;
                drug.DosID = d.DosId;
                drug.Days = d.Days;
                drug.Total = d.Total;
                drug.Remark = d.Remark;
                drug.Quantity = d.Quantity;

                string drugName = await _context.Drugs.Where(x => x.DrugId == d.DrugId).Select(x => x.DrugName).FirstOrDefaultAsync();
                drug.DrugName = drugName;

                Dosage dosage = await _context.Dosages.FirstOrDefaultAsync(x => x.DosId == d.DosId);
                drug.Freq = dosage?.Freq;

                vm.Drugs.Add(drug);
            }


            return View(vm);
        }
    }
}
