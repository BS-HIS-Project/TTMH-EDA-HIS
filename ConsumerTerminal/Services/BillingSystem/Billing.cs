using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;
using HISDB.Data;

namespace ConsumerTerminal.Services
{
    public abstract class Billing
    {

        // 掛號費
        public abstract Decimal RegistrationFee();
        // 藥費
        public abstract Decimal DrugFee();
        // 部分負擔費
        public abstract Decimal PartialPayment();
        // 診察費
        public abstract Decimal DiagnosticFee();
    }


}
