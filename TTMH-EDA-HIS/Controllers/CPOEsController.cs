using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using HISDB.Data;
using HISDB.Models;
using TTMH_EDA_HIS.ViewModels;
using HISDB;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Immutable;

namespace TTMH_EDA_HIS.Controllers
{
    public class CPOEsController : Controller
    {
        private readonly HisdbContext _context;
        public CPOEsController(HisdbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("ChartList");
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public async Task<IActionResult> ChartList(int? page)
        {
            page=page ?? 0;
            int dpage=0;
            if (page<=1)
            {
                dpage = 1;
                page = 0;
            }
            else
            {
                dpage = (int)page;
                page *= 6;
                page -= 6;
            }
            CPOEsChartListViewModel vm = new CPOEsChartListViewModel();
            
            vm.ChartInfos = new List<CPOEsChartListViewModel_ChartInfos>();
            string[] chartIDs = await (
                from cha in _context.Charts
                orderby cha.Vdate descending
                select cha.ChaId
            ).Skip((int)page).Take(6).ToArrayAsync();
            foreach (string i in chartIDs) 
            {
                string? patientsID = await (
                    from dpc in _context.DoctorsPatientsCharts
                    where dpc.ChaId == i
                    select dpc.PatientId
                ).FirstOrDefaultAsync();
                Patient? patient = await _context.Patients.FindAsync(patientsID);
                CPOEsChartListViewModel_ChartInfos infos = new CPOEsChartListViewModel_ChartInfos()
                {
                    ChaID = i,
                    CaseHistory = patient.CaseHistory,
                    PatientName = patient.PatientName,
                    Gender = patient.Gender
                };    
                vm.ChartInfos.Add(infos);
            }
            vm.content = "";
            vm.UseButtonGp = true;
            vm.previous_page=dpage-1;
            vm.page1=dpage;
            vm.page2=dpage+1;
            vm.page3=dpage+2;
            vm.next_page=dpage+1;
            return View(vm);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult> ChartList(CPOEsChartListViewModel vm)
        {
            if(vm.content==null || vm.content == "")
            {
                return RedirectToAction("ChartList");
            }
            string[] contents = vm.content.Trim().ToUpper().Split(" ");
            vm.ChartInfos = new List<CPOEsChartListViewModel_ChartInfos>();

            foreach(string content in contents)
            {
                Patient[] patients = await (
                    from pat in _context.Patients
                    where (
                        pat.PatientId.ToUpper().Contains(content) ||
                        pat.PatientName.ToUpper().Contains(content) ||
                        pat.CaseHistory.ToUpper().Contains(content)
                    )
                    select pat
                ).Take(10).ToArrayAsync();
                foreach (Patient patient in patients)
                {
                    string[]? chaIDss = await (
                        from dpc in _context.DoctorsPatientsCharts
                        where dpc.PatientId == patient.PatientId
                        orderby dpc.ChaId descending
                        select dpc.ChaId
                    ).Take(10).ToArrayAsync();
                    foreach (var chaID in chaIDss)
                    {
                        CPOEsChartListViewModel_ChartInfos infos = new CPOEsChartListViewModel_ChartInfos()
                        {
                            ChaID = chaID,
                            CaseHistory = patient.CaseHistory,
                            PatientName = patient.PatientName,
                            Gender = patient.Gender
                        };
                        if (!vm.ChartInfos.Any(x => x.ChaID == infos.ChaID))
                        {
                            vm.ChartInfos.Add(infos);
                        }
                    }
                }

                string[] chaIDs = await (
                    from cha in _context.Charts
                    where cha.ChaId.ToUpper().Contains(content)
                    select cha.ChaId
                ).Take(10).ToArrayAsync();
                foreach (string chaID in chaIDs)
                {
                    string? patientID = await (
                        from dpc in _context.DoctorsPatientsCharts
                        where dpc.ChaId == chaID
                        select dpc.PatientId
                    ).FirstOrDefaultAsync();
                    Patient? patient = await _context.Patients.FindAsync(patientID);
                    CPOEsChartListViewModel_ChartInfos infos = new CPOEsChartListViewModel_ChartInfos()
                    {
                        ChaID = chaID,
                        CaseHistory = patient.CaseHistory,
                        PatientName = patient.PatientName,
                        Gender = patient.Gender
                    };
                    if (!vm.ChartInfos.Any(x => x.ChaID == infos.ChaID))
                    {
                        vm.ChartInfos.Add(infos);
                    }
                }
            }

            vm.ChartInfos = vm.ChartInfos.Skip(0).Take(15).ToList();
            vm.content = "";
            vm.UseButtonGp = false;
            return View(vm);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public async Task<IActionResult> PatientDetails(string? CaseHistory, string? ChaID)
        {
            CPOEsPatientDetailsViewModel vm = new CPOEsPatientDetailsViewModel();
			Patient? patient = null;
            IEnumerable<string>? ChaIDs = null;
            Chart[]? charts = null;
            DoctorsPatientsChart? dpc = null;
            bool isLatest = false;
			if (CaseHistory == null && ChaID == null)
            {
                return NotFound();
            }
            else if (CaseHistory == null && ChaID != null)
            {
                vm.chart = await _context.Charts.FindAsync(ChaID);
                if (vm.chart == null)
                {
                    return NotFound();
                }
				dpc = await _context.DoctorsPatientsCharts.FirstOrDefaultAsync(x => x.ChaId == ChaID);
				patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == dpc.PatientId);
			}
			else
			{
				patient = await _context.Patients.FirstOrDefaultAsync(x => x.CaseHistory == CaseHistory);
				if (patient == null)
				{
					return NotFound();
				}
				if (ChaID == "latest" || ChaID == null)
				{
                    isLatest = true;
				}
				else
				{
					vm.chart = await _context.Charts.FirstOrDefaultAsync(x => x.ChaId == ChaID);
					if (vm.chart == null)
					{
						return NotFound();
					}
					dpc = await _context.DoctorsPatientsCharts.FirstOrDefaultAsync(x => x.ChaId == ChaID);
				}
			}
			ChaIDs = (
					from i in _context.DoctorsPatientsCharts
					where i.PatientId == patient.PatientId
					select i.ChaId
				);
			charts = await (
				from i in _context.Charts
				join j in ChaIDs on i.ChaId equals j
				orderby i.Vdate descending
				select i
			).ToArrayAsync();
            if (isLatest) { 
                vm.chart = charts[0];
				dpc = await _context.DoctorsPatientsCharts.FirstOrDefaultAsync(x => x.ChaId == charts[0].ChaId);
			}
			Employee? doctor = await _context.Employees.FindAsync(dpc.DoctorId);
            DateTime zerotime = new DateTime(1, 1, 1);
            TimeSpan ageSpan = DateTime.Now - patient.BirthDate;
            int age = (zerotime + ageSpan).Year - 1;

            vm.CaseHistory = patient.CaseHistory;
            vm.PatientID = patient.PatientId;
            vm.PatientName = patient.PatientName;
            vm.Age = age.ToString();
            vm.Gender = patient.Gender;
            vm.BirthDate = patient.BirthDate.ToString("yyyy/MM/dd");
            vm.DoctorID = doctor.EmployeeId;
            vm.DoctorName = doctor.EmployeeName;


            List<CPOEsPatientDetailsViewModel_DrugTableTD> drugs = new List<CPOEsPatientDetailsViewModel_DrugTableTD>();
            List<ChartsDrugsDosage>? cdds = await (from i in _context.ChartsDrugsDosages where i.ChaId == vm.chart.ChaId select i).ToListAsync();
            foreach (var i in cdds)
            {
                Drug? drug = await _context.Drugs.FindAsync(i.DrugId);
                Dosage? dosage = await _context.Dosages.FindAsync(i.DosId);
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

            vm.RecordsOfChaID = new List<string>();
            vm.RecordsOfVDate = new List<string>();
            for (int i=0;i<charts.Length;i++)
            {
                vm.RecordsOfChaID.Add(charts[i].ChaId);
                vm.RecordsOfVDate.Add(charts[i].Vdate.ToString("yyyy/MM/dd"));
            }
            vm.FirstChart = charts[charts.Length - 1].ChaId;
            vm.LastChart = charts[0].ChaId;

            int currentIndex = charts.ToList().FindIndex(x=>x.ChaId==vm.chart.ChaId);
            if (currentIndex + 1 <= charts.Length - 1)
            {
                vm.PreviousChart = charts[currentIndex + 1].ChaId;
            }
            else
            {
                vm.PreviousChart = charts[charts.Length - 1].ChaId;
            }
            if (currentIndex-1 >= 0)
            {
                vm.NextChart = charts[currentIndex-1].ChaId;
            }
            else
            {
                vm.NextChart = charts[0].ChaId;
            }
            vm.VDate_Display = vm.chart.Vdate.ToString("yyyy/MM/dd");
            vm.ChaID_Display = vm.chart.ChaId.Substring(vm.chart.ChaId.Length-3,3);

            return View(vm);
        }
    }
}

