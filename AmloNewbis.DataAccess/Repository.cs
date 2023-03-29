using AmloNewbis.DataContract;
using AmloNewbis.DataContract.Amlo;
using DataAccessUtility;
using ITUtility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmloNewbis.DataAccess
{
    public partial class Repository : RepositoryBaseManagedCore
    {
        public Repository(string ConnectionString, ConnectionClientDetail ClientDetail = null)
         : base(ConnectionString, ClientDetail)
        {

        }
        public Repository(OracleConnection connection) : base(connection)
        {

        }

        public static double? differenceDay(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || endDate == null)
                return null;
            TimeSpan span = endDate.Value - startDate.Value;
            return span.TotalDays;
        }
        public async Task<string> GetCDD_PKG_CHK_DATA_SUSPECT(string name, string sureName,string idCard)
        {
           
            string sqlStr = "POLICY.CDD_PKG.CHK_DATA_SUSPECT";
            OracleCommand oCmd = new OracleCommand(sqlStr, connection);
            oCmd.CommandType = CommandType.StoredProcedure;

            oCmd.Parameters.Clear();


            OracleParameter _name = new OracleParameter("i_name", OracleDbType.Varchar2, name, ParameterDirection.Input);
            oCmd.Parameters.Add(_name);
            OracleParameter _sureName = new OracleParameter("i_surname", OracleDbType.Varchar2, sureName, ParameterDirection.Input);
            oCmd.Parameters.Add(_sureName);

            OracleParameter _idCard = new OracleParameter("i_idcardno", OracleDbType.Varchar2, idCard, ParameterDirection.Input);
            oCmd.Parameters.Add(_idCard);

            OracleParameter _passport = new OracleParameter("i_passport", OracleDbType.Varchar2, idCard, ParameterDirection.Input);
            oCmd.Parameters.Add(_passport);

           

            OracleParameter _outflg = new OracleParameter("o_findflg", OracleDbType.Varchar2, ParameterDirection.Output);
            _outflg.Size = 1;
            oCmd.Parameters.Add(_outflg);
            OracleParameter _outEntityId = new OracleParameter("o_entityid", OracleDbType.Varchar2, ParameterDirection.Output);
            oCmd.Parameters.Add(_outEntityId);
            OracleParameter _outInfosource = new OracleParameter("o_infosource", OracleDbType.Varchar2, ParameterDirection.Output);
            oCmd.Parameters.Add(_outInfosource);
            oCmd.ExecuteNonQuery();

            oCmd.Dispose();

            var flg = !string.IsNullOrEmpty(Utility.GetOraParamString(_outflg).ToString()) ? Utility.GetOraParamString(_outflg).ToString() : null;
            var result_flg = Utility.GetOraParamString(_outflg);
            var error_msg = Utility.GetOraParamString(_outEntityId);
            var outInfosource = Utility.GetOraParamString(_outInfosource);

            return flg; 
        }

        public async Task<AMLOCDD_DATA_BATCH[]> GetAmlocdd_Data_Batch(string name, string sureName, string idCard,int? typeIn, string infoSource)
        {
            List<DBParameter> param = new List<DBParameter>();
            string sqlStr = "";
            string fullName_1 = string.Empty;
            string fullName_2 = string.Empty;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(sureName))
            {
                fullName_1 = name + " " + sureName;
                fullName_2 = sureName + " " + name;
            }
            else if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(sureName))
            {
                fullName_1 = name;
                fullName_2 = name;
            }
            else if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(sureName))
            {
                fullName_1 = sureName;
                fullName_2 = name;
            }
            else
            {
                fullName_1 = null;
                fullName_2 = null;
            }

            /*sqlStr = $@" SELECT * FROM  POLICY.AMLOCDD_DATA_BATCH WHERE (SINGLE_STRING_NAME Like  '%{name}%' or ORIGINAL_SCRIPT_NAME Like  '%{name}%') 
            AND (SINGLE_STRING_NAME Like  '%{sureName}%' or ORIGINAL_SCRIPT_NAME Like  '%{sureName}%' ) AND  ID_VALUE = '{idCard}'";*/


            if (typeIn == 1 && !string.IsNullOrEmpty(idCard))
            {
                Utility.SQLValueString(param, "vIdCard", idCard);

                sqlStr = @" SELECT * FROM  POLICY.AMLOCDD_DATA_BATCH WHERE id_value = :vIdCard ";


            }
            else if (typeIn == 2)
            {
                Utility.SQLValueString(param, "vFullName1", fullName_1);
                Utility.SQLValueString(param, "vFullName2", fullName_2);
                sqlStr = @" SELECT * FROM  POLICY.AMLOCDD_DATA_BATCH WHERE single_string_name= :vFullName1 or single_string_name = :vFullName2 ";
            }
            else if (typeIn == 3)
            {
                Utility.SQLValueString(param, "vFullName1", fullName_1);
                Utility.SQLValueString(param, "vFullName2", fullName_2);
                sqlStr = @" SELECT * FROM  POLICY.AMLOCDD_DATA_BATCH WHERE original_script_name = :vFullName1 or original_script_name = :vFullName2";
            }
            else
            {
                return null;
            }

            var obj = await Utility.FillDataTableAsync<AMLOCDD_DATA_BATCH>(sqlStr, connection, param);


            if (obj != null && obj.Any())
            {
                return obj;
            }
            return null;
        }
        public async Task<bool> GetOthersAmlocdd(string appNo, bool nationalityNT, bool nationalityONT, bool riskOCC,bool riskPRD)
        {
            List<DBParameter> listParam = new List<DBParameter>();
            Utility.SQLValueString(listParam, "vAppno", appNo);
            string sqlStr = @"   SELECT NAM.NATIONALITY,ZPI.RISK_CLASS, OCC.REGULAR_OCP_TYPE
            FROM  U_APPLICATION_ID UAPPID,U_APPLICATION UAPP,U_NAME_ID NAM, U_OCCUPATION OCC,U_LIFE_ID LF, ZTB_PLAN_ID ZPI 
            WHERE  UAPPID.APP_ID=UAPP.APP_ID AND UAPP.UAPP_ID=NAM.UAPP_ID AND NAM.UNAME_ID=OCC.UNAME_ID 
            AND LF.PL_BLOCK=ZPI.PL_BLOCK AND LF.PL_cODE=ZPI.PL_cODE AND LF.PL_cODE2=ZPI.PL_CODE2  AND TMN='N' AND APP_NO = :vAppno  ";

            if (nationalityNT)
            {
                sqlStr = sqlStr + " AND NAM.NATIONALITY IN  ('IR','KP') ";
            }
            if (nationalityONT)
            {
                sqlStr = sqlStr + " AND NAM.NATIONALITY IN ( SELECT NATIONALITY FROM ZTB_COUNTRY WHERE HRJC='Y') ";
            }
            if (riskOCC)
            {
                sqlStr = sqlStr + " AND OCC.REGULAR_OCP_TYPE IN   ('D2','D6','D3','D4','E4','J3','E3','D9','D5')";
            }
            if (riskPRD)
            {
                sqlStr = sqlStr + " AND ZPI.RISK_cLASS=3 ";
            }

            var obj = await Utility.FillDataTableAsync<OTHERS_AMLOCDD_DATA>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return true;
            }
            return false;
        }
        public async Task<P_AML_CTF_MATRIX[]> GetP_AML_CTF_MATRIX(P_AML_CTF_MATRIX dataObject)
        {

            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();
            string sqlStr = @"SELECT * FROM POLICY.P_AML_CTF_MATRIX ";

            if (dataObject != null)
            {
                if (!string.IsNullOrEmpty(dataObject.FREEZE_FLG) && dataObject.FREEZE_FLG == "Y")
                {
                    listCondition.Add(" FREEZE_FLG= :vFREEZE_FLG");
                    Utility.SQLValueString(listParam, "vFREEZE_FLG", dataObject.FREEZE_FLG);
                }
                else if (!string.IsNullOrEmpty(dataObject.PEP_OUT_FLG) && dataObject.PEP_OUT_FLG == "Y")
                {
                    listCondition.Add(" PEP_OUT_FLG= :vPEP_OUT_FLG");
                    Utility.SQLValueString(listParam, "vPEP_OUT_FLG", dataObject.PEP_OUT_FLG);
                }
                else if (!string.IsNullOrEmpty(dataObject.NATIONALITY_SERIOUS_FLG) && dataObject.NATIONALITY_SERIOUS_FLG == "Y")
                {
                    listCondition.Add("  NATIONALITY_SERIOUS_FLG = :vNATIONALITY_SERIOUS_FLG");
                    Utility.SQLValueString(listParam, "vNATIONALITY_SERIOUS_FLG", dataObject.NATIONALITY_SERIOUS_FLG);
                }
                else if (!string.IsNullOrEmpty(dataObject.HR02_FLG) && dataObject.HR02_FLG == "Y")
                {
                    listCondition.Add("  HR02_FLG = :vHR02_FLG");
                    Utility.SQLValueString(listParam, "vHR02_FLG", dataObject.HR02_FLG);
                }
                else if (!string.IsNullOrEmpty(dataObject.HR08_FLG) && dataObject.HR08_FLG == "Y")
                {
                    listCondition.Add("  HR08_FLG = :vHR08_FLG");
                    Utility.SQLValueString(listParam, "vHR08_FLG", dataObject.HR08_FLG);
                }

                if (listCondition.Any())
                {
                    sqlStr += "\n\t WHERE " + string.Join("\n\tAND ", listCondition);
                }
                else
                {
                    if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "7");

                    }else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "8");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "9");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "10");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "11");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "Y" && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "12");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "13");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "14");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "15");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "16");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "17");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "18");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "19");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "20");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "Y" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "21");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "Y"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "22");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "23");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "24");

                    }
                    else if (dataObject.PEP_IN_FLG == "Y" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "Y")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "25");

                    }
                    else if (dataObject.PEP_IN_FLG == "N" && dataObject.OCCUPATION_FLG == "N" && dataObject.NATIONALITY_OTH_FLG == "N"  && dataObject.PRODUCT_FLG == "N")
                    {
                        listCondition.Add(" MX_ID = :vMX_ID");
                        Utility.SQLValueString(listParam, "vMX_ID", "26");

                    }

                    if (listCondition.Any())
                    {
                        sqlStr += "\n\t WHERE " + string.Join("\n\tAND ", listCondition);
                    }
                }

                var obj = await Utility.FillDataTableAsync<P_AML_CTF_MATRIX>(sqlStr, connection, listParam);
                if (obj != null && obj.Any())
                {
                    return obj;
                }
            }

            return null;

        }

        public async Task<P_POLICY_ID> GetDetailAppno(string appno,string policy) 
        {

            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();
            string sqlStr = @"   SELECT PID.POLICY,PID.POLICY_ID,PID.CHANNEL_TYPE,PAI.APP_NO,uapp.app_dt
             ,(PLID.PL_BLOCK ||PLID.PL_CODE || PLID.PL_CODE2) AS PLAN_CODE,plid.title PLAN_TITLE
             FROM POLICY.P_POLICY_ID PID, 
             POLICY.P_APPL_ID PAI,
             POLICY.P_LIFE_ID PLI,
            POLICY.ZTB_PLAN PLID,
             POLICY.U_APPLICATION_ID UAPPID,
             POLICY.U_APPLICATION UAPP
             WHERE 
              PAI.POLICY_ID = PID.POLICY_ID
             AND PAI.POLICY_ID = PLI.POLICY_ID
              AND PLI.pl_block = PLID.pl_block
              AND PLI.pl_code = PLID.PL_CODE
              AND PLI.pl_code2 = PLID.pl_code2
             AND PAI.APP_NO = UAPPID.APP_NO
              AND UAPPID.APP_ID=UAPP.APP_ID  ";


                if (!string.IsNullOrEmpty(appno))
                {
                    listCondition.Add(" PAI.APP_NO = :vAPP_NO");
                    Utility.SQLValueString(listParam, "vAPP_NO", appno);
                }

                if (!string.IsNullOrEmpty(policy))
                {
                    listCondition.Add(" PID.POLICY = :vPOLICY");
                    Utility.SQLValueString(listParam, "vPOLICY", policy);
                }

                if (listCondition.Any())
                {
                    sqlStr += "\n\t AND " + string.Join("\n\tAND ", listCondition);
                }

                var obj = await Utility.FillDataTableAsync<P_POLICY_ID>(sqlStr, connection, listParam);
                if (obj != null && obj.Any())
                {
                    return obj.FirstOrDefault();
                }
            

            return null;

        }
        public async Task<P_POL_NAME[]> GetP_POL_NAME(long? policy_Id)
        {
            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();

            if (policy_Id.HasValue)
            {
                listCondition.Add("PPN.POLICY_ID= :vPOLICY_ID ");
                Utility.SQLValueString(listParam, "vPOLICY_ID", policy_Id);
            }


            string sqlStr = @" SELECT PPN.NAME_ID,PPN.POLICY_ID,PNI.PRENAME,PNI.NAME,PNI.SURNAME,PNI.IDCARD_NO,PNI.PASSPORT,PNI.NATIONALITY
             FROM 
             POLICY.P_POL_NAME PPN,
            POLICY.P_NAME_ID PNI
            WHERE PPN.NAME_ID = PNI.NAME_ID
            AND PNI.TMN = 'N' ";
            if (listCondition.Any())
            {
                sqlStr += "\n\t AND " + string.Join("\n\tAND ", listCondition);
            }
            var obj = await Utility.FillDataTableAsync<P_POL_NAME>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return obj;
            }
            return null;
        }
        public async Task<P_POL_PARENT[]> GetP_POL_PARENT(long? policy_Id)
        {
            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();

            if (policy_Id.HasValue)
            {
                listCondition.Add("PPA.POLICY_ID = :vPOLICY_ID ");
                Utility.SQLValueString(listParam, "vPOLICY_ID", policy_Id);
            }


            string sqlStr = @" SELECT PPA.PARENT_ID,PPA.POLICY_ID,PPI.PRENAME,PPI.NAME,PPI.SURNAME,PPI.IDCARD_NO,PPI.PASSPORT,PPI.NATIONALITY
            FROM POLICY.P_POL_PARENT PPA, POLICY.P_PARENT_ID PPI
            WHERE PPA.PARENT_ID = PPI.PARENT_ID
            AND PPA.TMN = 'N' ";
            if (listCondition.Any())
            {
                sqlStr += "\n\t AND " + string.Join("\n\tAND ", listCondition);
            }
            var obj = await Utility.FillDataTableAsync<P_POL_PARENT>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return obj;
            }
            return null;
        }
        public async Task<P_POL_BENEFIT[]> GetP_POL_BENEFIT(long? policy_Id)
        {
            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();

            if (policy_Id.HasValue)
            {
                listCondition.Add("PPB.POLICY_ID = :vPOLICY_ID ");
                Utility.SQLValueString(listParam, "vPOLICY_ID", policy_Id);
            }


            string sqlStr = @" SELECT PPB.POLICY_ID,PPB.BENEFIT_ID,PBP.PRENAME,PBP.NAME,PBP.SURNAME,PBP.RELATION,PBP.GAIN_PERCENT,PBP.CARD_NO
            FROM POLICY.P_POL_BENEFIT PPB, POLICY.P_BENEFIT_ID PBI ,POLICY.P_BENEFIT_PERSON PBP
            WHERE PPB.BENEFIT_ID = PBI.BENEFIT_ID
            AND PBI.BENEFIT_ID = PBP.BENEFIT_ID
            AND PBI.TMN = 'N' ";
            if (listCondition.Any())
            {
                sqlStr += "\n\t AND " + string.Join("\n\tAND ", listCondition);
            }
            var obj = await Utility.FillDataTableAsync<P_POL_BENEFIT>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return obj;
            }
            return null;
        }
        public async Task<U_NAME_ID[]> GetU_NAME_ID(long? uApp_Id)
        {
            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();

            if (uApp_Id.HasValue)
            {
                listCondition.Add("UAPP_ID= :vUAPP_ID ");
                Utility.SQLValueString(listParam, "vUAPP_ID", uApp_Id);
            }


            string sqlStr = @"SELECT * FROM POLICY.U_NAME_ID ";
            if (listCondition.Any())
            {
                sqlStr += "\n\t WHERE " + string.Join("\n\tAND ", listCondition);
            }
            var obj = await Utility.FillDataTableAsync<U_NAME_ID>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return obj;
            }
            return null;
        }

        public async Task<U_APPLICATION> GetU_APPLICATION(string appNo)
        {
            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();

            if (!string.IsNullOrEmpty(appNo))
            {
                listCondition.Add("UAPPID.APP_NO= :vAPP_NO ");
                Utility.SQLValueString(listParam, "vAPP_NO", appNo);
            }


            string sqlStr = @"  SELECT  UAPP.UAPP_ID,UAPPID.APP_ID,UAPPID.APP_NO,uappid.channel_type,UAPP.APP_DT, 
              (PLID.PL_BLOCK ||PLID.PL_CODE || PLID.PL_CODE2) AS PLAN_CODE,plid.title PLAN_TITLE
              FROM POLICY.U_APPLICATION_ID UAPPID,
              POLICY.U_APPLICATION UAPP,
              POLICY.U_LIFE_ID ULI,
              POLICY.ZTB_PLAN PLID
              WHERE UAPPID.TMN='N' 
              AND UAPPID.APP_ID=UAPP.APP_ID 
              AND UAPP.UAPP_ID = ULI.UAPP_ID
              AND uli.pl_block = PLID.pl_block
              AND uli.pl_code = PLID.PL_CODE
              AND uli.pl_code2 = PLID.pl_code2 ";
            if (listCondition.Any())
            {
                sqlStr += "\n\t AND " + string.Join("\n\tAND ", listCondition);
            }
            var obj = await Utility.FillDataTableAsync<U_APPLICATION>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return obj.FirstOrDefault();
            }
            return null;
        }

        public async Task<U_APP_BENEFIT[]> GetU_APP_BENEFIT(long? uAppId)
        {
            List<string> listCondition = new List<string>();
            List<DBParameter> listParam = new List<DBParameter>();

            if (uAppId.HasValue)
            {
                listCondition.Add("UAB.UAPP_ID = :vUAPP_ID ");
                Utility.SQLValueString(listParam, "vUAPP_ID", uAppId);
            }


            string sqlStr = @" SELECT  UAB.UAPP_ID,UAB.SEQ,UAB.UBENEFIT_ID,UBP.PRENAME,UBP.NAME,UBP.SURNAME,UBP.RELATION,UBP.GAIN_PERCENT,UBP.CARD_NO
            FROM  POLICY.U_APP_BENEFIT UAB,
            POLICY.U_BENEFIT_PERSON UBP
            WHERE   UAB.UBENEFIT_ID = UBP.UBENEFIT_ID
            ";
            if (listCondition.Any())
            {
                sqlStr += "\n\t AND " + string.Join("\n\tAND ", listCondition);
            }
            var obj = await Utility.FillDataTableAsync<U_APP_BENEFIT>(sqlStr, connection, listParam);
            if (obj != null && obj.Any())
            {
                return obj;
            }
            return null;
        }

        public void AddP_AML_CTF(ref P_AML_CTF addObject)
        {
            string sqlStr;
            List<DBParameter> param = new List<DBParameter>();
            StringBuilder SQL = new StringBuilder();

            SQL.Append(" INSERT INTO \"POLICY\".P_AML_CTF ( ENTRY_DT , ENTRY_TIME , APP_NO , POLICY , POL_YR , POL_LT , P_MODE , PROC_FLG , CLASSIFIED_BY , CUST_FLG , PRENAME , NAME , SURNAME , IDCARD_NO , PASSPORT , BIRTH_DT , NATIONALITY , SUMM , BSC_PRM , RDR_PRM , PAYMENT_OPT , RISK_CLASS , SENDAUTH_DT , AMLO_AUTH , AUTH_DT , AMLO_SEND , AMLO_DT , FSYSTEM_DT , UPD_DT , UPD_ID , BNF_PAY , SAME_PS_SYSTEM_FLG , SAME_PS_SYSTEM_DT , SAME_PS_UND_FLG , SAME_PS_UND_DT , FREEZE_FLG , PEP_IN_FLG , PEP_OUT_FLG , RCA_IN_FLG , RCA_OUT_FLG , STR_FLG , HR02_FLG , HR08_FLG , NATION_SERIOUS_FLG , NATION_OTH_FLG , OCC_FLG , PRODUCT_FLG , SYSTEM_DT , POLICY_ID , SEQ , TMN , TMN_DT , TMN_ID )  ");
            SQL.Append(" VALUES  ( :vENTRY_DT , :vENTRY_TIME , :vAPP_NO , :vPOLICY , :vPOL_YR , :vPOL_LT , :vP_MODE , :vPROC_FLG , :vCLASSIFIED_BY , :vCUST_FLG , :vPRENAME , :vNAME , :vSURNAME , :vIDCARD_NO , :vPASSPORT , :vBIRTH_DT , :vNATIONALITY , :vSUMM , :vBSC_PRM , :vRDR_PRM , :vPAYMENT_OPT , :vRISK_CLASS , :vSENDAUTH_DT , :vAMLO_AUTH , :vAUTH_DT , :vAMLO_SEND , :vAMLO_DT , :vFSYSTEM_DT , :vUPD_DT , :vUPD_ID , :vBNF_PAY , :vSAME_PS_SYSTEM_FLG , :vSAME_PS_SYSTEM_DT , :vSAME_PS_UND_FLG , :vSAME_PS_UND_DT , :vFREEZE_FLG , :vPEP_IN_FLG , :vPEP_OUT_FLG , :vRCA_IN_FLG , :vRCA_OUT_FLG , :vSTR_FLG , :vHR02_FLG , :vHR08_FLG , :vNATION_SERIOUS_FLG , :vNATION_OTH_FLG , :vOCC_FLG , :vPRODUCT_FLG , :vSYSTEM_DT , :vPOLICY_ID , :vSEQ , :vTMN , :vTMN_DT , :vTMN_ID )  ");
            Utility.SQLValueString(param, "vENTRY_DT", addObject.ENTRY_DT);

            Utility.SQLValueString(param, "vENTRY_TIME", addObject.ENTRY_TIME);

            Utility.SQLValueString(param, "vAPP_NO", addObject.APP_NO);

            Utility.SQLValueString(param, "vPOLICY", addObject.POLICY);

            Utility.SQLValueString(param, "vPOL_YR", addObject.POL_YR);

            Utility.SQLValueString(param, "vPOL_LT", addObject.POL_LT);

            Utility.SQLValueString(param, "vP_MODE", addObject.P_MODE);

            Utility.SQLValueString(param, "vPROC_FLG", addObject.PROC_FLG);

            Utility.SQLValueString(param, "vCLASSIFIED_BY", addObject.CLASSIFIED_BY);

            Utility.SQLValueString(param, "vCUST_FLG", addObject.CUST_FLG);

            Utility.SQLValueString(param, "vPRENAME", addObject.PRENAME);

            Utility.SQLValueString(param, "vNAME", addObject.NAME);

            Utility.SQLValueString(param, "vSURNAME", addObject.SURNAME);

            Utility.SQLValueString(param, "vIDCARD_NO", addObject.IDCARD_NO);

            Utility.SQLValueString(param, "vPASSPORT", addObject.PASSPORT);

            Utility.SQLValueString(param, "vBIRTH_DT", addObject.BIRTH_DT);

            Utility.SQLValueString(param, "vNATIONALITY", addObject.NATIONALITY);

            Utility.SQLValueString(param, "vSUMM", addObject.SUMM);

            Utility.SQLValueString(param, "vBSC_PRM", addObject.BSC_PRM);

            Utility.SQLValueString(param, "vRDR_PRM", addObject.RDR_PRM);

            Utility.SQLValueString(param, "vPAYMENT_OPT", addObject.PAYMENT_OPT);

            Utility.SQLValueString(param, "vRISK_CLASS", addObject.RISK_CLASS);

            Utility.SQLValueString(param, "vSENDAUTH_DT", addObject.SENDAUTH_DT);

            Utility.SQLValueString(param, "vAMLO_AUTH", addObject.AMLO_AUTH);

            Utility.SQLValueString(param, "vAUTH_DT", addObject.AUTH_DT);

            Utility.SQLValueString(param, "vAMLO_SEND", addObject.AMLO_SEND);

            Utility.SQLValueString(param, "vAMLO_DT", addObject.AMLO_DT);

            Utility.SQLValueString(param, "vFSYSTEM_DT", addObject.FSYSTEM_DT);

            Utility.SQLValueString(param, "vUPD_DT", addObject.UPD_DT);

            Utility.SQLValueString(param, "vUPD_ID", addObject.UPD_ID);

            Utility.SQLValueString(param, "vBNF_PAY", addObject.BNF_PAY);

            Utility.SQLValueString(param, "vSAME_PS_SYSTEM_FLG", addObject.SAME_PS_SYSTEM_FLG);

            Utility.SQLValueString(param, "vSAME_PS_SYSTEM_DT", addObject.SAME_PS_SYSTEM_DT);

            Utility.SQLValueString(param, "vSAME_PS_UND_FLG", addObject.SAME_PS_UND_FLG);

            Utility.SQLValueString(param, "vSAME_PS_UND_DT", addObject.SAME_PS_UND_DT);

            Utility.SQLValueString(param, "vFREEZE_FLG", addObject.FREEZE_FLG);

            Utility.SQLValueString(param, "vPEP_IN_FLG", addObject.PEP_IN_FLG);

            Utility.SQLValueString(param, "vPEP_OUT_FLG", addObject.PEP_OUT_FLG);

            Utility.SQLValueString(param, "vRCA_IN_FLG", addObject.RCA_IN_FLG);

            Utility.SQLValueString(param, "vRCA_OUT_FLG", addObject.RCA_OUT_FLG);

            Utility.SQLValueString(param, "vSTR_FLG", addObject.STR_FLG);

            Utility.SQLValueString(param, "vHR02_FLG", addObject.HR02_FLG);

            Utility.SQLValueString(param, "vHR08_FLG", addObject.HR08_FLG);

            Utility.SQLValueString(param, "vNATION_SERIOUS_FLG", addObject.NATION_SERIOUS_FLG);

            Utility.SQLValueString(param, "vNATION_OTH_FLG", addObject.NATION_OTH_FLG);

            Utility.SQLValueString(param, "vOCC_FLG", addObject.OCC_FLG);

            Utility.SQLValueString(param, "vPRODUCT_FLG", addObject.PRODUCT_FLG);

            Utility.SQLValueString(param, "vSYSTEM_DT", addObject.SYSTEM_DT);

            Utility.SQLValueString(param, "vPOLICY_ID", addObject.POLICY_ID);

            Utility.SQLValueString(param, "vSEQ", addObject.SEQ);

            Utility.SQLValueString(param, "vTMN", addObject.TMN);

            Utility.SQLValueString(param, "vTMN_DT", addObject.TMN_DT);

            Utility.SQLValueString(param, "vTMN_ID", addObject.TMN_ID);


            int recordCount = Utility.ExecuteNonQuery(SQL.ToString(), connection, param);
        }

        public void AddP_AML_CTF_CHANNEL(ref P_AML_CTF_CHANNEL addObject)
        {
            string sqlStr;
            List<DBParameter> param = new List<DBParameter>();
            StringBuilder SQL = new StringBuilder();

            SQL.Append(" INSERT INTO \"POLICY\".P_AML_CTF_CHANNEL ( ENTRY_DT , ENTRY_TIME , APP_NO , POLICY , CHANNEL_TYPE , POLICY_HOLDING , ALL_POLICY , ALL_SUMM , ALL_PREMIUM )  ");
            SQL.Append(" VALUES  ( :vENTRY_DT , :vENTRY_TIME , :vAPP_NO , :vPOLICY , :vCHANNEL_TYPE , :vPOLICY_HOLDING , :vALL_POLICY , :vALL_SUMM , :vALL_PREMIUM )  ");
            Utility.SQLValueString(param, "vENTRY_DT", addObject.ENTRY_DT);

            Utility.SQLValueString(param, "vENTRY_TIME", addObject.ENTRY_TIME);

            Utility.SQLValueString(param, "vAPP_NO", addObject.APP_NO);

            Utility.SQLValueString(param, "vPOLICY", addObject.POLICY);

            Utility.SQLValueString(param, "vCHANNEL_TYPE", addObject.CHANNEL_TYPE);

            Utility.SQLValueString(param, "vPOLICY_HOLDING", addObject.POLICY_HOLDING);

            Utility.SQLValueString(param, "vALL_POLICY", addObject.ALL_POLICY);

            Utility.SQLValueString(param, "vALL_SUMM", addObject.ALL_SUMM);

            Utility.SQLValueString(param, "vALL_PREMIUM", addObject.ALL_PREMIUM);


            int recordCount = Utility.ExecuteNonQuery(SQL.ToString(), connection, param);
        }
        public void AddP_AML_CTF_REMARK(ref P_AML_CTF_REMARK addObject)
        {
            string sqlStr;
            List<DBParameter> param = new List<DBParameter>();
            StringBuilder SQL = new StringBuilder();

            SQL.Append(" INSERT INTO \"POLICY\".P_AML_CTF_REMARK ( APP_NO , POLICY_ID , REMARK )  ");
            SQL.Append(" VALUES  ( :vAPP_NO , :vPOLICY_ID , :vREMARK )  ");
            Utility.SQLValueString(param, "vAPP_NO", addObject.APP_NO);

            Utility.SQLValueString(param, "vPOLICY_ID", addObject.POLICY_ID);

            Utility.SQLValueString(param, "vREMARK", addObject.REMARK);


            int recordCount = Utility.ExecuteNonQuery(SQL.ToString(), connection, param);
        }
        public void AddP_AML_CTF_EDD(ref P_AML_CTF_EDD addObject)
        {
            string sqlStr;
            List<DBParameter> param = new List<DBParameter>();
            StringBuilder SQL = new StringBuilder();

            SQL.Append(" INSERT INTO \"POLICY\".P_AML_CTF_EDD ( APP_NO , POLICY_ID , NEWS_FLG , NEWS_REMARK , TH_IDCARD_FLG , NOTH_IDCARD_FLG , PAYMENT_INCOME_FLG , INCOME_SRC_FLG , SIGNATURE_FLG , INFORCE_FLG , INFORCE_REMARK , EFF_DT , UPD_ID , TMN , TMN_DT )  ");
            SQL.Append(" VALUES  ( :vAPP_NO , :vPOLICY_ID , :vNEWS_FLG , :vNEWS_REMARK , :vTH_IDCARD_FLG , :vNOTH_IDCARD_FLG , :vPAYMENT_INCOME_FLG , :vINCOME_SRC_FLG , :vSIGNATURE_FLG , :vINFORCE_FLG , :vINFORCE_REMARK , :vEFF_DT , :vUPD_ID , :vTMN , :vTMN_DT )  ");
            Utility.SQLValueString(param, "vAPP_NO", addObject.APP_NO);

            Utility.SQLValueString(param, "vPOLICY_ID", addObject.POLICY_ID);

            Utility.SQLValueString(param, "vNEWS_FLG", addObject.NEWS_FLG);

            Utility.SQLValueString(param, "vNEWS_REMARK", addObject.NEWS_REMARK);

            Utility.SQLValueString(param, "vTH_IDCARD_FLG", addObject.TH_IDCARD_FLG);

            Utility.SQLValueString(param, "vNOTH_IDCARD_FLG", addObject.NOTH_IDCARD_FLG);

            Utility.SQLValueString(param, "vPAYMENT_INCOME_FLG", addObject.PAYMENT_INCOME_FLG);

            Utility.SQLValueString(param, "vINCOME_SRC_FLG", addObject.INCOME_SRC_FLG);

            Utility.SQLValueString(param, "vSIGNATURE_FLG", addObject.SIGNATURE_FLG);

            Utility.SQLValueString(param, "vINFORCE_FLG", addObject.INFORCE_FLG);

            Utility.SQLValueString(param, "vINFORCE_REMARK", addObject.INFORCE_REMARK);

            Utility.SQLValueString(param, "vEFF_DT", addObject.EFF_DT);

            Utility.SQLValueString(param, "vUPD_ID", addObject.UPD_ID);

            Utility.SQLValueString(param, "vTMN", addObject.TMN);

            Utility.SQLValueString(param, "vTMN_DT", addObject.TMN_DT);


            int recordCount = Utility.ExecuteNonQuery(SQL.ToString(), connection, param);
        }


    }
}
