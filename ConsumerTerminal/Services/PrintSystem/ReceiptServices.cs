using ConsumerTerminal.ViewModels;
using HISDB.Data;
using HISDB.Models;
using IronPdf.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.Services.PrintSystem
{
    public class ReceiptServices : DataProcessing
    {
        private HisdbContext _context;

        private readonly List<string> _data;
        private readonly List<string> _output;
        private readonly List<MatchData> _match;

        //private readonly string ChaId;
        //private readonly string PatientId;
        //private readonly string DetId;

        private readonly DetailIdViewModel detailIdViewModel;

        public ReceiptServices(DetailIdViewModel detailIdViewModel)
        {
            _context = new HisdbContext();

            _data = new List<string>();
            _output = new List<string>();
            _match = new List<MatchData>();

            //ChaId = chaId;
            //PatientId = patientId;
            //DetId = detId;
            this.detailIdViewModel = detailIdViewModel;
        }

        public void setMatchData()
        {
            //var CDDs = _context.ChartsDrugsDosages.Where(cdd => cdd.ChaId == ChaId && cdd.DrugId == DrugId).FirstOrDefault();
            var pat = _context.Patients.Where(p => p.PatientId == detailIdViewModel.PatientId).FirstOrDefault();
            //var DPCs = _context.DoctorsPatientsCharts.Where(dpc => dpc.ChaId == ChaId).FirstOrDefault();
            //var Doctor = _context.Employees.Where(d => d.EmployeeId == DPCs.DoctorId).FirstOrDefault();
            //var chart = _context.Charts.Where(c => c.ChaId == ChaId).FirstOrDefault();
            //var dosage = _context.Dosages.Where(d => d.DosId == CDDs.DosId).FirstOrDefault();
            var detail = _context.Details.Where(d => d.DetId == detailIdViewModel.DetId).FirstOrDefault();
            var cashierName = _context.Employees.Where(e => e.EmployeeId == detail.CasId).FirstOrDefault().EmployeeName;
            //var cashierName = "";


            var PatSer = new PartialServices(detailIdViewModel.PatientId);

            _match.Add(new MatchData { htmlStr = "#CaseHistory", pdfStr = pat.PatientId.ToString() });
            _match.Add(new MatchData { htmlStr = "#PatientName", pdfStr = pat.PatientName.ToString() });
            _match.Add(new MatchData { htmlStr = "#Gender", pdfStr = PatSer.GetGender() });
            _match.Add(new MatchData { htmlStr = "#BirthDate", pdfStr = DateTimeToYMD(pat.BirthDate) });
            _match.Add(new MatchData { htmlStr = "#Age", pdfStr = PatSer.GetAge().ToString() });
            _match.Add(new MatchData { htmlStr = "#DoctorName", pdfStr = detailIdViewModel.DoctorName ?? "" });
            _match.Add(new MatchData { htmlStr = "#VDate", pdfStr = DateTimeToYMD( DateTime.Parse(detailIdViewModel.Vdate))});
            
            _match.Add(new MatchData { htmlStr = "#Registration", pdfStr = detail.Registration.ToString() ?? ""});
            _match.Add(new MatchData { htmlStr = "#MedicalCost", pdfStr = detail.MedicalCost.ToString() });
            _match.Add(new MatchData { htmlStr = "#PartialPayment", pdfStr = detail.PartialPayment.ToString() ?? ""});
            _match.Add(new MatchData { htmlStr = "#Diagnostic", pdfStr = detail.Diagnostic.ToString() ?? ""});
            _match.Add(new MatchData { htmlStr = "#DrugPartialPayment", pdfStr = detail.DrugPartialPayment.ToString() ?? ""});
            _match.Add(new MatchData { htmlStr = "#Payable", pdfStr = detail.Payable.ToString() });
            _match.Add(new MatchData { htmlStr = "#DetID", pdfStr = detailIdViewModel.DetId.Substring(detailIdViewModel.DetId.Length - 3).ToString() });
            _match.Add(new MatchData { htmlStr = "#CashierName", pdfStr = cashierName.ToString() });
            _match.Add(new MatchData { htmlStr = "#PaymentTime", pdfStr = detail.PaymentTime.ToString() ?? "".ToString() });

            _match.Add(new MatchData { htmlStr = "#ReceiptType", pdfStr = PatSer.IsHealthInsurance() == true ? "健保" : "非健保" });

        }

        private static string DateTimeToYMD(DateTime dateTime)
        {
            var year = dateTime.ToString("yyyy");
            var month = dateTime.ToString("MM");
            var day = dateTime.ToString("dd");

            return $"{year}/{month}/{day}";
        }

        public override void ChangeData()
        {
            var s_k = 0;
            foreach (string s in _data)
            {
                string temp = s;

                foreach (MatchData m in _match)
                {
                    if (s.Contains(m.htmlStr))
                    {
                        temp = s.Replace(m.htmlStr, m.pdfStr);
                    }
                }

                _output.Add(temp);
                s_k++;
            }
        }

        public override void InputHTML(string FileName)
        {
            StreamReader sr = new StreamReader(FileName, Encoding.UTF8);

            string? line = null;
            line = sr.ReadLineAsync().Result;
            while (line != null)
            {
                _data.Add(line.ToString());
                line = sr.ReadLine();
            }

            sr.Close();
            sr.Dispose();
        }

        public override void OutputPDF(string FileName)
        {
            Random rnd = new Random();
            string path = @$".\..\..\..\HTML\Receipt{rnd.Next(Int32.MinValue, Int32.MaxValue)}.html";

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);

            foreach (string x in _output)
            {
                sw.Write(x);
            }

            sw.Close();

            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = PdfPaperSize.A4Rotated;
            renderer.RenderingOptions.MarginTop = 0; // millimeters
            renderer.RenderingOptions.MarginBottom = 0; // millimeters
            var pdf = renderer.RenderUrlAsPdf(path);
            pdf.SaveAs(FileName);
        }

        public void close()
        {
            _context.Dispose();
        }
    }
}
