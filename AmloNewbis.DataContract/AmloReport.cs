using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class AmloReport
    {
        public string APP_NO { get; set; }
        public string POLICY { get; set; }
        public string POLICY_NUMBER { get; set; }
        public string CERT_NUMBER { get; set; }
        public string CUST_FLG { get; set; }
        public string PRENAME { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string SAME_PS_UND_FLG { get; set; }
        public string FREEZE_FLG { get; set; }
        public string PEP_IN_FLG { get; set; }
        public string PEP_OUT_FLG { get; set; }
        public string RCA_IN_FLG { get; set; }
        public string RCA_OUT_FLG { get; set; }
        public string STR_FLG { get; set; }
        public string HR02_FLG { get; set; }
        public string HR08_FLG { get; set; }
        public string NATION_SERIOUS_FLG { get; set; }
        public string NATION_OTH_FLG { get; set; }
        public string OCC_FLG { get; set; }
        public string PRODUCT_FLG { get; set; }
        public string RISK_CLASS { get; set; }
        public string UPD_ID { get; set; }
        public DateTime UPD_DT { get; set; }
        public string ASSESSOR { get; set; }
        public string SYSTEM_DT { get; set; }
    }

    public class AmloReportRequest
    {
        public string N_USER_ID { get; set; }
        public string APP_NO { get; set; }
        public string POLICY_NO { get; set; }
    }
}
