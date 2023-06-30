using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;

namespace ConsumerTerminal.Services.BillingSystem
{
    public abstract class Billing
    {

        // 掛號費
        public abstract decimal RegistrationFee();
        // 藥費
        public abstract decimal DrugFee();
        // 部分負擔費
        public abstract decimal PartialPayment();
        // 診察費
        public abstract decimal DiagnosticFee();
    }


}
