using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class EddReport
    {
        public string FULL_NAME { get; set; }
        public string BIRTH_DT { get; set; }
        public string IDCARD_NO { get; set; }
        public string MB_PHONE { get; set; }
        public string EMAIL { get; set; }
        public string UPD_ID { get; set; }
        public string POLICY_ID { get; set; }
        public string APP_NO { get; set; }
        public string NATIONALITY { get; set; }
        public string BSC_PRM { get; set; }
        public string RDR_PRM { get; set; }
        public string SUM_PRM { get; set; }
        public string PAYMENT_OPT { get; set; }
        public string OPT_NAME { get; set; }
        public string PL_BLOCK { get; set; }
        public string PL_CODE { get; set; }
        public string PL_CODE2 { get; set; }
        public string TITLE { get; set; }
        public string OCC_FLG { get; set; }
        public string SIGNATURE_FLG { get; set; }
        public string TH_IDCARD_FLG { get; set; }
        public string NOTH_IDCARD_FLG { get; set; }
        public string PAYMENT_INCOME_FLG { get; set; }
        public string INCOME_SRC_FLG { get; set; }
        public string NEWS_FLG { get; set; }
        public string NEWS_REMARK { get; set; }
        public string INFORCE_FLG { get; set; }
        public string INFORCE_REMARK { get; set; }
        public string FREEZE_FLG { get; set; }
        public string PEP_IN_FLG { get; set; }
        public string PEP_OUT_FLG { get; set; }
        public string STR_FLG { get; set; }
        public string HR02_FLG { get; set; }
        public string HR08_FLG { get; set; }
        public string RCA_IN_FLG { get; set; }
        public string RCA_OUT_FLG { get; set; }
        public string NATION_SERIOUS_FLG { get; set; }
        public string NATION_OTH_FLG { get; set; }
        public string PRODUCT_FLG { get; set; }
        public string REMARK { get; set; }
        public string MyProperty { get; set; }
        public string FACT_RECORDER { get; set; }
        public string RESULT_FLG { get; set; }
        public string CURRENT_ADDRESS { get; set; }
        public string WORK_ADDRESS { get; set; }
        public string REGISTRATION_ADDRESS { get; set; }
        public string RISK_BENEFIT { get; set; }
    }

    public class EddReportRequest
    {
        public string N_USER_ID { get; set; }
        public string APP_NO { get; set; }
        public string POLICY_NO { get; set; }
    }
}
