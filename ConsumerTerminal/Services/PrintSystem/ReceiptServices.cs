﻿using HISDB.Data;
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
        private readonly HisdbContext _context;

        private readonly List<string> _data;
        private readonly List<string> _output;
        private readonly List<MatchData> _match;

        private readonly string ChaId;
        private readonly string DrugId;
        private readonly string PatientId;
        private readonly string DetId;

        public ReceiptServices(string chaId, string drugId, string patientId, string detId)
        {
            _context = new HisdbContext();

            _data = new List<string>();
            _output = new List<string>();
            _match = new List<MatchData>();

            ChaId = chaId;
            DrugId = drugId;
            PatientId = patientId;
            DetId = detId;
        }

        public void setMatchData()
        {
            var CDDs = _context.ChartsDrugsDosages.Where(cdd => cdd.ChaId == ChaId && cdd.DrugId == DrugId).FirstOrDefault();
            var pat = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            var DPCs = _context.DoctorsPatientsCharts.Where(dpc => dpc.ChaId == ChaId).FirstOrDefault();
            var Doctor = _context.Employees.Where(d => d.EmployeeId == DPCs.DoctorId).FirstOrDefault();
            var chart = _context.Charts.Where(c => c.ChaId == ChaId).FirstOrDefault();
            var drug = _context.Drugs.Where(d => d.DrugId == DrugId).FirstOrDefault();
            var dosage = _context.Dosages.Where(d => d.DosId == CDDs.DosId).FirstOrDefault();
            var detail = (
                from dt in _context.Details
                where dt.PatientId == pat.PatientId
                orderby dt.PaymentTime descending
                select dt
            ).FirstOrDefault();
            var cashierName = (
                from em in _context.Employees
                where em.EmployeeId==detail.CasId
                select em.EmployeeName
            ).FirstOrDefault();


            var PatSer = new PartialServices(PatientId);

            _match.Add(new MatchData { htmlStr = "#CaseHistory", pdfStr = pat.CaseHistory });
            _match.Add(new MatchData { htmlStr = "#PatientName", pdfStr = pat.PatientName });
            _match.Add(new MatchData { htmlStr = "#Gender", pdfStr = PatSer.GetGender() });
            _match.Add(new MatchData { htmlStr = "#BirthDate", pdfStr = pat.BirthDate.ToString("yyyy/MM/dd") });
            _match.Add(new MatchData { htmlStr = "#Age", pdfStr = PatSer.GetAge().ToString() });
            _match.Add(new MatchData { htmlStr = "#DoctorName", pdfStr = Doctor.EmployeeName});
            _match.Add(new MatchData { htmlStr = "#VDate", pdfStr = chart.Vdate.ToString("yyyy/MM/dd")});
            _match.Add(new MatchData { htmlStr = "#Registration", pdfStr = detail.Registration.ToString()});
            _match.Add(new MatchData { htmlStr = "#MedicalCost", pdfStr = detail.MedicalCost.ToString()});
            _match.Add(new MatchData { htmlStr = "#PartialPayment", pdfStr = detail.PartialPayment.ToString()});
            _match.Add(new MatchData { htmlStr = "#Diagnostic", pdfStr = detail.Diagnostic.ToString()});
            _match.Add(new MatchData { htmlStr = "#DrugPartialPayment", pdfStr =  detail.DrugPartialPayment.ToString()});
            _match.Add(new MatchData { htmlStr = "#Payable", pdfStr =  detail.Payable.ToString()});
            _match.Add(new MatchData { htmlStr = "#DetID", pdfStr =  detail.DetId});
            _match.Add(new MatchData { htmlStr = "#CashierName", pdfStr =  cashierName});
            _match.Add(new MatchData { htmlStr = "#PaymentTime", pdfStr = detail.PaymentTime?.ToString("yyyy/MM/dd")});
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
            string path = @$".\..\..\..\HTML\Receipt{rnd.Next(Int32.MinValue, Int32.MaxValue)}.html";

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
    }
}
