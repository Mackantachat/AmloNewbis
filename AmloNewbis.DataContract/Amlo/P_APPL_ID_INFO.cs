using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AmloNewbis.DataContract.Amlo
{
    [DataContract]
    public class P_APPL_ID_INFO
    {

        [DataMember]
        public string POLICY { get; set; }

        [DataMember]
        public long? POLICY_ID { get; set; }

        [DataMember]
        public string CHANNEL_TYPE { get; set; }
        [DataMember]
        public string APP_NO { get; set; }
        [DataMember]
        public DateTime? APP_DT { get; set; }
        [DataMember]
        public string PLANCODE { get; set; }
        [DataMember]
        public string PLAN_DESC { get; set; }
        [DataMember]
        public RISK_INFO[] RISK_INFOs { get; set; }
    }
    
    public class RISK_INFO
    {
        public string PRE_NAME { get; set; }
        public string NAME { get; set; }
        public string SURE_NAME { get; set; }
        public string ID_CARD  { get; set; }
        public string NATIONALITY  { get; set; }
        public string PERSON_STATUS  { get; set; }

    }
}
