using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class P_POL_BENEFIT
    {
        public long? POLICY_ID { get; set; }
        public long? BENEFIT_ID { get; set; }
        public string PRENAME { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string RELATION { get; set; }
        public decimal? GAIN_PERCENT { get; set; }
        public string CARD_NO { get; set; }
    }
}
