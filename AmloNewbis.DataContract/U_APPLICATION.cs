using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class U_APPLICATION
    {
        public long? UAPP_ID { get; set; }
        public long? APP_ID { get; set; }
        public string APP_NO { get; set; }
        public string CHANNEL_TYPE { get; set; }
        public DateTime? APP_DT { get; set; }
        public string PLAN_CODE { get; set; }
        public string PLAN_TITLE { get; set; }
    }
}
