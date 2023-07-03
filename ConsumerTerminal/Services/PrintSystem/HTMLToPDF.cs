using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTerminal.Services.PrintSystem
{
    public abstract class HTMLToPDF
    {
        public abstract void InputHTML(string FileName);
        public abstract void OutputPDF(string FileName);
    }
}
