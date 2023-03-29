using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmloNewbis.DataContract
{
    [DataContract]
    public class P_AML_CTF_EDD
    {

        [DataMember]
        public string APP_NO { get; set; }

        [DataMember]
        public string POLICY_ID { get; set; }

        [DataMember]
        public string NEWS_FLG { get; set; }

        [DataMember]
        public string NEWS_REMARK { get; set; }

        [DataMember]
        public string TH_IDCARD_FLG { get; set; }

        [DataMember]
        public string NOTH_IDCARD_FLG { get; set; }

        [DataMember]
        public string PAYMENT_INCOME_FLG { get; set; }

        [DataMember]
        public string INCOME_SRC_FLG { get; set; }

        [DataMember]
        public string SIGNATURE_FLG { get; set; }

        [DataMember]
        public string INFORCE_FLG { get; set; }

        [DataMember]
        public string INFORCE_REMARK { get; set; }

        [DataMember]
        public DateTime? EFF_DT { get; set; }

        [DataMember]
        public string UPD_ID { get; set; }

        [DataMember]
        public string? TMN { get; set; }

        [DataMember]
        public DateTime? TMN_DT { get; set; }
    }

}
