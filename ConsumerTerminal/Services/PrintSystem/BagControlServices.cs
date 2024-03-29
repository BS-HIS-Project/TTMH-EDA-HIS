﻿using System;
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
    public class BagControlServices : IDisposable
    {
        private readonly HisdbContext _context;

        private DoctorMessage doctorMessage;
        private MedicineBagServices MedicineBagSer;
        private PaymentSlipServices PaymentSlipSer;
        private PrescriptionServices PrescriptionSer;
        private ReceiptServices ReceiptSer;

        private readonly string PresNo;
        private readonly string DetId;

        public BagControlServices(DoctorMessage doctorMessage, string presNo, string detId)
        {
            _context = new HisdbContext();

            this.doctorMessage = doctorMessage;
            PresNo = presNo;
            DetId = detId;
        }

        public void Dispose()
        {
            doctorMessage = null;
        }

        public void run(int PrintType)
        {
            _context.Dispose();

            string path = @$".\..\..\..\PDF\{doctorMessage.PatientId}{doctorMessage.ChaId}";

            CreateDirectory(path);

            if (PrintType == 1)
            {
                int k = 0;
                foreach (var item in doctorMessage.ChartsDrugsDosages)
                {
                    MedicineBagSer = new MedicineBagServices(doctorMessage.ChaId,
                                                            item.DrugId,
                                                            doctorMessage.PatientId,
                                                            PresNo);
                    MedicineBagSer.InputHTML(@".\..\..\..\Forms\MedicineBag.html");
                    MedicineBagSer.setMatchData();
                    MedicineBagSer.ChangeData();
                    MedicineBagSer.OutputPDF(@$"{path}\MedicineBag{doctorMessage.PatientId}{doctorMessage.PatientId}-{k++}.pdf");
                    MedicineBagSer.close();

                    Console.WriteLine(@$"MedicineBag{doctorMessage.PatientId}{doctorMessage.PatientId}-{k-1}.pdf 已列印");
                }

                PrescriptionSer = new PrescriptionServices(doctorMessage.ChaId, doctorMessage.PatientId, PresNo, DetId);
                PrescriptionSer.setDrugList();

                foreach (var item in doctorMessage.ChartsDrugsDosages)
                {
                    PrescriptionSer.AddDrugList(item.DrugId);
                }

                PrescriptionSer.InputHTML(@".\..\..\..\Forms\Prescription.html");
                PrescriptionSer.setMatchData();
                PrescriptionSer.ChangeData();
                PrescriptionSer.OutputPDF(@$"{path}\Prescription{PresNo}{DetId}.pdf");
                PrescriptionSer.close();

                Console.WriteLine(@$"Prescription{PresNo}{DetId}.pdf 已列印");
            }
            else if (PrintType == 2)
            {
                PaymentSlipSer = new PaymentSlipServices(doctorMessage.ChaId, doctorMessage.PatientId, PresNo, DetId);
                PaymentSlipSer.setDrugList();

                foreach (var item in doctorMessage.ChartsDrugsDosages)
                {
                    PaymentSlipSer.AddDrugList(item.DrugId);
                }

                PaymentSlipSer.InputHTML(@".\..\..\..\Forms\PaymentSlip.html");
                PaymentSlipSer.setMatchData();
                PaymentSlipSer.ChangeData();
                PaymentSlipSer.OutputPDF(@$"{path}\PaymentSlip{PresNo}{DetId}.pdf");
                PaymentSlipSer.close();

                Console.WriteLine(@$"PaymentSlip{PresNo}{DetId}.pdf 已列印");
            }


            

            //ReceiptSer = new ReceiptServices(doctorMessage.ChaId, doctorMessage.PatientId, DetId);
            //ReceiptSer.InputHTML(@".\..\..\..\Forms\Receipt.html");
            //ReceiptSer.setMatchData();
            //ReceiptSer.ChangeData();
            //ReceiptSer.OutputPDF(@$"{path}\Receipt{PresNo}{DetId}.pdf");
            //ReceiptSer.close();
        }

        private void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
 