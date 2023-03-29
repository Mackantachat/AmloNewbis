using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class U_NAME_ID
    {
        public long? UNAME_ID { get; set; }
        public long? UAPP_ID { get; set; }
        public long? NAME_ID { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string PRENAME { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string IDCARD_NO { get; set; }
        public string PASSPORT { get; set; }
        public DateTime? BIRTH_DT { get; set; }
        public string SEX { get; set; }
        public string MB_PHONE { get; set; }
        public string NATIONALITY { get; set; }
        public int? ST_AGE { get; set; }
        public string MARITAL_STATUS { get; set; }
        public string RELIGION { get; set; }
        public long? PARENT_ID { get; set; }
        public long? ONAME_ID { get; set; }
        public long? OPARENT_ID { get; set; }
        public DateTime? CARD_EXT_DT { get; set; }
        public string VULNERABLE_FLG { get; set; }
    }
}
