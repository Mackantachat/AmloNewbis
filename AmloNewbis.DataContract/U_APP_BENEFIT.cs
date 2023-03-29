using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class U_APP_BENEFIT
    {
        public long? UAPP_ID { get; set; }
        public int? SEQ { get; set; }
        public long? UBENEFIT_ID { get; set; }
        public string PRENAME { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string RELATION { get; set; }
        public decimal? GAIN_PERCENT { get; set; }
        public string CARD_NO { get; set; }
        public string MESSAGE { get; set; }
    }
}
