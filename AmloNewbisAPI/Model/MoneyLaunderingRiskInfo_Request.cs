using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Model
{
    public class MoneyLaunderingRiskInfo_Request
    {
        public RiskInfo[] RiskInfo { get; set; }
        public string PlanId { get; set; }
        public string Policy { get; set; }
        public long? Policy_Id { get; set; }
        public string Appno { get; set; }
    }

    public class BenefitInfo
    {
        public string Benefit_Name { get; set; }
        public string Benefit_SureName { get; set; }
        public int Benefit_IdCard { get; set; }

    }
    public class RiskInfo
    {
        public string Name { get; set; }
        public string SureName { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public string PersonStatus { get; set; }

    }
    public class Info
    {
        public string PlanId { get; set; }
        public string Appno { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_SureName { get; set; }
        public int Customer_IdCard { get; set; }
        public string Customer_Nationality { get; set; }
        public string Payer_Name { get; set; }
        public string Payer_SureName { get; set; }
        public int Payer_IdCard { get; set; }
        public string Payer_Nationality { get; set; }
        public BenefitInfo[] BenefitInfo { get; set; }

    }



}
