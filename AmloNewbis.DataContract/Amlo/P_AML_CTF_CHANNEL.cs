using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmloNewbis.DataContract
{
    [DataContract]
    public class P_AML_CTF_CHANNEL
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
        public string CHANNEL_TYPE { get; set; }

        [DataMember]
        public string POLICY_HOLDING { get; set; }

        [DataMember]
        public int? ALL_POLICY { get; set; }

        [DataMember]
        public long? ALL_SUMM { get; set; }

        [DataMember]
        public long? ALL_PREMIUM { get; set; }
    }

}
