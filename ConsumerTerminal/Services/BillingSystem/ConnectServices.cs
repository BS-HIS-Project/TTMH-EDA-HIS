﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;

namespace ConsumerTerminal.Services.BillingSystem
{
    public class ConnectServices
    {
        private readonly HisdbContext _context;

        public ConnectServices()
        {
            _context = new HisdbContext();
        }

        public Billing GetBilling(string PatientId)
        {
            if (ChackPatient(PatientId))
            {
                if (ChackHealthInsurance(PatientId))
                {
                    _context.Dispose();
                    return new HealthInsurance(PatientId);
                }
                else
                {
                    _context.Dispose();
                    return new NonHealthInsurance(PatientId);
                }
            }
            else
            {
                _context.Dispose();
                throw new Exception("Patient not found");
            }


        }

        private bool ChackPatient(string PatientId)
        {
            var _patient = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            if (_patient == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ChackHealthInsurance(string PatientId)
        {
            var _patient = _context.Patients.Where(p => p.PatientId == PatientId).FirstOrDefault();
            if (_patient.Status == "HealthInsurance")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
