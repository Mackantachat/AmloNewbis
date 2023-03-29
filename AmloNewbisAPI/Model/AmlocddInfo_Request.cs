using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Model
{
    public class AmlocddInfo_Request
    {
        public CustomerInfoRequest CustomerInfo { get; set; }
        public PayerInfoRequest PayerInfo { get; set; }
        public BenefitInfoRequest[] BenefitInfo { get; set; }
        public RiskMatrix[] RiskMatrix { get; set; }
        public UnderwrittingConsiderations UnderWritingConsideration { get; set; }
        //public string POLICY { get; set; }
        //public long? POLICY_ID { get; set; }
        public string AppNo { get; set; }
        public string PlanId { get; set; }
        public string VerifyRisk { get; set; }
        public DateTime? VerifyRiskDate { get; set; }
        public string RiskRemark { get; set; }
        public string UserId { get; set; }
    }

    public class CustomerInfoRequest
    {
        public string PreName { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public string PersonStatus { get; set; }
        public VerifySystemInfo[] VerifySystem { get; set; }
        public VerifyUserInfo[] VerifyUser { get; set; }

        public RiskAmlo[] RiskAmlos { get; set; }
        public RiskOthers[] RiskOthers { get; set; }
    }


    public class PayerInfoRequest
    {
        public string PreName { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public string PersonStatus { get; set; }
        public VerifySystemInfo[] VerifySystem { get; set; }
        public VerifyUserInfo[] VerifyUser { get; set; }
        public RiskAmlo[] RiskAmlos { get; set; }
        public RiskOthers[] RiskOthers { get; set; }
    }
    public class BenefitInfoRequest
    {
        public string PreName { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public string PersonStatus { get; set; }
        public string FullName { get; set; }
        public VerifySystemInfo[] VerifySystem { get; set; }
        public VerifyUserInfo[] VerifyUser { get; set; }
        public RiskAmlo[] RiskAmlos { get; set; }
    }
}
