using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmloNewbis.DataContract
{
    [DataContract]
    public class P_AML_CTF_MATRIX
    {

        [DataMember]
        public long? MX_ID { get; set; }

        [DataMember]
        public string FREEZE_FLG { get; set; }

        [DataMember]
        public string PEP_OUT_FLG { get; set; }

        [DataMember]
        public string NATIONALITY_SERIOUS_FLG { get; set; }

        [DataMember]
        public string HR02_FLG { get; set; }

        [DataMember]
        public string HR08_FLG { get; set; }

        [DataMember]
        public string PEP_IN_FLG { get; set; }

        [DataMember]
        public string OCCUPATION_FLG { get; set; }

        [DataMember]
        public string NATIONALITY_OTH_FLG { get; set; }

        [DataMember]
        public string BENEFIT_FLG { get; set; }

        [DataMember]
        public string PRODUCT_FLG { get; set; }

        [DataMember]
        public string RISK_LEVEL { get; set; }

        [DataMember]
        public string EDD_FORM { get; set; }

        [DataMember]
        public string APPROVER { get; set; }
        [DataMember]
        public string PRESON_STATUS { get; set; }

        [DataMember]
        public DateTime? EFF_DT { get; set; }

        [DataMember]
        public string UPD_ID { get; set; }

        [DataMember]
        public string TMN { get; set; }

        [DataMember]
        public DateTime? TMN_DT { get; set; }
    }
}
