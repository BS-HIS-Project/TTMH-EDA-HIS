using HISDB.Data;
using HISDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTMH_EDA_HIS.ViewModels;
using static TTMH_EDA_HIS.ViewModels.ChartsViewModel;

namespace TTMH_EDA_HIS.Controllers
{
    public class ChartsViewController : Controller
    {
        private readonly HisdbContext _context;

        public ChartsViewController(HisdbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
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
                return RedirectToAction("ChartsDetails"); //補要指向的Action
            }            

            return RedirectToAction("ChartsDetails", "ChartsView", new { DetId = q }); //Action名字, controller名稱不用加controller
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChartsDetails(ChartsViewModel chavm)
        {
            if (chavm.content == null || chavm.content == "")
            {
                return RedirectToAction("ChartsDetails");
            }

            Patient patient = await _context.Patients.FirstOrDefaultAsync(p => p.CaseHistory == chavm.content);
            if (patient == null)
            {
                Detail detail = await _context.Details.FirstOrDefaultAsync(det=>det.DetId == chavm.content);
                patient = await _context.Patients.FirstOrDefaultAsync(p => p.PatientId == detail.PatientId);                
            }

            ChartsViewModel vm = new ChartsViewModel()
            {
                Patient = patient ?? new Patient(),
                content = ""
            };            

            return View(vm);
        }
       


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChartsDetails(string? DetId)
        {
            ChartsViewModel vm = new ChartsViewModel();            

            if (DetId == null)
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
                    //Drugs = new List<PresViewModel_Drug>()
                };
                return View(empty);
            }

            Detail detail = await _context.Details.FirstOrDefaultAsync(det=>det.DetId==DetId);
            if(detail == null)
            {
                return NotFound();
            }
            vm.DetId = detail.DetId;

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
