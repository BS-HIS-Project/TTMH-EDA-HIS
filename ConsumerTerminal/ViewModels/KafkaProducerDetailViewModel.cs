using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HISDB.Models;

namespace ConsumerTerminal.ViewModels
{
    public class DetailIdViewModel
    {
        public string DetId { get; set; } = null!;
        public string? PatientId { get; set; }
        public string? Vdate { get; set; }
        public string? DoctorName { get; set; }
    }
}
