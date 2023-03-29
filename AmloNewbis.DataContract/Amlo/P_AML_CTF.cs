using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmloNewbis.DataContract
{
    [DataContract]
    public class P_AML_CTF
    {

        [DataMember]
        public DateTime? ENTRY_DT { get; set; }

        [DataMember]
        public string ENTRY_TIME { get; set; }

        [DataMember]
        public string APP_NO { get; set; }

        [DataMember]
        public string POLICY { get; set; }

        [DataMember]
        public int? POL_YR { get; set; }

        [DataMember]
        public int? POL_LT { get; set; }

        [DataMember]
        public int? P_MODE { get; set; }

        [DataMember]
        public string PROC_FLG { get; set; }

        [DataMember]
        public string CLASSIFIED_BY { get; set; }

        [DataMember]
        public string CUST_FLG { get; set; }

        [DataMember]
        public string PRENAME { get; set; }

        [DataMember]
        public string NAME { get; set; }

        [DataMember]
        public string SURNAME { get; set; }

        [DataMember]
        public string IDCARD_NO { get; set; }

        [DataMember]
        public string PASSPORT { get; set; }

        [DataMember]
        public DateTime? BIRTH_DT { get; set; }

        [DataMember]
        public string NATIONALITY { get; set; }

        [DataMember]
        public long? SUMM { get; set; }

        [DataMember]
        public decimal? BSC_PRM { get; set; }

        [DataMember]
        public decimal? RDR_PRM { get; set; }

        [DataMember]
        public string PAYMENT_OPT { get; set; }

        [DataMember]
        public string RISK_CLASS { get; set; }

        [DataMember]
        public DateTime? SENDAUTH_DT { get; set; }

        [DataMember]
        public string AMLO_AUTH { get; set; }

        [DataMember]
        public DateTime? AUTH_DT { get; set; }

        [DataMember]
        public string AMLO_SEND { get; set; }

        [DataMember]
        public DateTime? AMLO_DT { get; set; }

        [DataMember]
        public DateTime? FSYSTEM_DT { get; set; }

        [DataMember]
        public DateTime? UPD_DT { get; set; }

        [DataMember]
        public string UPD_ID { get; set; }

        [DataMember]
        public int? BNF_PAY { get; set; }

        [DataMember]
        public string SAME_PS_SYSTEM_FLG { get; set; }

        [DataMember]
        public DateTime? SAME_PS_SYSTEM_DT { get; set; }

        [DataMember]
        public string SAME_PS_UND_FLG { get; set; }

        [DataMember]
        public DateTime? SAME_PS_UND_DT { get; set; }

        [DataMember]
        public string FREEZE_FLG { get; set; }

        [DataMember]
        public string PEP_IN_FLG { get; set; }

        [DataMember]
        public string PEP_OUT_FLG { get; set; }

        [DataMember]
        public string RCA_IN_FLG { get; set; }

        [DataMember]
        public string RCA_OUT_FLG { get; set; }

        [DataMember]
        public string STR_FLG { get; set; }

        [DataMember]
        public string HR02_FLG { get; set; }

        [DataMember]
        public string HR08_FLG { get; set; }

        [DataMember]
        public string NATION_SERIOUS_FLG { get; set; }

        [DataMember]
        public string NATION_OTH_FLG { get; set; }

        [DataMember]
        public string OCC_FLG { get; set; }

        [DataMember]
        public string PRODUCT_FLG { get; set; }

        [DataMember]
        public DateTime? SYSTEM_DT { get; set; }

        [DataMember]
        public long? POLICY_ID { get; set; }

        [DataMember]
        public int? SEQ { get; set; }

        [DataMember]
        public string TMN { get; set; }

        [DataMember]
        public DateTime? TMN_DT { get; set; }

        [DataMember]
        public string TMN_ID { get; set; }

    }

}
