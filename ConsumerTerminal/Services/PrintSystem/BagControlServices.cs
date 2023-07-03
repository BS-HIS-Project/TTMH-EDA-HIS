using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;
using ConsumerTerminal.ViewModels;
using System.Security.Claims;
using System.Runtime;

namespace ConsumerTerminal.Services.PrintSystem
{
    public class BagControlServices
    {
        private readonly HisdbContext _context;
        private readonly DoctorMessage doctorMessage;

        private readonly string PresNo;
        private readonly string DetId;

        public BagControlServices(DoctorMessage doctorMessage, string presNo, string detId)
        {
            _context = new HisdbContext();

            this.doctorMessage = doctorMessage;
            PresNo = presNo;
            DetId = detId;
        }

        public void run()
        {
            int k = 0;
            foreach (var item in doctorMessage.ChartsDrugsDosages)
            {
                var MedicineBagSer = new MedicineBagServices(doctorMessage.ChaId,
                                                            item.DrugId, 
                                                            doctorMessage.PatientId, 
                                                            PresNo);
                MedicineBagSer.InputHTML(@".\..\..\..\Forms\MedicineBag.html");
                MedicineBagSer.setMatchData();
                MedicineBagSer.ChangeData();
                MedicineBagSer.OutputPDF(@$".\..\..\..\PDF\藥袋\MedicineBag{doctorMessage.PatientId}{doctorMessage.PatientId}-{k++}.pdf");
            }

            
        }
    }
}
