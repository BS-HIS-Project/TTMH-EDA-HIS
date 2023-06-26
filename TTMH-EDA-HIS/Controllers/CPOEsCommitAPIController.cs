using HISDB.Data;
using HISDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TTMH_EDA_HIS.ViewModels;

namespace TTMH_EDA_HIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CPOEsCommitAPIController : ControllerBase
    {
        private readonly HisdbContext _context;
        public CPOEsCommitAPIController(HisdbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> CommitDrugs(CPOEsCommitAPICommitDrugsViewModel vm)
        {
            Doctor? doctor = new Doctor();
            Patient? patient = new Patient();

            try
            {
                doctor = await _context.Doctors.FindAsync(vm.DoctorID);
                patient = await _context.Patients.FindAsync(vm.PatientID);
                if (patient == null)
                {
                    return Ok(new
                    {
                        Status = "NotFound",
                        Message = "Patient Not Found"
                    });
                }
                if (doctor == null)
                {
                    return Ok(new
                    {
                        Status = "NotFound",
                        Message = "Doctor Not Found"
                    });
                }



                string yearStr = DateTime.Now.Year.ToString();
                string monthStr = TwoUnitNumericString(DateTime.Now.Month);
                string dayStr = TwoUnitNumericString(DateTime.Now.Day);
                string hourStr = TwoUnitNumericString(DateTime.Now.Hour);
                string minStr = TwoUnitNumericString(DateTime.Now.Minute);

                string cid = $"CHA{yearStr}{monthStr}{dayStr}{hourStr}{minStr}";
                Chart chart = new Chart()
                {
                    ChaId = cid,
                    DepartmentName = doctor.DepartmentName ?? "",
                    Vdate = DateTime.Now,
                    Subject = vm.Subject,
                    Object = vm.Object,
                    History = ""
                };
                await _context.Charts.AddAsync(chart);



                DoctorsPatientsChart dpc = new DoctorsPatientsChart()
                {
                    PatientId = patient.PatientId,
                    ChaId = chart.ChaId,
                    DoctorId = doctor.DoctorId
                };
                await _context.DoctorsPatientsCharts.AddAsync(dpc);

                foreach (CPOEsCommitAPICommitDrugsViewModel_Drugs i in vm.Drugs)
                {
                    ChartsDrugsDosage cdd = new ChartsDrugsDosage()
                    {
                        ChaId = chart.ChaId,
                        DrugId = i.DrugID,
                        DosId = i.DosID,
                        Days = int.Parse(i.Days),
                        Total = double.Parse(i.Total),
                        Remark = i.Remark,
                        Quantity = double.Parse(i.Quantity)
                        //Freq = int.Parse(i.Freq)
                    };
                    await _context.ChartsDrugsDosages.AddAsync(cdd);
                }

                await _context.SaveChangesAsync();


                return Ok(new
                {
                    Status = "Success",
                    Message = ""
                });
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Status = "Error",
                    Message = ex.Message,
                    Details = ex.InnerException.ToString()
                });
            }
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> GeneratePrescription()
        {



            return Ok(new{
                Status = "Success",
                Message = ""
            });
        }

        private string TwoUnitNumericString(int value)
        {
            string result = "";
            if (value >= 0 && value < 10)
            {
                result = "0" + value.ToString();
            }
            else
            {
                result = value.ToString();
            }
            return result;
        }
    }
}
