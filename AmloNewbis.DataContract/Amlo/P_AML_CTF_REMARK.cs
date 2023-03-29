using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmloNewbis.DataContract
{
    [DataContract]
    public class P_AML_CTF_REMARK
    {

        [DataMember]
        public string APP_NO { get; set; }

        [DataMember]
        public long? POLICY_ID { get; set; }

        [DataMember]
        public string REMARK { get; set; }
    }

}
