using AmloNewbis.DataContract;
using ITUtility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using DataAccessUtility;

namespace AmloNewbis.DataAccess
{
    public partial class Repository : RepositoryBaseManagedCore
    {
        public EddReport[] GetEddReportTypeP(string appNo)
        {
            EddReport[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select aml.name||' ' ||aml.surname as full_name , aml.BIRTH_DT , aml.IDCARD_NO , uname.mb_phone,uemail.email ,aml.upd_id , 
                        aml.policy_id , aml.app_no , aml.NATIONALITY , aml.BSC_PRM , aml.RDR_PRM ,aml.PAYMENT_OPT  , po.OPT_NAME , ulife.pl_block , ulife.pl_code , 
                        ulife.pl_code2 , zplan.title, aml.OCC_FLG,  amlEdd.signature_flg, amledd.th_idcard_flg , amledd.noth_idcard_flg ,amledd.payment_income_flg ,
                        amledd.income_src_flg,amledd.news_flg,amledd.news_remark,amledd.inforce_flg, amledd.inforce_remark, aml.FREEZE_FLG , aml.PEP_IN_FLG ,
                        aml.pep_out_flg , aml.str_flg , aml.hr02_Flg , aml.hr08_Flg ,aml.Rca_in_flg , aml.rca_out_flg , aml.nation_serious_flg , aml.nation_oth_flg , 
                        aml.occ_flg, aml.product_flg, amlremark.remark
                        from policy.p_aml_Ctf aml
                        LEFT JOIN policy.ztb_pay_option po ON aml.payment_opt = po.OPT_CODE
                        LEFT JOIN policy.U_APPLICATION_ID uappid ON aml.app_no = uappid.app_no
                        LEFT JOIN policy.U_APPLICATION uapp ON uappid.APP_ID = uapp.APP_ID
                        LEFT JOIN policy.U_LIFE_ID ulife ON ulife.UAPP_ID = uapp.UAPP_ID
                        LEFT JOIN policy.ztb_plan zplan ON ulife.pl_block = zplan.pl_block AND  ulife.pl_code = zplan.pl_code AND  ulife.pl_code2 = zplan.pl_code2 
                        LEFT JOIN policy.U_NAME_ID uName ON uname.uapp_id = uapp.uapp_id
                        LEFT JOIN policy.u_email_id uEmail ON uEmail.uname_id = uname.uname_id
                        LEFt JOIN policy.p_aml_Ctf_edd amlEdd ON amlEdd.app_no = aml.app_no
                        LEFT JOIN policy.p_aml_ctf_remark amlRemark ON amlremark.app_no = aml.app_no
                        where  aml.cust_flg='P' AND aml.CLASSIFIED_BY = 'APP' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(appNo))
            {
                sql.Append(@" AND aml.APP_NO = :appNo");
                oCmd.Parameters.Add(new OracleParameter("appNo", appNo));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<EddReport>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public EddReport[] GetEddReportTypeC(string appNo)
        {
            EddReport[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select aml.name||' ' ||aml.surname as full_name , aml.BIRTH_DT , aml.IDCARD_NO , uname.mb_phone,uemail.email ,aml.upd_id , 
                        aml.policy_id , aml.app_no , aml.NATIONALITY , aml.BSC_PRM , aml.RDR_PRM ,aml.PAYMENT_OPT  , po.OPT_NAME , ulife.pl_block , ulife.pl_code , 
                        ulife.pl_code2 , zplan.title, aml.OCC_FLG,  amlEdd.signature_flg, amledd.th_idcard_flg , amledd.noth_idcard_flg ,amledd.payment_income_flg ,
                        amledd.income_src_flg,amledd.news_flg,amledd.news_remark,amledd.inforce_flg, amledd.inforce_remark, aml.FREEZE_FLG , aml.PEP_IN_FLG ,
                        aml.pep_out_flg , aml.str_flg , aml.hr02_Flg , aml.hr08_Flg ,aml.Rca_in_flg , aml.rca_out_flg , aml.nation_serious_flg , aml.nation_oth_flg , 
                        aml.occ_flg, aml.product_flg, amlremark.remark
                        from policy.p_aml_Ctf aml
                        LEFT JOIN policy.ztb_pay_option po ON aml.payment_opt = po.OPT_CODE
                        LEFT JOIN policy.U_APPLICATION_ID uappid ON aml.app_no = uappid.app_no
                        LEFT JOIN policy.U_APPLICATION uapp ON uappid.APP_ID = uapp.APP_ID
                        LEFT JOIN policy.U_LIFE_ID ulife ON ulife.UAPP_ID = uapp.UAPP_ID
                        LEFT JOIN policy.ztb_plan zplan ON ulife.pl_block = zplan.pl_block AND  ulife.pl_code = zplan.pl_code AND  ulife.pl_code2 = zplan.pl_code2 
                        LEFT JOIN policy.U_NAME_ID uName ON uname.uapp_id = uapp.uapp_id
                        LEFT JOIN policy.u_email_id uEmail ON uEmail.uname_id = uname.uname_id
                        LEFt JOIN policy.p_aml_Ctf_edd amlEdd ON amlEdd.app_no = aml.app_no
                        LEFT JOIN policy.p_aml_ctf_remark amlRemark ON amlremark.app_no = aml.app_no
                        where  aml.cust_flg='C' AND aml.CLASSIFIED_BY = 'APP' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(appNo))
            {
                sql.Append(@" AND aml.APP_NO = :appNo");
                oCmd.Parameters.Add(new OracleParameter("appNo", appNo));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<EddReport>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public ZTB_USER GetFactRecorder(string userId)
        {
            ZTB_USER returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select name || ' ' || surname as fullname from center.ztb_user where 1=1");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(userId))
            {
                sql.Append(@" AND n_userid = :n_userid");
                oCmd.Parameters.Add(new OracleParameter("n_userid", userId));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<ZTB_USER>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public EKYC_MAIN GetEKYC_MAIN(string reference_id_2 , string appNo)
        {
            EKYC_MAIN returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select RESULT_FLG From policy.ekyc_main  where 1=1");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(reference_id_2))
            {
                sql.Append(@" AND reference_id_2 = :reference_id_2");
                oCmd.Parameters.Add(new OracleParameter("reference_id_2", reference_id_2));
            }
            if (!string.IsNullOrEmpty(appNo))
            {
                sql.Append(@" AND REFERENCE_KEY_2 = :appNo");
                oCmd.Parameters.Add(new OracleParameter("appNo", appNo));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<EKYC_MAIN>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public U_ADDRESS_ID[] GetAddress(string appNo)
        {
            U_ADDRESS_ID[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select uAdr.ADDRESS_TYPE 
                            from policy.U_APPLICATION_ID uAppId
                            LEFT JOIN policy.U_APPLICATION uApp ON uapp.app_id = uappid.app_id 
                            LEFT JOIN policy.U_NAME_ID uName ON uname.uapp_id = uapp.uapp_id
                            LEFT JOIN policy.U_APP_ADDRESS uAppAdr ON uappadr.uname_id = uname.uname_id
                            LEFT JOIN policy.U_ADDRESS_ID uAdr ON uappadr.uaddress_id = uadr.uaddress_id
                            where 1=1");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(appNo))
            {
                sql.Append(@" AND uAppId.app_no = :appNo");
                oCmd.Parameters.Add(new OracleParameter("appNo", appNo));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<U_ADDRESS_ID>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public EddReport GetRiskBenefit(string appNo)
        {
            EddReport returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select app_no from policy.p_aml_Ctf where  cust_flg='B'  and risk_class in (2,3)");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(appNo))
            {
                sql.Append(@" AND app_no = :appNo");
                oCmd.Parameters.Add(new OracleParameter("appNo", appNo));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<EddReport>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public AmloReport[] GetAmloReport(string appNo)
        {
            AmloReport[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select APP_NO , POLICY , CUST_FLG , PRENAME , NAME , SURNAME , SAME_PS_UND_FLG , FREEZE_FLG , PEP_IN_FLG , PEP_OUT_FLG , 
                        RCA_IN_FLG , RCA_OUT_FLG , STR_FLG , HR02_FLG , HR08_FLG , NATION_SERIOUS_FLG , NATION_OTH_FLG , OCC_FLG , PRODUCT_FLG , RISK_CLASS , UPD_ID , UPD_DT
                        from  policy.p_aml_Ctf 
                        WHERE cust_flg in ('B','C') AND CLASSIFIED_BY = 'APP' ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(appNo))
            {
                sql.Append(@" AND APP_NO = :appNo");
                oCmd.Parameters.Add(new OracleParameter("appNo", appNo));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<AmloReport>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public P_POLICY_IDENTITY GetP_POLICY_IDENTITY(string policyId)
        {
            P_POLICY_IDENTITY returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select * from policy.p_policy_identity where 1=1 ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(policyId))
            {
                sql.Append(@" AND policy_id = :policyId");
                oCmd.Parameters.Add(new OracleParameter("policyId", policyId));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<P_POLICY_IDENTITY>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public ZTB_USER GetZTB_USER(string userId)
        {
            ZTB_USER returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select USERID , PRENAME , NAME , SURNAME from center.ztb_user where 1=1");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(userId))
            {
                sql.Append(@" AND n_userid = :n_userid");
                oCmd.Parameters.Add(new OracleParameter("n_userid", userId));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<ZTB_USER>().FirstOrDefault();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public AUTB_CHANNEL[] GetAUTB_CHANNEL()
        {
            AUTB_CHANNEL[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT CHANNEL_TYPE ,DESCRIPTION , POLICY_TYPE FROM POLICY.AUTB_CHANNEL ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<AUTB_CHANNEL>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }

        public AmloReport[] GetAmloData(EddRequest request)
        {
            AmloReport[] returnValue = null;
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PI.POLICY_NUMBER , PI.CERT_NUMBER , PN.NAME , PN.SURNAME , PAC.SYSTEM_DT , PAC.APP_NO
                            FROM POLICY.P_POLICY_IDENTITY PI
                            LEFT JOIN POLICY.P_APPL_ID PAI ON PAI.POLICY_ID = PI.POLICY_ID
                            LEFT JOIN POLICY.P_POL_NAME PPN ON PPN.POLICY_ID = PI.POLICY_ID 
                            LEFT JOIN POLICY.P_NAME_ID PN ON PN.NAME_ID = PPN.NAME_ID AND PN.TMN = 'N'
                            LEFT JOIN POLICY.P_AML_CTF PAC ON PAC.POLICY_ID = PI.POLICY_ID AND PAC.SYSTEM_DT > TO_DATE('01/12/2022','dd/mm/yyyy')
                            WHERE PAC.CLASSIFIED_BY = 'APP' AND PAC.CUST_FLG = 'C'
                              ");
            OracleCommand oCmd = new OracleCommand();
            oCmd.BindByName = true;
            oCmd.Connection = connection;
            oCmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(request.ChannelType))
            {
                sql.Append(@" AND PI.CHANNEL_TYPE = :CHANNEL_TYPE");
                oCmd.Parameters.Add(new OracleParameter("CHANNEL_TYPE", request.ChannelType));
            }
            if (!string.IsNullOrEmpty(request.PolicyNumber))
            {
                sql.Append(@" AND PI.POLICY_NUMBER = :POLICY_NUMBER");
                oCmd.Parameters.Add(new OracleParameter("POLICY_NUMBER", request.PolicyNumber));
            }
            if (!string.IsNullOrEmpty(request.CertNumber))
            {
                sql.Append(@" AND PI.CERT_NUMBER = :CERT_NUMBER");
                oCmd.Parameters.Add(new OracleParameter("CERT_NUMBER", request.CertNumber));
            }
            if (!string.IsNullOrEmpty(request.StartSystemDate) && !string.IsNullOrEmpty(request.EndSystemDate))
            {
                sql.Append(@"  AND PAC.SYSTEM_DT BETWEEN TO_DATE(:STARTSYSTEMDATE,'DD/MM/YYYY') AND TO_DATE(:ENDSYSTEMDATE,'DD/MM/YYYY') ");
                oCmd.Parameters.Add(new OracleParameter("STARTSYSTEMDATE", request.StartSystemDate));
                oCmd.Parameters.Add(new OracleParameter("ENDSYSTEMDATE", request.EndSystemDate));
            }
            oCmd.CommandText = sql.ToString();
            using (DataTable dt = Utility.FillDataTable(oCmd))
            {
                if (dt.Rows.Count > 0)
                {
                    returnValue = dt.AsEnumerable<AmloReport>().ToArray();
                }
            }
            oCmd.Dispose();
            return returnValue;
        }
    }
}
