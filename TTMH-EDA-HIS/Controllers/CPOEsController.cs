using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using HISDB.Data;
using HISDB.Models;
using TTMH_EDA_HIS.ViewModels;
using HISDB;
using Microsoft.AspNetCore.Authorization;



namespace TTMH_EDA_HIS.Controllers
{
    public class CPOEsController : Controller
    {
        private readonly HisdbContext _context;
        public CPOEsController(HisdbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("ChartList");
        }

        [Authorize]
        [HttpGet("~/[controller]/[action]/page={id}")]
        [HttpGet("~/[controller]/[action]")]
        public async Task<IActionResult> ChartList(int? id)
        {
            id=id ?? 0;
            int page=0;
            if (id<=1)
            {
                page = 1;
                id = 0;
            }
            else
            {
                page = (int)id;
                id *= 6;
                id -= 6;
            }
            CPOEsChartListViewModel vm = new CPOEsChartListViewModel();  
            vm.Patients = await _context.Patients.Skip((int)id).Take(6).ToListAsync();
            vm.content = "";
            vm.UseButtonGp = true;
            vm.previous_page=page-1;
            vm.page1=page;
            vm.page2=page+1;
            vm.page3=page+2;
            vm.next_page=page+1;
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChartList(CPOEsChartListViewModel vm)
        {
            if(vm.content==null || vm.content == "")
            {
                return RedirectToAction("ChartList");
            }
            List<Patient> result=await(
                from i in _context.Patients
                where (
                    i.PatientId.Contains(vm.content) ||
                    i.PatientName.Contains(vm.content) ||
                    i.CaseHistory.Contains(vm.content)
                ) select i
            ).ToListAsync();
            vm.Patients = result;
            vm.content = "";
            vm.UseButtonGp = false;
            return View(vm);
        }

        [Authorize]
        [HttpGet("~/[controller]/[action]/CHistory={CaseHistory}&ChaID={ChaID}")]
        [HttpGet("~/[controller]/[action]/CHistory={CaseHistory}")]
        [HttpGet("~/[controller]/[action]")]
        public async Task<IActionResult> PatientDetails(string? CaseHistory,string? ChaID)
        {
            if(CaseHistory == null)
            {
                return NotFound();
            }
            Patient? patient=await _context.Patients.FirstOrDefaultAsync(x=>x.CaseHistory == CaseHistory);
            if(patient == null) 
            {
                return NotFound(); 
            }

            IEnumerable<string> ChaIDs = (
                from i in _context.DoctorsPatientsCharts
                where i.PatientId == patient.PatientId
                select i.ChaId
            );
            List<Chart> charts = await (
                from i in _context.Charts
                join j in ChaIDs on i.ChaId equals j
                orderby i.Vdate descending
                select i
            ).ToListAsync();


            CPOEsPatientDetailsViewModel vm = new CPOEsPatientDetailsViewModel();

            if (ChaID == null)
            {
                vm.chart = charts[0];
            }
            else
            {
                vm.chart = charts.FirstOrDefault(x => x.ChaId == ChaID);
                if (vm.chart == null)
                {
                    return NotFound();
                }
            }
            DoctorsPatientsChart dpc = await _context.DoctorsPatientsCharts.FirstOrDefaultAsync(x => x.ChaId == vm.chart.ChaId);
            Employee doctor = await _context.Employees.FindAsync(dpc.DoctorId);
            int age = DateTime.Today.Year - patient.BirthDate.Year;
            if (patient.BirthDate.Date > DateTime.Today.AddYears(--age)) { age--; }

            vm.CaseHistory = patient.CaseHistory;
            vm.PatientName = patient.PatientName;
            vm.Age = age.ToString();
            vm.Gender = patient.Gender;
            vm.BirthDate = $"{patient.BirthDate.Year.ToString()}/{patient.BirthDate.Month.ToString()}/{patient.BirthDate.Day.ToString()}";
            vm.DoctorID = doctor.EmployeeId;
            vm.DoctorName = doctor.EmployeeName;


            List<CPOEsPatientDetailsViewModel_DrugTableTD> drugs = new List<CPOEsPatientDetailsViewModel_DrugTableTD>();
            List<ChartsDrugsDosage>? cdds = await (from i in _context.ChartsDrugsDosages where i.ChaId == vm.chart.ChaId select i).ToListAsync();
            foreach (var i in cdds)
            {
                Drug drug = await _context.Drugs.FindAsync(i.DrugId);
                Dosage dosage = await _context.Dosages.FindAsync(i.DosId);
                RoutesOfAdminstration? roa = await _context.RoutesOfAdminstrations.FindAsync(drug.Roaid);
                drugs.Add(new CPOEsPatientDetailsViewModel_DrugTableTD()
                {
                    DrugID = drug.DrugId,
                    DrugName = drug.DrugName,
                    DosID = dosage.DosId,
                    Freq = dosage.Freq,
                    Quantity = i.Quantity,
                    Days = i.Days,
                    Total = i.Total,
                    Remark = i.Remark,
                    BodyParts = roa.BodyParts
                });
            }
            vm.Drugs = drugs;

            return View(vm);
        }
    }
}

