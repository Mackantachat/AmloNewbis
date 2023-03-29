using AmloNewbis.DataContract;
using AmloNewbis.DataContract.Amlo;
using AmloNewbisAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Library.Helper
{
    public static class ModelAdapterHelper
    {
        public static MoneyLaunderingRiskInfo ToAppMoneyLaunderingRiskInfo(this MoneyLaunderingRiskInfo_Request appDoc)
        {

            var model = new MoneyLaunderingRiskInfo()
            {
                Appno = appDoc.Appno,
                Policy = appDoc.Policy,
                Policy_Id = appDoc.Policy_Id,
                PlanId = appDoc.PlanId,
                RiskInfo = appDoc.RiskInfo?.Select(item => new AmloNewbis.DataContract.RiksInfo()
                {
                    //Appno = item.Appno,
                    IdCard = item.IdCard,
                    //PlanId = item.PlanId,
                    Name = item.Name,
                    SureName = item.SureName,
                    Nationality = item.Nationality,
                    PersonStatus = item.PersonStatus,


                }).ToArray()
            };
            return model;
        }
        public static AppNoInfo ToDataAppNoInfo(this P_APPL_ID_INFO p_APPL_ID_INFO)
        {

            AppNoInfo appInfo = new AppNoInfo();
            List<RiskInfo> lstRiskInfo = new List<RiskInfo>();
            appInfo.AppNo = p_APPL_ID_INFO.APP_NO;
            appInfo.PolicyId = p_APPL_ID_INFO.POLICY_ID;
            appInfo.Policy = p_APPL_ID_INFO.POLICY;
            appInfo.PlanCode = p_APPL_ID_INFO.PLANCODE;
            appInfo.AppDate = p_APPL_ID_INFO.APP_DT;
            appInfo.ChannelType = p_APPL_ID_INFO.CHANNEL_TYPE;
            appInfo.PlanDesc = p_APPL_ID_INFO.PLAN_DESC;

            foreach (var item in p_APPL_ID_INFO.RISK_INFOs)
            {
                RiskInfo rowRisk = new RiskInfo();
                rowRisk.Name = item.NAME;
                rowRisk.SureName = item.SURE_NAME;
                rowRisk.IdCard = item.ID_CARD;
                rowRisk.Nationality = item.NATIONALITY;
                rowRisk.PersonStatus = item.PERSON_STATUS;
                lstRiskInfo.Add(rowRisk);
            }
            if (lstRiskInfo.Any())
            {
                appInfo.RiskInfos = lstRiskInfo.ToArray();
            }
            return appInfo;
        }
        public static AMLOCDD_INFO ToAmlocdDataInfo(this AmlocddInfo_Request amloInfo)
        {
            #region :: Variables ::
            AMLOCDD_INFO AmloResponse = new AMLOCDD_INFO();
            CUSTOMER_INFO _CustomerInfo = new CUSTOMER_INFO();
            PAYER_INFO _PayerInfo = new PAYER_INFO();
            List<BENEFIT_INFO> lstBenefitInfo = new List<BENEFIT_INFO>();
            RISK_OTHERs riskOther = new RISK_OTHERs();
            BENEFIT_INFO bENEFIT_INFO = new BENEFIT_INFO();
            #endregion
            #region :: Customer Risk ::
            List<RISK_AMLO> lstRiskAmlo = new List<RISK_AMLO>();
            List<RISK_OTHERs> lstRiskOthers = new List<RISK_OTHERs>();
            List<VERIFY_SYSTEM_INFO> lstVerifySystemInfos = new List<VERIFY_SYSTEM_INFO>();
            List<VERIFY_USER_INFO> lstVerifyUserInfos = new List<VERIFY_USER_INFO>();

            foreach (var item in amloInfo.CustomerInfo.RiskAmlos)
            {
                RISK_AMLO riskAMLO = new RISK_AMLO();
                riskAMLO.Seq = item.Seq;
                riskAMLO.RISK_STATUS = item.RiskStatus ? "Y" : "N";
                riskAMLO.RISK_DESC = item.RiskDesc;
                riskAMLO.RISK_CODE = item.Code;
                lstRiskAmlo.Add(riskAMLO);
            }

            foreach (var item in amloInfo.CustomerInfo.RiskOthers)
            {
                RISK_OTHERs riskAMLO_Other = new RISK_OTHERs();
                riskAMLO_Other.Seq = item.Seq;
                riskAMLO_Other.RISK_STATUS = item.RiskStatus ? "Y" : "N";
                riskAMLO_Other.RISK_DESC = item.RiskDesc;
                riskAMLO_Other.RISK_CODE = item.Code;
                lstRiskOthers.Add(riskAMLO_Other);
            }

            if (amloInfo.CustomerInfo.VerifySystem != null && amloInfo.CustomerInfo.VerifySystem.Any())
            {
                foreach (var item in amloInfo.CustomerInfo.VerifySystem)
                {
                    VERIFY_SYSTEM_INFO verifySystemInfo = new VERIFY_SYSTEM_INFO();
                    verifySystemInfo.STATUS = item.Status ? "Y" : "N";
                    verifySystemInfo.DESC_SYSTEM = item.DescSystem;
                    verifySystemInfo.VERIFY_SYSTEM_DATE = item.VerifySystemDate;
                    lstVerifySystemInfos.Add(verifySystemInfo);
                }
            }
            if (amloInfo.CustomerInfo.VerifyUser != null && amloInfo.CustomerInfo.VerifyUser.Any())
            {
                foreach (var item in amloInfo.CustomerInfo.VerifyUser)
                {
                    VERIFY_USER_INFO verifyUserInfo = new VERIFY_USER_INFO();
                    verifyUserInfo.STATUS = item.Status ? "Y" : "N";
                    verifyUserInfo.DESC_USER = item.DescUser;
                    verifyUserInfo.VERIFY_USER_DATE = item.VerifyUserDate;
                    lstVerifyUserInfos.Add(verifyUserInfo);
                }
            }
            _CustomerInfo.PRE_NAME = amloInfo.CustomerInfo.PreName;
            _CustomerInfo.NAME = amloInfo.CustomerInfo.Name;
            _CustomerInfo.SURE_NAME = amloInfo.CustomerInfo.SureName;
            _CustomerInfo.ID_CARD = amloInfo.CustomerInfo.IdCard;
            _CustomerInfo.NATIONALITY = amloInfo.CustomerInfo.Nationality;
            _CustomerInfo.PERSON_STATUS = amloInfo.CustomerInfo.PersonStatus;
            _CustomerInfo.VERIFY_SYSTEM = lstVerifySystemInfos.ToArray();
            _CustomerInfo.VERIFY_USER = lstVerifyUserInfos.ToArray();
            _CustomerInfo.RISK_AMLOs = lstRiskAmlo.Any() ? lstRiskAmlo.ToArray() : null;
            _CustomerInfo.RISK_OTHERs = lstRiskOthers.Any() ? lstRiskOthers.ToArray() : null;
            #endregion
            #region :: Payer Risk ::
            if (amloInfo.PayerInfo != null)
            {
                lstRiskAmlo = new List<RISK_AMLO>();
                lstRiskOthers = new List<RISK_OTHERs>();
                lstVerifyUserInfos = new List<VERIFY_USER_INFO>();
                lstVerifySystemInfos = new List<VERIFY_SYSTEM_INFO>();
                foreach (var item in amloInfo.PayerInfo.RiskAmlos)
                {
                    RISK_AMLO riskAMLO = new RISK_AMLO();
                    riskAMLO.Seq = item.Seq;
                    riskAMLO.RISK_STATUS = item.RiskStatus ? "Y" : "N";
                    riskAMLO.RISK_DESC = item.RiskDesc;
                    riskAMLO.RISK_CODE = item.Code;
                    lstRiskAmlo.Add(riskAMLO);
                }
                foreach (var item in amloInfo.PayerInfo.RiskOthers)
                {
                    RISK_OTHERs riskAMLO_Other = new RISK_OTHERs();
                    riskAMLO_Other.Seq = item.Seq;
                    riskAMLO_Other.RISK_STATUS = item.RiskStatus ? "Y" : "N";
                    riskAMLO_Other.RISK_DESC = item.RiskDesc;
                    riskAMLO_Other.RISK_CODE = item.Code;
                    lstRiskOthers.Add(riskAMLO_Other);
                }
                if (amloInfo.PayerInfo.VerifySystem != null && amloInfo.PayerInfo.VerifySystem.Any())
                {
                    foreach (var item in amloInfo.PayerInfo.VerifySystem)
                    {
                        VERIFY_SYSTEM_INFO verifySystemInfo = new VERIFY_SYSTEM_INFO();
                        verifySystemInfo.STATUS = item.Status ? "Y" : "N";
                        verifySystemInfo.DESC_SYSTEM = item.DescSystem;
                        verifySystemInfo.VERIFY_SYSTEM_DATE = item.VerifySystemDate;
                        lstVerifySystemInfos.Add(verifySystemInfo);
                    }
                }
                if (amloInfo.PayerInfo.VerifyUser != null && amloInfo.PayerInfo.VerifyUser.Any())
                {
                    foreach (var item in amloInfo.PayerInfo.VerifyUser)
                    {
                        VERIFY_USER_INFO verifyUserInfo = new VERIFY_USER_INFO();
                        verifyUserInfo.STATUS = item.Status ? "Y" : "N";
                        verifyUserInfo.DESC_USER = item.DescUser;
                        verifyUserInfo.VERIFY_USER_DATE = item.VerifyUserDate;
                        lstVerifyUserInfos.Add(verifyUserInfo);
                    }
                }
                _PayerInfo.PRE_NAME = amloInfo.PayerInfo.PreName;
                _PayerInfo.NAME = amloInfo.PayerInfo.Name;
                _PayerInfo.SURE_NAME = amloInfo.PayerInfo.SureName;
                _PayerInfo.ID_CARD = amloInfo.PayerInfo.IdCard;
                _PayerInfo.NATIONALITY = amloInfo.PayerInfo.Name;
                _PayerInfo.PERSON_STATUS = amloInfo.PayerInfo.PersonStatus;
                _PayerInfo.VERIFY_SYSTEM = lstVerifySystemInfos.ToArray();
                _PayerInfo.VERIFY_USER = lstVerifyUserInfos.ToArray();
                _PayerInfo.RISK_AMLOs = lstRiskAmlo.Any() ? lstRiskAmlo.ToArray() : null;
                _PayerInfo.RISK_OTHERs = lstRiskOthers.Any() ? lstRiskOthers.ToArray() : null;
            }




            #endregion
            #region :: Benefit Risk ::
            if (amloInfo.BenefitInfo != null && amloInfo.BenefitInfo.Any())
            {
                foreach (var item in amloInfo.BenefitInfo)
                {
                    lstRiskAmlo = new List<RISK_AMLO>();
                    lstVerifyUserInfos = new List<VERIFY_USER_INFO>();
                    lstVerifySystemInfos = new List<VERIFY_SYSTEM_INFO>();
                    BENEFIT_INFO row = new BENEFIT_INFO();

                    foreach (var riskAmlo in item.RiskAmlos)
                    {
                        RISK_AMLO riskAMLO = new RISK_AMLO();
                        riskAMLO.Seq = riskAmlo.Seq;
                        riskAMLO.RISK_STATUS = riskAmlo.RiskStatus ? "Y" : "N";
                        riskAMLO.RISK_DESC = riskAmlo.RiskDesc;
                        riskAMLO.RISK_CODE = riskAmlo.Code;
                        lstRiskAmlo.Add(riskAMLO);
                    }
                    if (lstRiskAmlo.Any())
                    {
                        row.RISK_AMLOs = lstRiskAmlo.ToArray();
                    }
                    if (item.VerifySystem != null && item.VerifySystem.Any())
                    {
                        foreach (var rowVerifySystem in item.VerifySystem)
                        {
                            VERIFY_SYSTEM_INFO verifySystemInfo = new VERIFY_SYSTEM_INFO();
                            verifySystemInfo.STATUS = rowVerifySystem.Status ? "Y" : "N";
                            verifySystemInfo.DESC_SYSTEM = rowVerifySystem.DescSystem;
                            verifySystemInfo.VERIFY_SYSTEM_DATE = rowVerifySystem.VerifySystemDate;
                            lstVerifySystemInfos.Add(verifySystemInfo);
                        }
                    }
                    if (item.VerifyUser != null && item.VerifyUser.Any())
                    {
                        foreach (var rowVerifyUser in item.VerifyUser)
                        {
                            VERIFY_USER_INFO verifyUserInfo = new VERIFY_USER_INFO();
                            verifyUserInfo.STATUS = rowVerifyUser.Status ? "Y" : "N";
                            verifyUserInfo.DESC_USER = rowVerifyUser.DescUser;
                            verifyUserInfo.VERIFY_USER_DATE = rowVerifyUser.VerifyUserDate;
                            lstVerifyUserInfos.Add(verifyUserInfo);
                        }
                    }
                    row.PRE_NAME = item.PreName;
                    row.NAME = item.Name;
                    row.SURE_NAME = item.SureName;
                    row.ID_CARD = item.IdCard;
                    row.NATIONALITY = item.Nationality;
                    row.PERSON_STATUS = item.PersonStatus;
                    row.VERIFY_SYSTEM = lstVerifySystemInfos.ToArray();
                    row.VERIFY_USER = lstVerifyUserInfos.ToArray();
                    row.FULLNAME = item.FullName;
                    lstBenefitInfo.Add(row);
                }

            }

            #endregion

            List<RISK_MATRIX> lstRiskMatrix = new List<RISK_MATRIX>();
            foreach (var item in amloInfo.RiskMatrix)
            {
                RISK_MATRIX rowMatrix = new RISK_MATRIX();
                rowMatrix.CODE = item.Code;
                rowMatrix.DESC_MATRIX = item.DescMatrix;
                rowMatrix.STATUS = item.Status ? "Y" : "N";
                lstRiskMatrix.Add(rowMatrix);
            }
            UNDERWRITING_CONSIDERATIONS underwrittingConsiderations = new UNDERWRITING_CONSIDERATIONS();

            List<EDD_FORM> lstEddForm = new List<EDD_FORM>();
            List<SUMMARY_OF_INSURANCE_CONSIDERATIONS> lstSummaryOfInsuranceConsiderations = new List<SUMMARY_OF_INSURANCE_CONSIDERATIONS>();
            foreach (var item in amloInfo.UnderWritingConsideration.EddForms)
            {
                EDD_FORM rowEddForm = new EDD_FORM();
                rowEddForm.CODE = item.Code;
                rowEddForm.DESC_EDD_FORM = item.DescEddForm;
                rowEddForm.STATUS = item.Status ? "Y" : "N";
                rowEddForm.REMARK = item.Remark;
                lstEddForm.Add(rowEddForm);
            }
            foreach (var item in amloInfo.UnderWritingConsideration.SummaryOfInsuranceConsiderations)
            {
                SUMMARY_OF_INSURANCE_CONSIDERATIONS rowSummaryOfInsuranceConsideration = new SUMMARY_OF_INSURANCE_CONSIDERATIONS();
                rowSummaryOfInsuranceConsideration.CODE = item.Code;
                rowSummaryOfInsuranceConsideration.DESC_EDD_FORM = item.DescEddForm;
                rowSummaryOfInsuranceConsideration.STATUS = item.Status ? "Y" : "N";
                rowSummaryOfInsuranceConsideration.REMARK = item.Remark;
                lstSummaryOfInsuranceConsiderations.Add(rowSummaryOfInsuranceConsideration);
            }


            underwrittingConsiderations.EDD_FORMs = lstEddForm.ToArray();
            underwrittingConsiderations.SUMMARY_OF_INSURANCE_CONSIDERATIONs = lstSummaryOfInsuranceConsiderations.ToArray();

            AmloResponse.CUSTOMER_INFO = _CustomerInfo;
            AmloResponse.PAYER_INFO = _PayerInfo;
            AmloResponse.APP_NO = amloInfo.AppNo;
            AmloResponse.PLAN_ID = amloInfo.PlanId;
            AmloResponse.UNDERWRITING_CONSIDERATIONS = underwrittingConsiderations;
            AmloResponse.VERIFY_RISK = amloInfo.VerifyRisk;
            AmloResponse.VERIFY_RISK_DATE = amloInfo.VerifyRiskDate;
            AmloResponse.RISK_REMARK = amloInfo.RiskRemark;
            AmloResponse.USER_ID = amloInfo.UserId;
            if (lstBenefitInfo.Any())
            {
                AmloResponse.BENEFIT_INFO = lstBenefitInfo.ToArray();
            }

            return AmloResponse;
        }
        public static AmlocddInfo_Response ToAmlocddDataResponse(this AMLOCDD_INFO amloInfo)
        {
            #region :: Variables ::
            AmlocddInfo_Response AmloResponse = new AmlocddInfo_Response();
           
            PayerInfoResponse _PayerInfo = new PayerInfoResponse();
            List<BenefitInfoResponse> lstBenefitInfo = new List<BenefitInfoResponse>();
            RiskOthers riskOther = new RiskOthers();
            BenefitInfoResponse bENEFIT_INFO = new BenefitInfoResponse();
            #endregion
            #region :: Customer Risk ::
            List<RiskAmlo> lstRiskAmlo = new List<RiskAmlo>();
            List<RiskOthers> lstRiskOthers = new List<RiskOthers>();
            List<VerifySystemInfo> lstVerifySystemInfos = new List<VerifySystemInfo>();
            List<VerifyUserInfo> lstVerifyUserInfos = new List<VerifyUserInfo>();
            
            if (amloInfo.CUSTOMER_INFO != null)
            {
                CustomerInfoResponse _CustomerInfo = new CustomerInfoResponse();
                if (amloInfo.CUSTOMER_INFO.RISK_AMLOs != null && amloInfo.CUSTOMER_INFO.RISK_AMLOs.Any())
                {
                    foreach (var item in amloInfo.CUSTOMER_INFO.RISK_AMLOs)
                    {
                        RiskAmlo riskAMLO = new RiskAmlo();
                        riskAMLO.Seq = item.Seq;
                        riskAMLO.RiskStatus = item.RISK_STATUS == "Y";
                        riskAMLO.RiskDesc = item.RISK_DESC;
                        riskAMLO.Code = item.RISK_CODE;
                        lstRiskAmlo.Add(riskAMLO);
                    }
                }

                if (amloInfo.CUSTOMER_INFO.RISK_OTHERs != null && amloInfo.CUSTOMER_INFO.RISK_OTHERs.Any())
                {
                    foreach (var item in amloInfo.CUSTOMER_INFO.RISK_OTHERs)
                    {
                        RiskOthers riskAMLO = new RiskOthers();
                        riskAMLO.Seq = item.Seq;
                        riskAMLO.RiskStatus = item.RISK_STATUS == "Y";
                        riskAMLO.RiskDesc = item.RISK_DESC;
                        riskAMLO.Code = item.RISK_CODE;
                        lstRiskOthers.Add(riskAMLO);
                    }
                }
                if (amloInfo.CUSTOMER_INFO.VERIFY_SYSTEM != null && amloInfo.CUSTOMER_INFO.VERIFY_SYSTEM.Any())
                {
                    foreach (var item in amloInfo.CUSTOMER_INFO.VERIFY_SYSTEM)
                    {
                        VerifySystemInfo verifySystemInfo = new VerifySystemInfo();
                        verifySystemInfo.Status = item.STATUS == "Y";
                        verifySystemInfo.DescSystem = item.DESC_SYSTEM;
                        verifySystemInfo.VerifySystemDate = item.VERIFY_SYSTEM_DATE;
                        lstVerifySystemInfos.Add(verifySystemInfo);
                    }
                }
                if (amloInfo.CUSTOMER_INFO.VERIFY_USER != null && amloInfo.CUSTOMER_INFO.VERIFY_USER.Any())
                {
                    foreach (var item in amloInfo.CUSTOMER_INFO.VERIFY_USER)
                    {
                        VerifyUserInfo verifyUserInfo = new VerifyUserInfo();
                        verifyUserInfo.Status = item.STATUS == "Y";
                        verifyUserInfo.DescUser = item.DESC_USER;
                        verifyUserInfo.VerifyUserDate = item.VERIFY_USER_DATE;
                        lstVerifyUserInfos.Add(verifyUserInfo);
                    }
                }

                _CustomerInfo.PreName = amloInfo.CUSTOMER_INFO.PRE_NAME;
                _CustomerInfo.Name = amloInfo.CUSTOMER_INFO.NAME;
                _CustomerInfo.SureName = amloInfo.CUSTOMER_INFO.SURE_NAME;
                _CustomerInfo.IdCard = amloInfo.CUSTOMER_INFO.ID_CARD;
                _CustomerInfo.Nationality = amloInfo.CUSTOMER_INFO.NATIONALITY;
                _CustomerInfo.PersonStatus = amloInfo.CUSTOMER_INFO.PERSON_STATUS;
                _CustomerInfo.VerifySystem = lstVerifySystemInfos.ToArray();
                _CustomerInfo.VerifyUser = lstVerifyUserInfos.ToArray() ;
                _CustomerInfo.RiskAmlos = lstRiskAmlo.Any() ? lstRiskAmlo.ToArray() : null;
                _CustomerInfo.RiskOthers = lstRiskOthers.Any() ? lstRiskOthers.ToArray() : null;
                AmloResponse.CustomerInfo = _CustomerInfo;
            }
          
           
            #endregion
            #region :: Payer Risk ::

            if (amloInfo.PAYER_INFO != null)
            {
                lstRiskAmlo = new List<RiskAmlo>();
                lstRiskOthers = new List<RiskOthers>();
                lstVerifyUserInfos = new List<VerifyUserInfo>();
                lstVerifySystemInfos = new List<VerifySystemInfo>();
                foreach (var item in amloInfo.PAYER_INFO.RISK_AMLOs)
                {
                    RiskAmlo riskAMLO = new RiskAmlo();
                    riskAMLO.Seq = item.Seq;
                    riskAMLO.RiskStatus = item.RISK_STATUS == "Y";
                    riskAMLO.RiskDesc = item.RISK_DESC;
                    riskAMLO.Code = item.RISK_CODE;
                    lstRiskAmlo.Add(riskAMLO);
                }
                foreach (var item in amloInfo.PAYER_INFO.RISK_OTHERs)
                {
                    RiskOthers riskAmloOther = new RiskOthers();
                    riskAmloOther.Seq = item.Seq;
                    riskAmloOther.RiskStatus = item.RISK_STATUS == "Y";
                    riskAmloOther.RiskDesc = item.RISK_DESC;
                    riskAmloOther.Code = item.RISK_CODE;
                    lstRiskOthers.Add(riskAmloOther);
                }
                if (amloInfo.PAYER_INFO.VERIFY_SYSTEM != null && amloInfo.PAYER_INFO.VERIFY_SYSTEM.Any())
                {
                    foreach (var item in amloInfo.PAYER_INFO.VERIFY_SYSTEM)
                    {
                        VerifySystemInfo verifySystemInfo = new VerifySystemInfo();
                        verifySystemInfo.Status = item.STATUS == "Y";
                        verifySystemInfo.DescSystem = item.DESC_SYSTEM;
                        verifySystemInfo.VerifySystemDate = item.VERIFY_SYSTEM_DATE;
                        lstVerifySystemInfos.Add(verifySystemInfo);
                    }
                }
                if (amloInfo.PAYER_INFO.VERIFY_USER != null && amloInfo.PAYER_INFO.VERIFY_USER.Any())
                {
                    foreach (var item in amloInfo.CUSTOMER_INFO.VERIFY_USER)
                    {
                        VerifyUserInfo verifyUserInfo = new VerifyUserInfo();
                        verifyUserInfo.Status = item.STATUS == "Y";
                        verifyUserInfo.DescUser = item.DESC_USER;
                        verifyUserInfo.VerifyUserDate = item.VERIFY_USER_DATE;
                        lstVerifyUserInfos.Add(verifyUserInfo);
                    }
                }
                _PayerInfo.PreName = amloInfo.PAYER_INFO.PRE_NAME;
                _PayerInfo.Name = amloInfo.PAYER_INFO.NAME;
                _PayerInfo.SureName = amloInfo.PAYER_INFO.SURE_NAME;
                _PayerInfo.IdCard = amloInfo.PAYER_INFO.ID_CARD;
                _PayerInfo.Nationality = amloInfo.PAYER_INFO.NATIONALITY;
                _PayerInfo.PersonStatus = amloInfo.PAYER_INFO.PERSON_STATUS;
                _PayerInfo.VerifySystem = lstVerifySystemInfos.ToArray();
                _PayerInfo.VerifyUser = lstVerifyUserInfos.ToArray();
                _PayerInfo.RiskAmlos = lstRiskAmlo.Any() ? lstRiskAmlo.ToArray() : null;
                _PayerInfo.RiskOthers = lstRiskOthers.Any() ? lstRiskOthers.ToArray() : null;
                AmloResponse.PayerInfo = _PayerInfo;
            }




            #endregion
            #region :: Benefit Risk ::
            if (amloInfo.BENEFIT_INFO != null && amloInfo.BENEFIT_INFO.Any())
            {
                foreach (var item in amloInfo.BENEFIT_INFO)
                {
                    lstRiskAmlo = new List<RiskAmlo>();
                    lstVerifyUserInfos = new List<VerifyUserInfo>();
                    lstVerifySystemInfos = new List<VerifySystemInfo>();
                    BenefitInfoResponse row = new BenefitInfoResponse();

                    foreach (var riskAmlo in item.RISK_AMLOs)
                    {
                        RiskAmlo riskAMLO = new RiskAmlo();
                        riskAMLO.Seq = riskAmlo.Seq;
                        riskAMLO.RiskStatus = riskAmlo.RISK_STATUS == "Y";
                        riskAMLO.RiskDesc = riskAmlo.RISK_DESC;
                        riskAMLO.Code = riskAmlo.RISK_CODE;
                        lstRiskAmlo.Add(riskAMLO);
                    }
                    if (lstRiskAmlo.Any())
                    {
                        row.RiskAmlos = lstRiskAmlo.ToArray();
                    }
                    if (item.VERIFY_SYSTEM != null && item.VERIFY_SYSTEM.Any())
                    {
                        foreach (var rowVerifySystem in item.VERIFY_SYSTEM)
                        {
                            VerifySystemInfo verifySystemInfo = new VerifySystemInfo();
                            verifySystemInfo.Status = rowVerifySystem.STATUS == "Y";
                            verifySystemInfo.DescSystem = rowVerifySystem.DESC_SYSTEM;
                            verifySystemInfo.VerifySystemDate = rowVerifySystem.VERIFY_SYSTEM_DATE;
                            lstVerifySystemInfos.Add(verifySystemInfo);
                        }
                    }
                    if (item.VERIFY_USER != null && item.VERIFY_USER.Any())
                    {
                        foreach (var rowVerifyUserInfo in item.VERIFY_USER)
                        {
                            VerifyUserInfo verifyUserInfo = new VerifyUserInfo();
                            verifyUserInfo.Status = rowVerifyUserInfo.STATUS == "Y";
                            verifyUserInfo.DescUser = rowVerifyUserInfo.DESC_USER;
                            verifyUserInfo.VerifyUserDate = rowVerifyUserInfo.VERIFY_USER_DATE;
                            lstVerifyUserInfos.Add(verifyUserInfo);
                        }
                    }
                    row.PreName = item.PRE_NAME;
                    row.Name = item.NAME;
                    row.SureName = item.SURE_NAME;
                    row.IdCard = item.ID_CARD;
                    row.Nationality = item.NATIONALITY;
                    row.PersonStatus = item.PERSON_STATUS;
                    row.VerifySystem = lstVerifySystemInfos.ToArray();
                    row.VerifyUser = lstVerifyUserInfos.ToArray();
                    row.FullName = item.FULLNAME;
                    lstBenefitInfo.Add(row);
                }

            }

            #endregion
            List<RiskMatrix> lstRiskMatrix = new List<RiskMatrix>();
            foreach (var item in amloInfo.RISK_MATRIX)
            {
                RiskMatrix rowMatrix = new RiskMatrix();
                rowMatrix.Code = item.CODE;
                rowMatrix.DescMatrix = item.DESC_MATRIX;
                rowMatrix.Status = item.STATUS == "Y";
                lstRiskMatrix.Add(rowMatrix);
            }
            UnderwrittingConsiderations underwrittingConsiderations = new UnderwrittingConsiderations();
            
            List<EddForm> lstEddForm = new List<EddForm>();
            List<SummaryOfInsuranceConsideration> lstSummaryOfInsuranceConsiderations = new List<SummaryOfInsuranceConsideration>();
            foreach (var item in amloInfo.UNDERWRITING_CONSIDERATIONS.EDD_FORMs)
            {
                EddForm rowEddForm = new EddForm();
                rowEddForm.Code = item.CODE;
                rowEddForm.DescEddForm = item.DESC_EDD_FORM;
                rowEddForm.Status = item.STATUS == "Y";
                rowEddForm.Remark = item.REMARK;
                lstEddForm.Add(rowEddForm);
            }
            foreach (var item in amloInfo.UNDERWRITING_CONSIDERATIONS.SUMMARY_OF_INSURANCE_CONSIDERATIONs)
            {
                SummaryOfInsuranceConsideration rowSummaryOfInsuranceConsideration = new SummaryOfInsuranceConsideration();
                rowSummaryOfInsuranceConsideration.Code = item.CODE;
                rowSummaryOfInsuranceConsideration.DescEddForm = item.DESC_EDD_FORM;
                rowSummaryOfInsuranceConsideration.Status = item.STATUS == "Y";
                rowSummaryOfInsuranceConsideration.Remark = item.REMARK;
                lstSummaryOfInsuranceConsiderations.Add(rowSummaryOfInsuranceConsideration);
            }

            underwrittingConsiderations.EddStatus = amloInfo.UNDERWRITING_CONSIDERATIONS.EDD_STATUS == "Y";
            underwrittingConsiderations.EDDWarning = amloInfo.UNDERWRITING_CONSIDERATIONS.EDD_Warning;
            underwrittingConsiderations.EddForms = lstEddForm.ToArray();
            underwrittingConsiderations.SummaryOfInsuranceConsiderations = lstSummaryOfInsuranceConsiderations.ToArray();

            AmloResponse.AppNo = amloInfo.APP_NO;
            AmloResponse.PlanId = amloInfo.PLAN_ID;
            AmloResponse.UnderWritingConsideration = underwrittingConsiderations;
            AmloResponse.RiskMatrix = lstRiskMatrix.ToArray();
            AmloResponse.VerifyRisk = amloInfo.VERIFY_RISK == "Y";
            AmloResponse.VerifyRiskDate = amloInfo.VERIFY_RISK_DATE;
            AmloResponse.RiskRemark = amloInfo.RISK_REMARK;
            AmloResponse.UserId = amloInfo.USER_ID;

            if (lstBenefitInfo.Any())
            {
                AmloResponse.BenefitInfo = lstBenefitInfo.ToArray();
            }

            return AmloResponse;
        }
    }
}
