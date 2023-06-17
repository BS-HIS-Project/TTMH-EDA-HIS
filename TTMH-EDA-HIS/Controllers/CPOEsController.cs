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
        [HttpGet("[controller]/[action]/{id?}")]
        public async Task<IActionResult> ChartList(int id)
        {
            int page=0;
            if (id<=1)
            {
                page = 1;
                id = 0;
            }
            else
            {
                page = id;
                id *= 6;
                id -= 6;
            }
            CPOEsChartListViewModel vm = new CPOEsChartListViewModel();  
            vm.Patients = await _context.Patients.Skip(id).Take(6).ToListAsync();
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
        [HttpGet]
        public async Task<IActionResult> PatientDetails(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Patient? patient=await _context.Patients.FirstOrDefaultAsync(x=>x.CaseHistory == id);
            if(patient == null) 
            {
                return NotFound(); 
            } 
            else
            {
                DoctorsPatientsChart? dpc = await _context.DoctorsPatientsCharts.FirstOrDefaultAsync(x => x.PatientId == patient.PatientId);
                Employee? employee = await _context.Employees.FindAsync(dpc.DoctorId);
                Chart? chart = await _context.Charts.FindAsync(dpc.ChaId);

                List<DrugTableTD> drugs = new List<DrugTableTD>();
                List<ChartsDrugsDosage>? cdds = await (from i in _context.ChartsDrugsDosages where i.ChaId == dpc.ChaId select i).ToListAsync();
                foreach(var i in cdds)
                {
                    Drug drug = await _context.Drugs.FindAsync(i.DrugId);
                    Dosage dosage = await _context.Dosages.FindAsync(i.DosId);
                    RoutesOfAdminstration? roa= await _context.RoutesOfAdminstrations.FindAsync(drug.Roaid);
                    drugs.Add(new DrugTableTD()
                    {
                        DrugID = drug.DrugId,
                        DrugName = drug.DrugName,
                        DosID = dosage.DosId,
                        Freq = "",  //Add later after Migration
                        BodyParts = roa.BodyParts,
                        Days = i.Days.ToString(),
                        Total = i.Total.ToString(),
                        Remark = i.Remark
                    });
                }

                if (patient.Gender == "1")
                {
                    patient.Gender = "男";
                }
                else
                {
                    patient.Gender = "女";
                }
                int age= DateTime.Today.Year-patient.BirthDate.Year;
                if (patient.BirthDate.Date > DateTime.Today.AddYears(--age)) { age--; }
                
                CPOEsPatientDetailsViewModel vm = new CPOEsPatientDetailsViewModel() 
                {
                    CaseHistory=patient.CaseHistory,
                    PatientName=patient.PatientName,
                    Gender=patient.Gender,
                    BirthDate=$"{patient.BirthDate.Year.ToString()}/{patient.BirthDate.Month.ToString()}/{patient.BirthDate.Day.ToString()}",
                    Age= age.ToString(),
                    DoctorName=employee.EmployeeName,
                    VDate=$"{chart.Vdate.Year.ToString()}/{chart.Vdate.Month.ToString()}/{chart.Vdate.Day.ToString()}",
                    Subject=chart.Subject,
                    Object=chart.Object,
                    drugList=drugs
                };
                return View(vm);
            }            
        }
    }
}

