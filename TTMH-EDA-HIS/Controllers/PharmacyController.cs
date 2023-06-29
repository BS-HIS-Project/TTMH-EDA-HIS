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

        //領藥列表
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> Details()
        {
            //IEnumerable<PresViewModel> presModel =
            //await(
            //    from pres in _context.Prescriptions //領藥
            //    join pats in _context.Patients on pres.PatientId equals pats.PatientId //病患
            //    join dpcs in _context.DoctorsPatientsCharts on pats.PatientId equals dpcs.PatientId //醫生病人就診
            //    join chas in _context.Charts on dpcs.ChaId equals chas.ChaId //就診
            //    join docs in _context.Doctors on dpcs.DoctorId equals docs.DoctorId //醫生
            //    join emps in _context.Employees on docs.DoctorId equals emps.EmployeeId //員工
            //    where pres.PresNo == "PRE2023053013001001"
            //    select new PresViewModel
            //    {
            //        PresId = pres.PresNo, //領藥號
            //        chId = pats.CaseHistory, //病例號
            //        PatsName = pats.PatientName, //病患姓名
            //        gender = Convert.ToInt16(pats.Gender), //性別
            //        birthday = pats.BirthDate, //出生年月日
            //        docsName = emps.EmployeeName, //醫生姓名
            //        Vdate = chas.Vdate //就診日期
            //    }
            //).ToListAsync();

            //IEnumerable<DrugsViewModel> drugsModel =
            //    await(
            //        from drugs in _context.Drugs //藥品
            //        join CDDs in _context.ChartsDrugsDosages on drugs.DrugId equals CDDs.DrugId //就診藥品用藥頻率
            //        join doss in _context.Dosages on CDDs.DosId equals doss.DosId //用藥頻率
            //        join ROAs in _context.RoutesOfAdminstrations on drugs.Roaid equals ROAs.Roaid //給藥途徑
            //        select new DrugsViewModel
            //        {
            //            DrugId = drugs.DrugId,
            //            DrugName = drugs.DrugName,
            //            dosId = doss.DosId,
            //            bp = ROAs.BodyParts,
            //            days = CDDs.Days,
            //            //total = CDDs.Total,
            //            remark = CDDs.Remark
            //        }
                
                
            //    ).ToListAsync();


            //return View(presModel);
            return View();
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
            if (PresNo == null)
            {
                PresViewModel empty = new PresViewModel()
                {
                    Patient = new Patient(),
                    content = "",
                    PresNo = "",
                    age = 0,
                    docsName = "",
                    Vdate = DateTime.Now
                };
                return View(empty);
            }
            Prescription pres = await _context.Prescriptions.FirstOrDefaultAsync(pre=>pre.PresNo == PresNo);
            if (pres == null)
            {
                return NotFound();
            }

            Patient patient = await _context.Patients.FirstOrDefaultAsync(x => x.PatientId == pres.PatientId);
            









            PresViewModel vm = new PresViewModel()
            {
                content = "",
                Patient = patient ?? new Patient(),
            };

            return View(vm);
        }


    }
}
