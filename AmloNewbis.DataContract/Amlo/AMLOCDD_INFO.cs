using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class AMLOCDD_INFO
    {
        public CUSTOMER_INFO CUSTOMER_INFO { get; set; }
        public PAYER_INFO PAYER_INFO { get; set; }
        public BENEFIT_INFO[] BENEFIT_INFO { get; set; }
        public RISK_MATRIX[] RISK_MATRIX { get; set; }
        public UNDERWRITING_CONSIDERATIONS UNDERWRITING_CONSIDERATIONS { get; set; }
        public string POLICY { get; set; }
        public long? POLICY_ID { get; set; }
        public string APP_NO { get; set; }
        public string PLAN_ID { get; set; }
        public string VERIFY_RISK { get; set; }
        public DateTime? VERIFY_RISK_DATE { get; set; }
        public string RISK_REMARK { get; set; }
        public string USER_ID { get; set; }
    }

    public class CUSTOMER_INFO
    {
        public string PRE_NAME { get; set; }
        public string NAME { get; set; }
        public string SURE_NAME { get; set; }
        public string ID_CARD { get; set; }
        public string NATIONALITY { get; set; }
        public string PERSON_STATUS { get; set; }
        public VERIFY_SYSTEM_INFO[] VERIFY_SYSTEM { get; set; }
        public VERIFY_USER_INFO[] VERIFY_USER { get; set; }
        public RISK_AMLO[] RISK_AMLOs { get; set; }
        public RISK_OTHERs[] RISK_OTHERs { get; set; }

    }

    public class PAYER_INFO
    {
        public string PRE_NAME { get; set; }
        public string NAME { get; set; }
        public string SURE_NAME { get; set; }
        public string ID_CARD { get; set; }
        public string NATIONALITY { get; set; }
        public string PERSON_STATUS { get; set; }
        public VERIFY_SYSTEM_INFO[] VERIFY_SYSTEM { get; set; }
        public VERIFY_USER_INFO[] VERIFY_USER { get; set; }
        public DateTime? VERIFY_USER_DATE { get; set; }
        public DateTime? VERIFY_SYSTEM_DATE { get; set; }
        public RISK_AMLO[] RISK_AMLOs { get; set; }
        public RISK_OTHERs[] RISK_OTHERs { get; set; }
    }
    public class BENEFIT_INFO
    {
        public string PRE_NAME { get; set; }
        public string NAME { get; set; }
        public string SURE_NAME { get; set; }
        public string ID_CARD { get; set; }
        public string NATIONALITY { get; set; }
        public string PERSON_STATUS { get; set; }
        public VERIFY_SYSTEM_INFO[] VERIFY_SYSTEM { get; set; }
        public VERIFY_USER_INFO[] VERIFY_USER { get; set; }
        public string  FULLNAME { get; set; }
        public RISK_AMLO[] RISK_AMLOs { get; set; }
    }
    public class RISK_MATRIX
    {
        public string CODE { get; set; }
        public string STATUS { get; set; }
        public string DESC_MATRIX { get; set; }
    }
    public class VERIFY_SYSTEM_INFO
    {
        public string STATUS { get; set; }
        public string DESC_SYSTEM { get; set; }
        public DateTime? VERIFY_SYSTEM_DATE { get; set; }
    }

    public class VERIFY_USER_INFO
    {
        public string STATUS { get; set; }
        public string DESC_USER { get; set; }
        public DateTime? VERIFY_USER_DATE { get; set; }
    }
    public class UNDERWRITING_CONSIDERATIONS
    {
        public EDD_FORM[] EDD_FORMs { get; set; }
        public string EDD_STATUS { get; set; }
        public string EDD_Warning { get; set; }
        public SUMMARY_OF_INSURANCE_CONSIDERATIONS[] SUMMARY_OF_INSURANCE_CONSIDERATIONs { get; set; }
    }
    public class EDD_FORM
    {
        public string STATUS { get; set; }
        public string CODE { get; set; }
        public string DESC_EDD_FORM { get; set; }
        public string REMARK { get; set; }
    }
    public class SUMMARY_OF_INSURANCE_CONSIDERATIONS
    {
        public string STATUS { get; set; }
        public string CODE { get; set; }
        public string DESC_EDD_FORM { get; set; }
        public string REMARK { get; set; }
    }



    public class RISK_AMLO
    {
        public int Seq { get; set; }
        public string RISK_STATUS { get; set; }

        public string RISK_DESC { get; set; }
        public string RISK_CODE { get; set; }

    }
    public class RISK_OTHERs
    {
        public int Seq { get; set; }
        public string RISK_STATUS { get; set; }

        public string RISK_DESC { get; set; }
        public string RISK_CODE { get; set; }

    }
}
