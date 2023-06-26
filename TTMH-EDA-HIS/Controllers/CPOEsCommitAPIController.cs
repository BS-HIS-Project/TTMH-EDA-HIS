using HISDB.Data;
using HISDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TTMH_EDA_HIS.ViewModels;
using System.Text.Json;
using System.Text;

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
            Chart chart = new Chart();

            try
            {
                doctor = await _context.Doctors.FindAsync(vm.DoctorID);
                patient = await _context.Patients.FindAsync(vm.PatientID);
                if (patient == null)
                {
                    return Ok(new
                    {
                        Icon = "question",
                        Title = "Not Found",
                        Text = "Patient Not Found",
                        Details = ""
                    });
                }
                if (doctor == null)
                {
                    return Ok(new
                    {
                        Icon = "question",
                        Title = "Not Found",
                        Text = "Doctor Not Found",
                        Details = ""
                    });
                }



                string yearStr = DateTime.Now.Year.ToString();
                string monthStr = TwoUnitNumericString(DateTime.Now.Month);
                string dayStr = TwoUnitNumericString(DateTime.Now.Day);
                string hourStr = TwoUnitNumericString(DateTime.Now.Hour);
                string minStr = TwoUnitNumericString(DateTime.Now.Minute);

                string cid = $"CHA{yearStr}{monthStr}{dayStr}{hourStr}{minStr}";
                chart = new Chart()
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
                    };
                    await _context.ChartsDrugsDosages.AddAsync(cdd);
                }

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    Icon = "error",
                    Title = "Error",
                    Text = ex.Message,
                    Details = ex.InnerException.Message
                });
            }
            try
            {
                CPOEsCommitAPIPrescriptionViewModel pvm = new CPOEsCommitAPIPrescriptionViewModel()
                {
                    Topic = "",
                    Key = "",
                    Message = new CPOEsCommitAPIPrescriptionViewModel_DoctorMessage()
                    {
                        DoctorId = vm.DoctorID,
                        PatientId = vm.PatientID,
                        ChaId = chart.ChaId,
                        ChartsDrugsDosages = new List<CPOEsCommitAPIPrescriptionViewModel_ChartsDrugsDosage>()
                    }
                };
                foreach (CPOEsCommitAPICommitDrugsViewModel_Drugs i in vm.Drugs)
                {
                    pvm.Message.ChartsDrugsDosages.Add(new CPOEsCommitAPIPrescriptionViewModel_ChartsDrugsDosage()
                    {
                        DrugId = i.DrugID,
                        DosId = i.DosID,
                        Quantity = double.Parse(i.Quantity),
                        Days = int.Parse(i.Days),
                        Total = int.Parse(i.Total),
                        Remark = i.Remark
                    });
                }

                string response = await PrintPrescription(pvm);
                if (response == null) { 
                    throw new Exception("Connection Failed"); 
                }

                return Ok(new
                {
                    Icon = "success",
                    Title = "成功上存",
                    Text = response,
                    Details = ""
                });
            }
            catch (Exception ex) 
            {
                return Ok(new
                {
                    Icon = "warning",
                    Title = "Warning",
                    Text = "Database Inserted Success but failed to generate prescription",
                    Details = ex.Message + "\n" + ex.InnerException.Message
                });
            }
        }

        private async Task<string> PrintPrescription(CPOEsCommitAPIPrescriptionViewModel vm)
        {
            try
            {
                string? responseJsonStr = null;
                string postJsonStr = JsonSerializer.Serialize(vm);

                StringContent content = new StringContent(postJsonStr, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync("url", content);

                if (response.IsSuccessStatusCode)
                {
                    responseJsonStr = await response.Content.ReadAsStringAsync();
                }
                return responseJsonStr;
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
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
