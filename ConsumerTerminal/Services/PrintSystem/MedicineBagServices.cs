using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;
using Confluent.Kafka;
using IronPdf.Rendering;
using Microsoft.SqlServer.Server;

namespace ConsumerTerminal.Services.PrintSystem
{
    public class MedicineBagServices : DataProcessing
    {
        private readonly HisdbContext _context;

        private readonly List<string> _data;
        private readonly List<string> _output;
        private readonly List<MatchData> _match;

        private readonly string ChaId;
        private readonly string DrugId;
        private readonly string PatientId;
        private readonly string PresNo;

        public MedicineBagServices(string chaId,string drugId, string patientId, string presNo)
        {
            _context = new HisdbContext();

            _data = new List<string>();
            _output = new List<string>();
            _match = new List<MatchData>();
            
            ChaId = chaId;
            DrugId = drugId;
            PatientId = patientId;
            PresNo = presNo;
        }

        public void setMatchData()
        {
            var CDDs = _context.ChartsDrugsDosages.Find(ChaId, DrugId);
            var pat = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            var pres = _context.Prescriptions.Where(p => p.PresNo == PresNo).FirstOrDefault();
            var DPCs = _context.DoctorsPatientsCharts.Where(dpc => dpc.ChaId == ChaId).FirstOrDefault();
            var Doctor = _context.Employees.Where(d => d.EmployeeId == DPCs.DoctorId).FirstOrDefault();
            var chart = _context.Charts.Where(c => c.ChaId == ChaId).FirstOrDefault();
            var drug = _context.Drugs.Where(d => d.DrugId == DrugId).FirstOrDefault();
            Dosage? dosage = _context.Dosages.Where(d => d.DosId == CDDs.DosId).FirstOrDefault();
            var pharmacy = _context.Employees.Where(p => p.EmployeeId == pres.PhaId).FirstOrDefault();

            int length = PresNo.Length;

            var PatSer = new PartialServices(PatientId);

            _match.Add(new MatchData { htmlStr = "#PresNo", pdfStr = PresNo });
            _match.Add(new MatchData { htmlStr = "#PatientId", pdfStr = pat.PatientId.ToString() });
            _match.Add(new MatchData { htmlStr = "#BirthDate", pdfStr = DateTimeToYMD(pat.BirthDate) });
            _match.Add(new MatchData { htmlStr = "#DoctorName", pdfStr = Doctor.EmployeeName.ToString() });
            _match.Add(new MatchData { htmlStr = "#PatientName", pdfStr = pat.PatientName.ToString() });
            _match.Add(new MatchData { htmlStr = "#PatientYear", pdfStr = PatSer.GetAge().ToString() });
            _match.Add(new MatchData { htmlStr = "#Vdate", pdfStr = DateTimeToYMD(chart.Vdate) });
            _match.Add(new MatchData { htmlStr = "#Gender", pdfStr = PatSer.GetGender() });
            _match.Add(new MatchData { htmlStr = "#DrugName", pdfStr = drug.DrugName.ToString() });
            _match.Add(new MatchData { htmlStr = "#Appearance", pdfStr = drug.Appearance.ToString() });
            _match.Add(new MatchData { htmlStr = "#GenericName", pdfStr = drug.GenericName.ToString() });
            _match.Add(new MatchData { htmlStr = "#Dcontent", pdfStr = drug.Dcontent.ToString() });
            _match.Add(new MatchData { htmlStr = "#DosageFreq", pdfStr = dosage.Freq.ToString() });
            _match.Add(new MatchData { htmlStr = "#CDDsQty", pdfStr = CDDs.Quantity.ToString() });
            _match.Add(new MatchData { htmlStr = "#CDDsDays", pdfStr = CDDs.Days.ToString() });
            _match.Add(new MatchData { htmlStr = "#DrugWarningPrecautions", pdfStr = drug.WarningPrecautions.ToString() });
            _match.Add(new MatchData { htmlStr = "#DrugClinicalUses", pdfStr = drug.ClinicalUses.ToString() });
            _match.Add(new MatchData { htmlStr = "#DrugAdverseReactions", pdfStr = drug.AdverseReactions.ToString() });
            _match.Add(new MatchData { htmlStr = "#CDDsTotal", pdfStr = CDDs.Total.ToString() });
            _match.Add(new MatchData { htmlStr = "#PhaName", pdfStr = pharmacy.EmployeeName.ToString() });
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
            string path = @$".\..\..\..\HTML\MedicineBag{rnd.Next(Int32.MinValue, Int32.MaxValue)}.html";

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
        public void close()
        {
            _context.Dispose(); 
        }
    }
}
