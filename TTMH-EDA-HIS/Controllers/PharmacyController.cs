using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HISDB.Data;
using HISDB.Models;
using TTMH_EDA_HIS.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using HISDB;

namespace TTMH_EDA_HIS.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly ILogger<PharmacyController> _logger;
        private readonly HisdbContext _context;

        public PharmacyController(HisdbContext context, ILogger<PharmacyController> logger)
        {
            _context = context;

            string conn = _context.Database.GetDbConnection().ConnectionString;
            _logger = logger;
        }

        //領藥列表
        public IActionResult Index()
        {

            return View();
        }
        public async Task<IActionResult> Details()
        {
            //var prestb = from pres in _context.Prescriptions //領藥
            //             join pats in _context.Patients on pres.PatientId equals pats.PatientId //病患
            //             join dpcs in _context.DoctorsPatientsCharts on pats.PatientId equals dpcs.PatientId //醫生病人就診
            //             join chas in _context.Charts on dpcs.ChaId equals chas.ChaId //就診
            //             join docs in _context.Doctors on dpcs.DoctorId equals docs.DoctorId //醫生
            //             join emps in _context.Employees on docs.DoctorId equals emps.EmployeeId //員工
            //             select new { pres.PresNo, pats.CaseHistory, pats.PatientName, pats.Gender, pats.BirthDate, emps.EmployeeName, chas.Vdate};

            //var result = await prestb.ToListAsync();
            //return View(result);

            //var prestb = await(
            //    from pres in _context.Prescriptions //領藥
            //    join pats in _context.Patients on pres.PatientId equals pats.PatientId //病患
            //    join dpcs in _context.DoctorsPatientsCharts on pats.PatientId equals dpcs.PatientId //醫生病人就診
            //    join chas in _context.Charts on dpcs.ChaId equals chas.ChaId //就診
            //    join docs in _context.Doctors on dpcs.DoctorId equals docs.DoctorId //醫生
            //    join emps in _context.Employees on docs.DoctorId equals emps.EmployeeId //員工
            //    select new
            //    {
            //        PresId = pres.PresNo, //領藥號
            //        chId = pats.CaseHistory, //病例號
            //        PatsName = pats.PatientName, //病患姓名
            //        gender = pats.Gender, //性別
            //        birthday = pats.BirthDate, //出生年月日
            //        docsName = emps.EmployeeName, //醫生姓名
            //        Vdate = chas.Vdate //就診日期
            //    }).ToArrayAsync();


            //return View(prestb);

            IEnumerable<PresViewModel> presModel =
            await(
                from pres in _context.Prescriptions //領藥
                join pats in _context.Patients on pres.PatientId equals pats.PatientId //病患
                join dpcs in _context.DoctorsPatientsCharts on pats.PatientId equals dpcs.PatientId //醫生病人就診
                join chas in _context.Charts on dpcs.ChaId equals chas.ChaId //就診
                join docs in _context.Doctors on dpcs.DoctorId equals docs.DoctorId //醫生
                join emps in _context.Employees on docs.DoctorId equals emps.EmployeeId //員工
                where pres.PresNo == "PRE2023053013001001"
                select new PresViewModel
                {
                    PresId = pres.PresNo, //領藥號
                    chId = pats.CaseHistory, //病例號
                    PatsName = pats.PatientName, //病患姓名
                    gender = Convert.ToInt16(pats.Gender), //性別
                    birthday = pats.BirthDate, //出生年月日
                    docsName = emps.EmployeeName, //醫生姓名
                    Vdate = chas.Vdate //就診日期
                }
            ).ToListAsync();

            IEnumerable<DrugsViewModel> drugsModel =
                await(
                    from drugs in _context.Drugs //藥品
                    join CDDs in _context.ChartsDrugsDosages on drugs.DrugId equals CDDs.DrugId //就診藥品用藥頻率
                    join doss in _context.Dosages on CDDs.DosId equals doss.DosId //用藥頻率
                    join ROAs in _context.RoutesOfAdminstrations on drugs.Roaid equals ROAs.Roaid //給藥途徑
                    select new DrugsViewModel
                    {
                        DrugId = drugs.DrugId,
                        DrugName = drugs.DrugName,
                        dosId = doss.DosId,
                        bp = ROAs.BodyParts,
                        days = CDDs.Days,
                        total = CDDs.Total,
                        remark = CDDs.Remark
                    }
                
                
                ).ToListAsync();


            return View(presModel);
        }


    }
}
