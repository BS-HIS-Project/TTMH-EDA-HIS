using HISDB.Data;
using HISDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using TTMH_EDA_HIS.ViewModels;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TTMH_EDA_HIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Doctor")]
    public class CPOEsCommitAPIController : ControllerBase
    {
        private readonly HisdbContext _context;
        public CPOEsCommitAPIController(HisdbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Doctor")]
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
                        Title = "找不到病人資料",
                        Text = "Patient Not Found",
                        Details = ""
                    });
                }
                if (doctor == null)
                {
                    return Ok(new
                    {
                        Icon = "question",
                        Title = "找不到醫生資料",
                        Text = "Doctor Not Found",
                        Details = ""
                    });
                }

                string LastChaID = await (
                    from i in _context.Charts
                    where (i.Vdate.Year==DateTime.Now.Year && i.Vdate.DayOfYear==DateTime.Now.DayOfYear)
                    orderby i.Vdate descending
                    select i.ChaId
                ).MaxAsync();
                int LastIndex;
                if (LastChaID == null) { 
                    LastIndex = 0;
                }
                else
                {
                    LastIndex = int.Parse(LastChaID.Substring(LastChaID.Length - 3));
                }

                string yearStr = DateTime.Now.Year.ToString();
                string monthStr = DateTime.Now.Month.ToString().PadLeft(2,'0');
                string dayStr = DateTime.Now.Day.ToString().PadLeft(2,'0');
                string OPDStr = "13001"; //Out‑Patient Departments (OPD)
                string currentIndex = (++LastIndex).ToString().PadLeft(3,'0');

                string cid = $"CHA{yearStr}{monthStr}{dayStr}{OPDStr}{currentIndex}";
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
                    if(i.Remark == "")
                    {
                        i.Remark = "無";
                    }
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
                string eex= "";
                if (ex.Message == "An error occurred while saving the entity changes. See the inner exception for details.") 
                { eex += ex.InnerException.Message; }
                return Ok(new
                {
                    Icon = "error",
                    Title = "資料錯誤",
                    Text = $"{ex.Message}\n\n{eex}",
                    Details = eex
                });
            }
            try
            {
                CPOEsCommitAPIPrescriptionViewModel pvm = new CPOEsCommitAPIPrescriptionViewModel()
                {
                    Topic = "my-topic",
                    Key = "string",
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
                    if (i.Remark == "")
                    {
                        i.Remark = "無";
                    }
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
                else if (response != "")
                {
                    throw new Exception("");
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
                string eex = "";
                if (ex.Message == "An error occurred while saving the entity changes. See the inner exception for details.")
                { eex += ex.InnerException.Message; }
                return Ok(new
                {
                    Icon = "warning",
                    Title = "警告",
                    Text = "成功上存 , 但列印失敗\n\n" + ex.Message,
                    Details = eex
                });
            }
        }
		[Authorize(Roles = "Doctor")]
		[HttpPut("[action]")]
		public async Task<IActionResult> ReprintPrescription(string ChaID)
        {
            try
            {
                Chart? chart = await _context.Charts.FindAsync(ChaID);
                if(chart == null)
                {
                    return BadRequest();
                }
                DoctorsPatientsChart? dpc = await _context.DoctorsPatientsCharts.FirstOrDefaultAsync(x => x.ChaId == ChaID);
                ChartsDrugsDosage[] cdds = await (from d in _context.ChartsDrugsDosages where d.ChaId==ChaID select d).ToArrayAsync();
				CPOEsCommitAPIPrescriptionViewModel pvm = new CPOEsCommitAPIPrescriptionViewModel()
				{
					Topic = "my-topic",
					Key = "string",
					Message = new CPOEsCommitAPIPrescriptionViewModel_DoctorMessage()
					{
						DoctorId = dpc.DoctorId,
						PatientId = dpc.PatientId,
						ChaId = chart.ChaId,
						ChartsDrugsDosages = new List<CPOEsCommitAPIPrescriptionViewModel_ChartsDrugsDosage>()
					}
				};
				foreach (ChartsDrugsDosage i in cdds)
				{
					if (i.Remark == "")
					{
						i.Remark = "無";
					}
					pvm.Message.ChartsDrugsDosages.Add(new CPOEsCommitAPIPrescriptionViewModel_ChartsDrugsDosage()
					{
						DrugId = i.DrugId,
						DosId = i.DosId,
						Quantity = (double)i.Quantity,
						Days = (int)i.Days,
						Total = (int)i.Total,
						Remark = i.Remark
					});
				}
                string? response = await PrintPrescription(pvm);

                if (response == null)
                {
                    throw new Exception("Connection Failed");
                }
                else if (response != "")
                {
                    throw new Exception("");
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
				string eex = "";
				if (ex.Message == "An error occurred while saving the entity changes. See the inner exception for details.")
				{ eex += ex.InnerException.Message; }
				return Ok(new
				{
					Icon = "error",
					Title = "列印失敗",
					Text = $"{ex.Message}\n\n{eex}",
					Details = eex
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
                HttpResponseMessage response = await client.PostAsync("http://server.nicklu89.com/api/KafkaProducerDoctor", content);

                if (response.IsSuccessStatusCode)
                {
                    responseJsonStr = await response.Content.ReadAsStringAsync();
                }
                return responseJsonStr;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
