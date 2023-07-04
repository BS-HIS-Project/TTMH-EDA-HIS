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

        private List<DrugList> _drugList;

        private readonly string ChaId;
        private readonly string PatientId;
        private readonly string PresNo;
        private readonly string DetId;

        public PaymentSlipServices(string chaId, string patientId, string presNo, string detId)
        {
            _context = new HisdbContext();

            _data = new List<string>();
            _output = new List<string>();
            _drugListTemp = new List<string>();
            _match = new List<MatchData>();
            _drugList = new List<DrugList>();

            ChaId = chaId;
            PatientId = patientId;
            PresNo = presNo;
            DetId = detId;

            setDrugListTemp();
        }

        public void setMatchData()
        {
            var pat = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            var pres = _context.Prescriptions.Where(p => p.PresNo == PresNo).FirstOrDefault();
            var DPCs = _context.DoctorsPatientsCharts.Where(dpc => dpc.ChaId == ChaId).FirstOrDefault();
            var Doctor = _context.Employees.Where(d => d.EmployeeId == DPCs.DoctorId).FirstOrDefault();
            var chart = _context.Charts.Where(c => c.ChaId == ChaId).FirstOrDefault();
            var pharmacy = _context.Employees.Where(p => p.EmployeeId == pres.PhaId).FirstOrDefault();
            var det = _context.Details.Where(d => d.DetId == DetId).FirstOrDefault();

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
            _match.Add(new MatchData { htmlStr = "#Total", pdfStr = det.Payable.ToString() });
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

                if (s.Contains("#DrugList"))
                {
                    setDrugListToHTML();
                }
                else 
                {
                    foreach (MatchData m in _match)
                    {
                        if (s.Contains(m.htmlStr))
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
            string path = @$".\..\..\..\HTML\PaymentSlip{rnd.Next(Int32.MinValue, Int32.MaxValue)}.html";

            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);

            foreach (string x in _output)
            {
                sw.Write(x);
            }

            sw.Close();
            sw.Dispose();

            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = PdfPaperSize.A4;
            renderer.RenderingOptions.MarginTop = 33; // millimeters
            renderer.RenderingOptions.MarginBottom = 35; // millimeters
            var pdf = renderer.RenderUrlAsPdf(path);
            pdf.SaveAs(FileName);
        }

        public void setDrugList()
        {
            _drugList = new List<DrugList>();
        }

        private void setDrugListTemp()
        {
            _drugListTemp.Add("<tr>");
            _drugListTemp.Add("<td class=\"drugName\">#DrugName</td>");
            _drugListTemp.Add("<td class=\"dosId\">#DosId</td>");
            _drugListTemp.Add("<td class=\"CDDsQty\">#CDDsQty</td>");
            _drugListTemp.Add("<td class=\"CDDsDays\">#CDDsDays</td>");
            _drugListTemp.Add("<td class=\"CDDsTotal\">#CDDsTotal</td>");
            _drugListTemp.Add("</tr>");
        }

        private void setDrugListToHTML()
        {
            foreach(var drug in _drugList)
            {
                foreach(var temp in _drugListTemp)
                {
                    string temp2 = temp;

                    if(temp2.Contains("#DrugName"))
                        temp2 = temp2.Replace("#DrugName", drug.DrugName);
                    else if(temp2.Contains("#DosId"))
                        temp2 = temp2.Replace("#DosId", drug.DosId);
                    else if(temp2.Contains("#CDDsQty"))
                        temp2 = temp2.Replace("#CDDsQty", drug.CDDsQty);
                    else if(temp2.Contains("#CDDsDays"))
                        temp2 = temp2.Replace("#CDDsDays", drug.CDDsDays);
                    else if(temp2.Contains("#CDDsTotal"))
                        temp2 = temp2.Replace("#CDDsTotal", drug.CDDsTotal);

                    _output.Add(temp2);
                }
            }
        }

        public void AddDrugList(string DrugId)
        {
            var CDDs = _context.ChartsDrugsDosages.Where(cdd => cdd.ChaId == ChaId && cdd.DrugId == DrugId).FirstOrDefault();
            var DrugName = _context.Drugs.Where(d => d.DrugId == DrugId).FirstOrDefault().DrugName;

            if (CDDs == null)
            {
                new Exception("CDDs is null");
            } 
            else if (DrugName == null)
            {
                new Exception("DrugName is null");
            } 
            else
            {
                _drugList.Add(new DrugList
                {
                    DrugName = DrugName,
                    DosId = CDDs.DosId,
                    CDDsQty = CDDs.Quantity.ToString(),
                    CDDsDays = CDDs.Days.ToString(),
                    CDDsTotal = CDDs.Total.ToString()
                });
            }
        }

        private class DrugList
        {
            public string DrugName { get; set; }
            public string DosId { get; set; }
            public string CDDsQty { get; set; }
            public string CDDsDays { get; set; }
            public string CDDsTotal { get; set; }
        }

        public void close()
        {
            _context.Dispose();
        }
    }
}
