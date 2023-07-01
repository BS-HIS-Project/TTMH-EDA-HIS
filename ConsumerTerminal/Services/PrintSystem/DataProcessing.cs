using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.Services.PrintSystem
{
    public abstract class DataProcessing : HTMLToPDF
    {
        public abstract void ChangeData();
    }
}
