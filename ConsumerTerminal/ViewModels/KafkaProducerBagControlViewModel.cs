using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerTerminal.ViewModels;

namespace ConsumerTerminal.ViewModels
{
    public class KafkaProducerBagControlViewModel
    {
        public string? Topic { get; set; }
        public string? Key { get; set; }
        public BagControlMsg? Message { get; set; }
    }

    public class BagControlMsg
    {
        public DoctorMessage? DoctorMessage { get; set; }
        public string? PresNo { get; set; }
        public string? DetId { get; set; }
    }
}
