using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.DataContract
{
    public class MoneyLaunderingRiskInfo
    {
        public RiksInfo[] RiskInfo { get; set; }
        public string PlanId { get; set; }
        public string Policy { get; set; }
        public long? Policy_Id { get; set; }
        public string Appno { get; set; }
    }
    public class RiksInfo
    {
        public string PreName { get; set; }
        public string Name { get; set; }
        public string SureName { get; set; }
        public string IdCard { get; set; }
        public string Nationality { get; set; }
        public string PersonStatus { get; set; }

    }
}
