using HISDB.Data;
using IronPdf.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.Services.PrintSystem
{
    public class PaymentSlipServices : DataProcessing
    {
        private readonly HisdbContext _context;

        private readonly List<string> _data;
        private readonly List<string> _output;
        private readonly List<string> _drugListTemp;
        private readonly List<MatchData> _match;
        private readonly List<DrugList> _drugList;

        private readonly string ChaId;
        private readonly string DrugId;
        private readonly string PatientId;
        private readonly string PresNo;
        private readonly string DetId;

        public PaymentSlipServices(string chaId, string drugId, string patientId, string presNo, string detId)
        {
            _context = new HisdbContext();

            _data = new List<string>();
            _output = new List<string>();
            _drugListTemp = new List<string>();
            _match = new List<MatchData>();
            _drugList = new List<DrugList>();

            ChaId = chaId;
            DrugId = drugId;
            PatientId = patientId;
            PresNo = presNo;
            DetId = detId;

            //InputHTML(@".\..\..\..\Forms\PaymentSlip.html");
            //setMatchData();
            //ChangeData();
            //OutputPDF(@$".\..\..\..\PDF\藥袋\PaymentSlip{patientId}{chaId}.pdf");
            
        }

        public void setMatchData()
        {
            var CDDs = _context.ChartsDrugsDosages.Where(cdd => cdd.ChaId == ChaId && cdd.DrugId == DrugId).FirstOrDefault();
            var pat = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            var pres = _context.Prescriptions.Where(p => p.PresNo == PresNo).FirstOrDefault();
            var DPCs = _context.DoctorsPatientsCharts.Where(dpc => dpc.ChaId == ChaId).FirstOrDefault();
            var Doctor = _context.Employees.Where(d => d.EmployeeId == DPCs.DoctorId).FirstOrDefault();
            var chart = _context.Charts.Where(c => c.ChaId == ChaId).FirstOrDefault();
            var drug = _context.Drugs.Where(d => d.DrugId == DrugId).FirstOrDefault();
            var dosage = _context.Dosages.Where(d => d.DosId == CDDs.DosId).FirstOrDefault();
            var pharmacy = _context.Employees.Where(p => p.EmployeeId == pres.PhaId).FirstOrDefault();

            int length = PresNo.Length;

            var PatSer = new PartialServices(PatientId);

            _match.Add(new MatchData { htmlStr = "#PresNo", pdfStr = PresNo.Substring(length - 3) });
            _match.Add(new MatchData { htmlStr = "#DetId", pdfStr = DetId.Substring(DetId.Length - 3) });
            _match.Add(new MatchData { htmlStr = "#PatientId", pdfStr = pat.PatientId.ToString() });
            _match.Add(new MatchData { htmlStr = "#BirthDate", pdfStr = DateTimeToYMD(pat.BirthDate) });
            _match.Add(new MatchData { htmlStr = "#DoctorName", pdfStr = Doctor.EmployeeName.ToString() });
            _match.Add(new MatchData { htmlStr = "#PatientName", pdfStr = pat.PatientName.ToString() });
            _match.Add(new MatchData { htmlStr = "#Age", pdfStr = PatSer.GetAge().ToString() });
            _match.Add(new MatchData { htmlStr = "#Vdate", pdfStr = DateTimeToYMD(chart.Vdate) });
            _match.Add(new MatchData { htmlStr = "#Gender", pdfStr = PatSer.GetGender() });
            _match.Add(new MatchData { htmlStr = "#object", pdfStr = chart.Object.ToString() });
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

            String line;
            line = sr.ReadLine();
            while (line != null)
            {
                _data.Add(line.ToString());
                line = sr.ReadLine();
            }

            sr.Close();
        }

        public override void OutputPDF(string FileName)
        {
            Random rnd = new Random();
            string path = @$".\..\..\..\HTML\PaymentSlip{rnd.Next(Int32.MinValue, Int32.MaxValue)}.html";

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);

            foreach (string x in _output)
            {
                sw.Write(x);
            }

            sw.Close();

            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = PdfPaperSize.A4;
            renderer.RenderingOptions.MarginTop = 33; // millimeters
            renderer.RenderingOptions.MarginBottom = 35; // millimeters
            var pdf = renderer.RenderUrlAsPdf(path);
            pdf.SaveAs(FileName);
        }

        public void setDrugListTemp()
        {
            _drugListTemp.Add("<tr>");
            _drugListTemp.Add("<td class=\"drugName\">#DrugName</td>");
            _drugListTemp.Add("<td class=\"drugId\">#DrugId</td>");
            _drugListTemp.Add("<td class=\"CDDsQty\">#CDDsQty</td>");
            _drugListTemp.Add("<td class=\"CDDsDays\">#CDDsDays</td>");
            _drugListTemp.Add("<td class=\"CDDsTotal\">#CDDsTotal</td>");
            _drugListTemp.Add("</tr>");
        }

        public void AddDrugList(string DrugName, string DrugId, string CDDsQty, 
                                string CDDsDays, string CDDsTotal)
        {
            _drugList.Add(new DrugList { 
                DrugName = DrugName, 
                DrugId = DrugId, 
                CDDsQty = CDDsQty, 
                CDDsDays = CDDsDays, 
                CDDsTotal = CDDsTotal });
        }

        private class DrugList
        {
            public string DrugName { get; set; }
            public string DrugId { get; set; }
            public string CDDsQty { get; set; }
            public string CDDsDays { get; set; }
            public string CDDsTotal { get; set; }
        }
    }
}
