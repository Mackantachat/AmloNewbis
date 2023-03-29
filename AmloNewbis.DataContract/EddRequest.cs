using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class EddRequest
    {
        public string ChannelType { get; set; }
        public string PolicyNumber { get; set; }
        public string CertNumber { get; set; }
        public string StartSystemDate { get; set; }
        public string EndSystemDate { get; set; }

    }
}
