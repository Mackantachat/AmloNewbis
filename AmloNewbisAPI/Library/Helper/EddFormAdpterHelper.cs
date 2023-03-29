using AmloNewbis.DataContract.Amlo;
using AmloNewbisAPI.Model;
using System.Collections.Generic;
using System.Linq;
using PolicyWithCertServiceReference1;
namespace AmloNewbisAPI.Library.Helper
{
    public static class EddFormAdpterHelper
    {

        public static List<Model.ZTB_POLICYOWNER_PLAN> ToListDataZtbPolicyOwnerPlan(this ZTB_POLICYOWNER_PLAN_Collection arrData)
        {
            List<Model.ZTB_POLICYOWNER_PLAN> lstPolicyOwnerPlan = new List<Model.ZTB_POLICYOWNER_PLAN>();
            if (arrData != null && arrData.Any())
            {
                foreach (var item in arrData)
                {
                    Model.ZTB_POLICYOWNER_PLAN rowPolicyOwnerPlan = new Model.ZTB_POLICYOWNER_PLAN();
                    rowPolicyOwnerPlan.Policy = item.POLICY;
                    lstPolicyOwnerPlan.Add(rowPolicyOwnerPlan);
                }
            }
            return lstPolicyOwnerPlan;
        }
    }
}
