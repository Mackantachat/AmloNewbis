using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Model
{
    public class AmlocddInfo_Response
    {
        public CustomerInfoResponse CustomerInfo { get; set; }
        public PayerInfoResponse PayerInfo { get; set; }
        public BenefitInfoResponse[] BenefitInfo  { get; set; }
        public RiskMatrix[] RiskMatrix { get; set; }
        public UnderwrittingConsiderations UnderWritingConsideration { get; set; }
        public string AppNo { get; set; }
        public string PlanId { get; set; }
        public Boolean VerifyRisk { get; set; }
        public DateTime? VerifyRiskDate { get; set; }
        public string RiskRemark { get; set; }
        public string UserId { get; set; }
    }

    public class CustomerInfoResponse 
    {
        public string PreName { get; set; }
        public string Name { get; set; }
        public string SureName  { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public string PersonStatus  { get; set; }
        public VerifySystemInfo[] VerifySystem  { get; set; }
        public VerifyUserInfo[] VerifyUser  { get; set; }

        public RiskAmlo[] RiskAmlos { get; set; }
        public RiskOthers[] RiskOthers { get; set; }
    }


    public class PayerInfoResponse
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
    public class BenefitInfoResponse 
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
    public class VerifySystemInfo 
    {
        public Boolean Status { get; set; }
        public string DescSystem { get; set; }
        public DateTime? VerifySystemDate  { get; set; }
    }

    public class VerifyUserInfo
    {
        public Boolean Status { get; set; }
        public string DescUser { get; set; }
        public DateTime? VerifyUserDate { get; set; }
    }
    public class RiskMatrix
    {
        public string Code { get; set; }
        public Boolean Status { get; set; }
        public string DescMatrix  { get; set; }
    }

    public class UnderwrittingConsiderations
    {
        public EddForm[] EddForms { get; set; }
        public Boolean EddStatus { get; set; }
        public string EDDWarning { get; set; }
        public SummaryOfInsuranceConsideration[] SummaryOfInsuranceConsiderations { get; set; }
    }
    public class EddForm
    {
        public Boolean Status { get; set; }
        public string Code { get; set; }
        public string DescEddForm { get; set; }
        public string Remark { get; set; }
    }
    public class SummaryOfInsuranceConsideration
    {
        public Boolean Status { get; set; }
        public string Code { get; set; }
        public string DescEddForm { get; set; }
        public string Remark { get; set; }
    }

    public class RiskAmlo
    {
        public int Seq { get; set; }
        public string Code { get; set; }
        public Boolean RiskStatus { get; set; }

        public string RiskDesc { get; set; }

    }
    public class RiskOthers
    {
        public int Seq { get; set; }
        public string Code { get; set; }
        public Boolean RiskStatus { get; set; }

        public string RiskDesc  { get; set; }

    }
}
