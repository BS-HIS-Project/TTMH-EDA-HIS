using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using TTMH_EDA_HIS.Data;
using TTMH_EDA_HIS.Models;
using TTMH_EDA_HIS.ViewModels;

namespace TTMH_EDA_HIS.Controllers
{
    public class CPOEsController : Controller
    {
        private readonly HisdbContext _context;
        public CPOEsController(HisdbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ChartList");
        }
        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> ChartList(int id)
        {
            int iid=0;
            if (id<=1)
            {
                iid = 1;
                id = 0;
            }
            else
            {
                iid = id;
                id *= 6;
                id -= 6;
            }
            List<Patient?> result = await _context.Patients.Skip(id).Take(6).ToListAsync();
            ViewData["previous"]=iid-1;
            ViewData["pg1"]=iid;
            ViewData["pg2"]=iid+1;
            ViewData["pg3"]=iid+2;
            ViewData["next"]=iid+1;
            return View(result);
        }
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

                List<CPOEsPatientDetailsViewModel.DrugTableTD> drugs = new List<CPOEsPatientDetailsViewModel.DrugTableTD>();
                List<ChartsDrugsDosage>? cdds = await (from i in _context.ChartsDrugsDosages where i.ChaId == dpc.ChaId select i).ToListAsync();
                foreach(var i in cdds)
                {
                    Drug drug = await _context.Drugs.FirstOrDefaultAsync(x => x.DrugId == i.DrugId);
                    Dosage dosage = await _context.Dosages.FirstOrDefaultAsync(x => x.DosId == i.DosId);
                    drugs.Add(new CPOEsPatientDetailsViewModel.DrugTableTD()
                    {
                        DrugID = drug.DrugId,
                        DrugName = drug.DrugName,
                        DosID = dosage.DosId,
                        Direction = dosage.Direction,
                        SuggestedUsage = drug.SuggestedUsage,
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
                    BirthDate=$"{patient.BirthDate.Year.ToString()}年{patient.BirthDate.Month.ToString()}月{patient.BirthDate.Day.ToString()}日",
                    Age= age.ToString(),
                    DoctorName=employee.EmployeeName,
                    VDate=$"{chart.Vdate.Year.ToString()}年{chart.Vdate.Month.ToString()}月{chart.Vdate.Day.ToString()}日",
                    drugList=drugs
                };
                return View(vm);
            }            
        }
    }
}
