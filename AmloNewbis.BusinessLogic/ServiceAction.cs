using AmloNewbis.DataAccess;
using AmloNewbis.DataContract;
using AmloNewbis.DataContract.Amlo;
using AmloNewbis.DataContract.ENUM;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmloNewbis.BusinessLogic
{
    public partial class ServiceAction
    {
        private readonly DBSettingModel _dbSetting = null;

        public ServiceAction(DBSettingModel dataSetting)
        {
            this._dbSetting = dataSetting;

        }
        public async Task<AMLOCDD_INFO> GetInfoForAmloNewbis(MoneyLaunderingRiskInfo moneyLaunderingRisks_Info)
        {
            var Repo = new Repository(_dbSetting.ConnectionString);
            Repo.OpenConnection();
            string _Freezestatus = "N";
            string _Pepinstatus = "N";
            string _Pepoutstatus = "N";
            string _Rcainstatus = "N";
            string _Rcaoutstatus = "N";
            string _Strstatus = "N";
            string _Hr08status = "N";
            string _Hr02status = "N";
            string _NTstatus = "N";
            string _ONTstatus = "N";
            string _OCCstatus = "N";
            string _PRDstatus = "N";
            string _Benefitstatus = "N";
            string eddFormFLG = "Y";
            string matrixRiskLevel = "1";
            string approverMatrix = null;
            try
            {
                //AMLOCDD_INFO aMLOCDD_INFO = new AMLOCDD_INFO();


                AMLOCDD_INFO rowAMLOCDD = new AMLOCDD_INFO();
                List<BENEFIT_INFO> lstBENEFIT_INFOs = new List<BENEFIT_INFO>();
                List<RISK_MATRIX> lstRiskMatrix = new List<RISK_MATRIX>();
                List<P_AML_CTF_MATRIX> lstAmlCTFMatrix = new List<P_AML_CTF_MATRIX>();
                
                foreach (var item in moneyLaunderingRisks_Info.RiskInfo)
                {
                    CUSTOMER_INFO _customerInfo = new CUSTOMER_INFO();
                    List<RISK_AMLO> lstRiskAmlo = new List<RISK_AMLO>();
                    List<RISK_OTHERs> lstRiskOthers = new List<RISK_OTHERs>();
                    RISK_AMLO riskAMLO = new RISK_AMLO();
                    RISK_OTHERs riskOther = new RISK_OTHERs();
                    BENEFIT_INFO bENEFIT_INFO = new BENEFIT_INFO();
                    bool chkFreeze = false;
                    bool chkPEP_In = false;
                    bool chkPEP_Out = false;
                    bool chkRCA_In = false;
                    bool chkRCA_Out = false;
                    bool chkSTR = false;
                    bool chkHR_08 = false;
                    bool chkHR_02 = false;

                    bool chkNT = false;
                    bool chkONT = false;
                    bool chkOCC = false;
                    bool chkPRD = false;
                    AMLOCDD_DATA_BATCH[] dataAmlocdd = null;

                    /* var chkFlg = await Repo.GetCDD_PKG_CHK_DATA_SUSPECT(item.Name, item.SureName, item.IdCard);

                     if (chkFlg == "Y")
                     {
                          dataAmlocdd = await Repo.GetAmlocdd_Data_Batch(item.Name, item.SureName, item.IdCard,1, null);
                     }
                    */
                    if (string.IsNullOrEmpty(item.PersonStatus))
                    {
                        throw new Exception("ระบุประเภทของบุคคล");
                    }

                    dataAmlocdd = await Repo.GetAmlocdd_Data_Batch(item.Name, item.SureName, item.IdCard, 1, null);
                    if (dataAmlocdd == null)
                    {
                        dataAmlocdd = await Repo.GetAmlocdd_Data_Batch(item.Name, item.SureName, item.IdCard, 2, null);
                        if (dataAmlocdd == null)
                        {
                            dataAmlocdd = await Repo.GetAmlocdd_Data_Batch(item.Name, item.SureName, item.IdCard, 3, null);
                        }
                    }

                    #region :: Risk Amlo ::
                    if (dataAmlocdd != null)
                    {
                        chkFreeze = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.FREEZE.Code));
                        chkPEP_In = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.PEP_IN.Code));
                        chkPEP_Out = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.PEP_OUT.Code));
                        chkRCA_In = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.RCA_IN.Code));
                        chkRCA_Out = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.RCA_OUT.Code));
                        chkSTR = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.STR.Code));
                        chkHR_08 = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.HR_08.Code));
                        chkHR_02 = dataAmlocdd.Any(a => a.INFO_SOURCE.Contains(ENUM_INFO_SOURCE.HR_02.Code));

                    }



                    if (chkFreeze)
                    {

                        _Freezestatus = "Y";
                        riskAMLO.RISK_STATUS = _Freezestatus;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Freezestatus;
                    }
                    riskAMLO.Seq = 1;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.FREEZE.Value;
                    riskAMLO.RISK_CODE = ENUM_INFO_SOURCE.FREEZE.Code;
                    lstRiskAmlo.Add(riskAMLO);

                    riskAMLO = new RISK_AMLO();
                    if (chkPEP_In)
                    {
                        _Pepinstatus = "Y";
                        riskAMLO.RISK_STATUS = _Pepinstatus;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Pepinstatus;
                    }
                    riskAMLO.Seq = 2;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.PEP_IN.Value;
                    riskAMLO.RISK_CODE = "PEP_IN";
                    lstRiskAmlo.Add(riskAMLO);


                    riskAMLO = new RISK_AMLO();
                    if (chkPEP_Out)
                    {
                        _Pepoutstatus = "Y";
                        riskAMLO.RISK_STATUS = _Pepoutstatus;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Pepoutstatus;
                    }
                    riskAMLO.Seq = 3;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.PEP_OUT.Value;
                    riskAMLO.RISK_CODE = "PEP_OUT";
                    lstRiskAmlo.Add(riskAMLO);


                    riskAMLO = new RISK_AMLO();
                    if (chkSTR)
                    {
                        _Strstatus = "Y";
                        riskAMLO.RISK_STATUS = _Strstatus;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Strstatus;
                    }
                    riskAMLO.Seq = 4;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.STR.Value;
                    riskAMLO.RISK_CODE = ENUM_INFO_SOURCE.STR.Code;
                    lstRiskAmlo.Add(riskAMLO);

                    riskAMLO = new RISK_AMLO();
                    if (chkHR_08)
                    {
                        _Hr08status = "Y";
                        riskAMLO.RISK_STATUS = _Hr08status;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Hr08status;
                    }
                    riskAMLO.Seq = 5;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.HR_08.Value;
                    riskAMLO.RISK_CODE = ENUM_INFO_SOURCE.HR_08.Code;
                    lstRiskAmlo.Add(riskAMLO);

                    riskAMLO = new RISK_AMLO();
                    if (chkRCA_In)
                    {
                        _Rcainstatus = "Y";
                        riskAMLO.RISK_STATUS = _Rcainstatus;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Rcainstatus;
                    }
                    riskAMLO.Seq = 6;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.RCA_IN.Value;
                    riskAMLO.RISK_CODE = "RCA_IN";
                    lstRiskAmlo.Add(riskAMLO);

                    riskAMLO = new RISK_AMLO();
                    if (chkRCA_Out)
                    {
                        _Rcaoutstatus = "Y";
                        riskAMLO.RISK_STATUS = _Rcaoutstatus;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Rcaoutstatus;
                    }
                    riskAMLO.Seq = 7;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.RCA_OUT.Value;
                    riskAMLO.RISK_CODE = "RCA_OUT";
                    lstRiskAmlo.Add(riskAMLO);

                    riskAMLO = new RISK_AMLO();
                    if (chkHR_02)
                    {
                        _Hr02status = "Y";
                        riskAMLO.RISK_STATUS = _Hr02status;
                    }
                    else
                    {
                        riskAMLO.RISK_STATUS = _Hr02status;
                    }
                    riskAMLO.Seq = 8;
                    riskAMLO.RISK_DESC = ENUM_INFO_SOURCE.HR_02.Value;
                    riskAMLO.RISK_CODE = ENUM_INFO_SOURCE.HR_02.Code;
                    lstRiskAmlo.Add(riskAMLO);
                    #endregion :: Risk Amlo ::

                    #region :: Risk Others ::
                    if (item.PersonStatus == "C" || item.PersonStatus == "P")
                    {
                        chkNT = await Repo.GetOthersAmlocdd(moneyLaunderingRisks_Info.Appno, true, false, false, false);
                        chkONT = await Repo.GetOthersAmlocdd(moneyLaunderingRisks_Info.Appno, false, true, false, false);
                        chkOCC = await Repo.GetOthersAmlocdd(moneyLaunderingRisks_Info.Appno, false, false, true, false);
                        chkPRD = await Repo.GetOthersAmlocdd(moneyLaunderingRisks_Info.Appno, false, false, false, true);
                        riskOther = new RISK_OTHERs();
                        if (chkNT)
                        {
                            _NTstatus = "Y";
                            riskOther.RISK_STATUS = _NTstatus;
                        }
                        else
                        {
                            riskOther.RISK_STATUS = _NTstatus;
                        }
                        riskOther.Seq = 1;
                        riskOther.RISK_DESC = ENUM_OTHER_RISK.NT.Value;
                        riskOther.RISK_CODE = ENUM_OTHER_RISK.NT.Code;
                        lstRiskOthers.Add(riskOther);

                        riskOther = new RISK_OTHERs();
                        if (chkONT)
                        {
                            _ONTstatus = "Y";
                            riskOther.RISK_STATUS = _ONTstatus;
                        }
                        else
                        {
                            riskOther.RISK_STATUS = _ONTstatus;
                        }
                        riskOther.Seq = 2;
                        riskOther.RISK_DESC = ENUM_OTHER_RISK.ONT.Value;
                        riskOther.RISK_CODE = ENUM_OTHER_RISK.ONT.Code;
                        lstRiskOthers.Add(riskOther);

                        riskOther = new RISK_OTHERs();
                        if (chkOCC)
                        {
                            _OCCstatus = "Y";
                            riskOther.RISK_STATUS = _OCCstatus;
                        }
                        else
                        {
                            riskOther.RISK_STATUS = _OCCstatus;
                        }
                        riskOther.Seq = 3;
                        riskOther.RISK_DESC = ENUM_OTHER_RISK.OCC.Value;
                        riskOther.RISK_CODE = ENUM_OTHER_RISK.OCC.Code;
                        lstRiskOthers.Add(riskOther);

                        riskOther = new RISK_OTHERs();
                        if (item.PersonStatus == "C")
                        {
                            if (chkPRD)
                            {
                                _PRDstatus = "Y";
                                riskOther.RISK_STATUS = _PRDstatus;
                            }
                            else
                            {
                                riskOther.RISK_STATUS = _PRDstatus;
                            }
                            riskOther.Seq = 4;
                            riskOther.RISK_DESC = ENUM_OTHER_RISK.PRD.Value;
                            riskOther.RISK_CODE = ENUM_OTHER_RISK.PRD.Code;
                            lstRiskOthers.Add(riskOther);
                        }

                    }


                    #endregion :: Risk Others ::
                    #region :: Verify ::
                    
                    List<VERIFY_SYSTEM_INFO> lstVerifySystem = new List<VERIFY_SYSTEM_INFO>();
                    
                    List<VERIFY_USER_INFO> lstVerifyUser = new List<VERIFY_USER_INFO>();
                    string stsVerifySystem = "N";
                    string stsVerifySystemNot = "N";

                    if (_Freezestatus == "Y")
                    {
                        stsVerifySystem = _Freezestatus;

                    }
                    else if (_Pepinstatus == "Y")
                    {
                        stsVerifySystem = _Pepinstatus;
                    }
                    else if (_Pepoutstatus == "Y")
                    {
                        stsVerifySystem = _Pepoutstatus;
                    }
                    else if (_Rcainstatus == "Y")
                    {
                        stsVerifySystem = _Rcainstatus;
                    }
                    else if (_Rcaoutstatus == "Y")
                    {
                        stsVerifySystem = _Rcaoutstatus;
                    }
                    else if (_Strstatus == "Y")
                    {
                        stsVerifySystem = _Strstatus;
                    }
                    else if (_Hr08status == "Y")
                    {
                        stsVerifySystem = _Hr08status;
                    }
                    else if (_Hr02status == "Y")
                    {
                        stsVerifySystem = _Hr02status;
                    }
                    else if (_ONTstatus == "Y")
                    {
                        stsVerifySystem = _ONTstatus;
                    }
                    else if (_NTstatus == "Y")
                    {
                        stsVerifySystem = _NTstatus;
                    }
                    else if (_OCCstatus == "Y")
                    {
                        stsVerifySystem = _OCCstatus;
                    }
                    else if (_PRDstatus == "Y")
                    {
                        stsVerifySystem = _PRDstatus;
                    }
                    else
                    {
                        stsVerifySystemNot = "Y" ;
                    }

                    if (_Benefitstatus != "Y" && stsVerifySystem == "Y")
                    {
                        _Benefitstatus = stsVerifySystem;
                    }

                    P_AML_CTF_MATRIX rowAML_CTF_MATRIX = new P_AML_CTF_MATRIX();
                    rowAML_CTF_MATRIX.FREEZE_FLG = _Freezestatus;
                    rowAML_CTF_MATRIX.PEP_OUT_FLG = _Pepoutstatus;
                    rowAML_CTF_MATRIX.NATIONALITY_SERIOUS_FLG = _NTstatus;
                    rowAML_CTF_MATRIX.HR02_FLG = _Hr02status;
                    rowAML_CTF_MATRIX.HR08_FLG = _Hr08status;
                    rowAML_CTF_MATRIX.PEP_IN_FLG = _Pepinstatus;
                    rowAML_CTF_MATRIX.OCCUPATION_FLG = _OCCstatus;
                    rowAML_CTF_MATRIX.NATIONALITY_OTH_FLG = _ONTstatus;
                    rowAML_CTF_MATRIX.BENEFIT_FLG = _Benefitstatus;
                    rowAML_CTF_MATRIX.PRODUCT_FLG = _PRDstatus;
                    lstAmlCTFMatrix.Add(rowAML_CTF_MATRIX);
                    var dataMatrix = await Repo.GetP_AML_CTF_MATRIX(rowAML_CTF_MATRIX);
                    if (dataMatrix.Any())
                    {
                        var matrix = dataMatrix.FirstOrDefault();
                        lstAmlCTFMatrix.Add(matrix);
                        eddFormFLG = (eddFormFLG == "N" && matrix.EDD_FORM == "Y" )? matrix.EDD_FORM : "N";
                    }

                    VERIFY_SYSTEM_INFO rowVerifySystem = new VERIFY_SYSTEM_INFO();
                    rowVerifySystem.STATUS = stsVerifySystem;
                    rowVerifySystem.DESC_SYSTEM = ENUM_VERIFY.VERIFY_PRESON_DESC.Value;
                    rowVerifySystem.VERIFY_SYSTEM_DATE = Repo.SysDate;
                    lstVerifySystem.Add(rowVerifySystem);

                    rowVerifySystem = new VERIFY_SYSTEM_INFO();
                    rowVerifySystem.STATUS = stsVerifySystemNot;
                    rowVerifySystem.DESC_SYSTEM = ENUM_VERIFY.VERIFY_NOT_PRESON_DESC.Value;
                    rowVerifySystem.VERIFY_SYSTEM_DATE = Repo.SysDate;
                    lstVerifySystem.Add(rowVerifySystem);

                    VERIFY_USER_INFO rowVerifyUser = new VERIFY_USER_INFO();
                    rowVerifyUser.STATUS = "N";
                    rowVerifyUser.DESC_USER = ENUM_VERIFY.VERIFY_PRESON_DESC.Value;
                    rowVerifyUser.VERIFY_USER_DATE = null;
                    lstVerifyUser.Add(rowVerifyUser);
                    rowVerifyUser = new VERIFY_USER_INFO();
                    rowVerifyUser.STATUS = "N";
                    rowVerifyUser.DESC_USER = ENUM_VERIFY.VERIFY_NOT_PRESON_DESC.Value;
                    rowVerifyUser.VERIFY_USER_DATE = null;
                    lstVerifyUser.Add(rowVerifyUser);

                    #endregion

                    

                    if (item.PersonStatus == "C")
                    {
                        CUSTOMER_INFO customerInfo = new CUSTOMER_INFO();
                        customerInfo.PRE_NAME = item.PreName;
                        customerInfo.NAME = item.Name;
                        customerInfo.SURE_NAME = item.SureName;
                        customerInfo.ID_CARD = item.IdCard;
                        customerInfo.NATIONALITY = item.Nationality;
                        customerInfo.PERSON_STATUS = item.PersonStatus;
                        customerInfo.RISK_AMLOs = lstRiskAmlo.ToArray();
                        customerInfo.RISK_OTHERs = lstRiskOthers.ToArray();
                        customerInfo.VERIFY_SYSTEM = lstVerifySystem.ToArray();
                        customerInfo.VERIFY_USER = lstVerifyUser.ToArray();
                        if (customerInfo != null)
                        {
                            rowAMLOCDD.CUSTOMER_INFO = customerInfo;
                        }

                       

                    }
                    else if (item.PersonStatus == "P")
                    {
                        PAYER_INFO payerInfo = new PAYER_INFO();
                        payerInfo.PRE_NAME = item.PreName;
                        payerInfo.NAME = item.Name;
                        payerInfo.SURE_NAME = item.SureName;
                        payerInfo.ID_CARD = item.IdCard;
                        payerInfo.NATIONALITY = item.Nationality;
                        payerInfo.PERSON_STATUS = item.PersonStatus;
                        payerInfo.RISK_AMLOs = lstRiskAmlo.ToArray();
                        payerInfo.RISK_OTHERs = lstRiskOthers.ToArray();
                        payerInfo.VERIFY_SYSTEM = lstVerifySystem.ToArray();
                        payerInfo.VERIFY_USER = lstVerifyUser.ToArray();
                        if (payerInfo.RISK_AMLOs != null || payerInfo.RISK_OTHERs != null)
                        {
                            rowAMLOCDD.PAYER_INFO = payerInfo;
                        }
                    }
                    else if (item.PersonStatus == "B")
                    {
                        bENEFIT_INFO.PRE_NAME = item.PreName;
                        bENEFIT_INFO.NAME = item.Name;
                        bENEFIT_INFO.SURE_NAME = item.SureName;
                        bENEFIT_INFO.ID_CARD = item.IdCard;
                        bENEFIT_INFO.NATIONALITY = item.Nationality;
                        bENEFIT_INFO.PERSON_STATUS = item.PersonStatus;
                        string _name = !string.IsNullOrEmpty(item.Name) ? item.Name : "";
                        string _sureName = !string.IsNullOrEmpty(item.SureName) ? item.SureName : "";
                        bENEFIT_INFO.FULLNAME = _name + " " + _sureName;
                        bENEFIT_INFO.RISK_AMLOs = lstRiskAmlo.ToArray();
                        bENEFIT_INFO.VERIFY_SYSTEM = lstVerifySystem.ToArray();
                        bENEFIT_INFO.VERIFY_USER = lstVerifyUser.ToArray();
                        lstBENEFIT_INFOs.Add(bENEFIT_INFO);
                        
                    }




                }


                if (lstAmlCTFMatrix.Any())
                {
                    if (lstAmlCTFMatrix.Any(a=>a.RISK_LEVEL == "4"))
                    {
                        matrixRiskLevel = "4";

                    }
                    else if (lstAmlCTFMatrix.Any(a => a.RISK_LEVEL == "3"))
                    {
                        if (lstAmlCTFMatrix.Any(a => a.RISK_LEVEL == "3" && a.MX_ID == 5))
                        {
                            approverMatrix = ENUM_APPROVER.B.Value;
                        }
                        else
                        {
                            approverMatrix = ENUM_APPROVER.D.Value;
                        }
                        
                        matrixRiskLevel = "3";
                    }
                    else if (lstAmlCTFMatrix.Any(a => a.RISK_LEVEL == "2"))
                    {
                        matrixRiskLevel = "2";
                    }
                    else if (lstAmlCTFMatrix.Any(a => a.RISK_LEVEL == "1"))
                    {
                        matrixRiskLevel = "1";
                    }
                    else
                    {
                        matrixRiskLevel = "0";
                    }
                }
               
                #region :: Matrix ::
                for (int i = 0; i < 4; i++)
                {
                    RISK_MATRIX rowMatrix = new RISK_MATRIX();
                   
                    if (i == 0)
                    {
                        rowMatrix.CODE = ENUM_MATRIX.FORBIDDEN.Code;
                        rowMatrix.DESC_MATRIX = ENUM_MATRIX.FORBIDDEN.Value;
                    }
                    else if (i == 1)
                    {
                        rowMatrix.CODE = ENUM_MATRIX.HIGH_RISK.Code;
                        rowMatrix.DESC_MATRIX = ENUM_MATRIX.HIGH_RISK.Value;
                    }
                    else if (i == 2)
                    {
                        rowMatrix.CODE = ENUM_MATRIX.MIDDLE_RISK.Code;
                        rowMatrix.DESC_MATRIX = ENUM_MATRIX.MIDDLE_RISK.Value;
                    }
                    else if (i == 3)
                    {
                        rowMatrix.CODE = ENUM_MATRIX.LOW_RISK.Code;
                        rowMatrix.DESC_MATRIX = ENUM_MATRIX.LOW_RISK.Value;
                    }
                    
                    if (rowMatrix.CODE == matrixRiskLevel)
                    {
                        rowMatrix.STATUS = "Y";
                    }
                    else
                    {
                        rowMatrix.STATUS = "N";
                    }


                    lstRiskMatrix.Add(rowMatrix);
                }

                if (lstRiskMatrix.Any())
                {
                    rowAMLOCDD.RISK_MATRIX = lstRiskMatrix.ToArray();
                }

                #endregion

                #region :: EDD FORM ::
                UNDERWRITING_CONSIDERATIONS rowUnderWritingConsiderations = new UNDERWRITING_CONSIDERATIONS();
                
                List<EDD_FORM> lstEddForm = new List<EDD_FORM>();
                List<SUMMARY_OF_INSURANCE_CONSIDERATIONS> lstSummaryInsuranceConsiderations = new List<SUMMARY_OF_INSURANCE_CONSIDERATIONS>();

                for (int i = 0; i < 7; i++)
                {
                    EDD_FORM rowEddForm = new EDD_FORM();
                    rowEddForm.STATUS = "N";
                    if (i == 0)
                    {
                       
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_1.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_1.Value;
                    }
                    else if (i == 1)
                    {
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_2.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_2.Value;
                    }
                    else if (i == 2)
                    {
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_3.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_3.Value;
                    }
                    else if (i == 3)
                    {
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_4.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_4.Value;
                    }
                    else if (i == 4)
                    {
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_5.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_5.Value;
                    }
                    else if (i == 5)
                    {
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_6.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_6.Value;
                    }
                    else if (i == 6)
                    {
                        rowEddForm.CODE = ENUM_UNDERWRITING_CONSIDERATION.DESC_7.Code;
                        rowEddForm.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.DESC_7.Value;
                    }
                    lstEddForm.Add(rowEddForm);
                }

                for (int i = 0; i < 2; i++)
                {
                    SUMMARY_OF_INSURANCE_CONSIDERATIONS rowSummaryInsuranceConsideration = new SUMMARY_OF_INSURANCE_CONSIDERATIONS();
                    rowSummaryInsuranceConsideration.STATUS = "N";
                    if (i == 0)
                    {

                        rowSummaryInsuranceConsideration.CODE = ENUM_UNDERWRITING_CONSIDERATION.SUMMARY_DESC_1.Code;
                        rowSummaryInsuranceConsideration.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.SUMMARY_DESC_1.Value;
                    }
                    else if (i == 1)
                    {
                        rowSummaryInsuranceConsideration.CODE = ENUM_UNDERWRITING_CONSIDERATION.SUMMARY_DESC_2.Code;
                        rowSummaryInsuranceConsideration.DESC_EDD_FORM = ENUM_UNDERWRITING_CONSIDERATION.SUMMARY_DESC_2.Value;
                    }
                    lstSummaryInsuranceConsiderations.Add(rowSummaryInsuranceConsideration);
                }
                rowUnderWritingConsiderations.EDD_STATUS = eddFormFLG;
                rowUnderWritingConsiderations.EDD_Warning = approverMatrix;
                rowUnderWritingConsiderations.EDD_FORMs = lstEddForm.ToArray();
                rowUnderWritingConsiderations.SUMMARY_OF_INSURANCE_CONSIDERATIONs = lstSummaryInsuranceConsiderations.ToArray();
                rowAMLOCDD.UNDERWRITING_CONSIDERATIONS = rowUnderWritingConsiderations;

                #endregion

                rowAMLOCDD.APP_NO = moneyLaunderingRisks_Info.Appno;
                rowAMLOCDD.POLICY = moneyLaunderingRisks_Info.Policy;
                rowAMLOCDD.POLICY_ID = moneyLaunderingRisks_Info.Policy_Id;
                rowAMLOCDD.PLAN_ID = moneyLaunderingRisks_Info.PlanId;
                if (lstBENEFIT_INFOs.Any())
                {
                    rowAMLOCDD.BENEFIT_INFO = lstBENEFIT_INFOs.ToArray();
                }



                return rowAMLOCDD;
            }
            catch (Exception ex)
            {

                throw new Exception($"GetInfoForAmloNewbis Error : {ex.Message}");
            }
            finally
            {
                Repo.CloseConnection();
            }

        }
        public async Task<bool> SaveDataAMLO(AMLOCDD_INFO amlocddInfo)
        {
            bool saveFlg = false;
            var Repo = new Repository(_dbSetting.ConnectionString);
            Repo.OpenConnection();
            try
            {
               
                if (amlocddInfo != null)
                {

                    #region :: CUST_FLG = Customer(C) ::
                    if (amlocddInfo.CUSTOMER_INFO != null)
                    {
                        P_AML_CTF p_AML_CTF = new P_AML_CTF();
                        foreach (var item in amlocddInfo.CUSTOMER_INFO.RISK_AMLOs)
                        {
                            if (item.RISK_CODE == ENUM_INFO_SOURCE.FREEZE.Code)
                            {
                                p_AML_CTF.FREEZE_FLG = "Y"; 
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.HR_02.Code)
                            {
                                p_AML_CTF.HR02_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.HR_08.Code)
                            {
                                p_AML_CTF.HR08_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.PEP_IN.Code)
                            {
                                p_AML_CTF.PEP_IN_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.PEP_OUT.Code)
                            {
                                p_AML_CTF.PEP_OUT_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.RCA_IN.Code)
                            {
                                p_AML_CTF.RCA_IN_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.RCA_OUT.Code)
                            {
                                p_AML_CTF.RCA_OUT_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.STR.Code)
                            {
                                p_AML_CTF.STR_FLG = "Y";
                            }
                        }

                        foreach (var item in amlocddInfo.CUSTOMER_INFO.RISK_OTHERs)
                        {
                            if (item.RISK_CODE == ENUM_OTHER_RISK.NT.Code)
                            {
                                p_AML_CTF.NATION_SERIOUS_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_OTHER_RISK.OCC.Code)
                            {
                                p_AML_CTF.OCC_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_OTHER_RISK.ONT.Code)
                            {
                                p_AML_CTF.NATION_OTH_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_OTHER_RISK.PRD.Code)
                            {
                                p_AML_CTF.PRODUCT_FLG = "Y";
                            }
                        }

                        p_AML_CTF.ENTRY_TIME = "";
                        p_AML_CTF.ENTRY_DT = DateTime.Now;
                        p_AML_CTF.SYSTEM_DT = DateTime.Now;
                        p_AML_CTF.POLICY = amlocddInfo.POLICY;
                        p_AML_CTF.POLICY_ID = amlocddInfo.POLICY_ID;
                        p_AML_CTF.APP_NO = amlocddInfo.APP_NO;
                        p_AML_CTF.SEQ = 1;
                        p_AML_CTF.TMN = "Y";
                        p_AML_CTF.TMN_DT = DateTime.Now;
                        p_AML_CTF.CUST_FLG = amlocddInfo.CUSTOMER_INFO.PERSON_STATUS;
                        p_AML_CTF.PRENAME = amlocddInfo.CUSTOMER_INFO.PRE_NAME;
                        p_AML_CTF.NAME = amlocddInfo.CUSTOMER_INFO.NAME;
                        p_AML_CTF.SURNAME = amlocddInfo.CUSTOMER_INFO.SURE_NAME;
                        p_AML_CTF.IDCARD_NO = amlocddInfo.CUSTOMER_INFO.ID_CARD;
                        p_AML_CTF.BIRTH_DT = DateTime.Now;
                        p_AML_CTF.NATIONALITY = amlocddInfo.CUSTOMER_INFO.NATIONALITY;
                        p_AML_CTF.RISK_CLASS = "Y";
                        p_AML_CTF.SENDAUTH_DT = DateTime.Now;
                        p_AML_CTF.AMLO_AUTH = "Y";
                        p_AML_CTF.AUTH_DT = DateTime.Now;
                        p_AML_CTF.FSYSTEM_DT = DateTime.Now;

                        AddP_AML_CTF(ref p_AML_CTF);
                    }
                    #endregion

                    #region :: CUST_FLG = PAYER(P) ::
                    if (amlocddInfo.PAYER_INFO != null)
                    {
                        P_AML_CTF p_AML_CTF = new P_AML_CTF();
                        foreach (var item in amlocddInfo.PAYER_INFO.RISK_AMLOs)
                        {
                            if (item.RISK_CODE == ENUM_INFO_SOURCE.FREEZE.Code)
                            {
                                p_AML_CTF.FREEZE_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.HR_02.Code)
                            {
                                p_AML_CTF.HR02_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.HR_08.Code)
                            {
                                p_AML_CTF.HR08_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.PEP_IN.Code)
                            {
                                p_AML_CTF.PEP_IN_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.PEP_OUT.Code)
                            {
                                p_AML_CTF.PEP_OUT_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.RCA_IN.Code)
                            {
                                p_AML_CTF.RCA_IN_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.RCA_OUT.Code)
                            {
                                p_AML_CTF.RCA_OUT_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_INFO_SOURCE.STR.Code)
                            {
                                p_AML_CTF.STR_FLG = "Y";
                            }
                        }

                        foreach (var item in amlocddInfo.CUSTOMER_INFO.RISK_OTHERs)
                        {
                            if (item.RISK_CODE == ENUM_OTHER_RISK.NT.Code)
                            {
                                p_AML_CTF.NATION_SERIOUS_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_OTHER_RISK.OCC.Code)
                            {
                                p_AML_CTF.OCC_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_OTHER_RISK.ONT.Code)
                            {
                                p_AML_CTF.NATION_OTH_FLG = "Y";
                            }
                            else if (item.RISK_CODE == ENUM_OTHER_RISK.PRD.Code)
                            {
                                p_AML_CTF.PRODUCT_FLG = "Y";
                            }
                        }

                        p_AML_CTF.ENTRY_TIME = "";
                        p_AML_CTF.ENTRY_DT = DateTime.Now;
                        p_AML_CTF.SYSTEM_DT = DateTime.Now;
                        p_AML_CTF.POLICY = amlocddInfo.POLICY;
                        p_AML_CTF.POLICY_ID = amlocddInfo.POLICY_ID;
                        p_AML_CTF.APP_NO = amlocddInfo.APP_NO;
                        p_AML_CTF.SEQ = 1;
                        p_AML_CTF.TMN = "Y";
                        p_AML_CTF.TMN_DT = DateTime.Now;
                        p_AML_CTF.CUST_FLG = amlocddInfo.CUSTOMER_INFO.PERSON_STATUS;
                        p_AML_CTF.PRENAME = amlocddInfo.CUSTOMER_INFO.PRE_NAME;
                        p_AML_CTF.NAME = amlocddInfo.CUSTOMER_INFO.NAME;
                        p_AML_CTF.SURNAME = amlocddInfo.CUSTOMER_INFO.SURE_NAME;
                        p_AML_CTF.IDCARD_NO = amlocddInfo.CUSTOMER_INFO.ID_CARD;
                        p_AML_CTF.BIRTH_DT = DateTime.Now;
                        p_AML_CTF.NATIONALITY = amlocddInfo.CUSTOMER_INFO.NATIONALITY;
                        p_AML_CTF.RISK_CLASS = "Y";
                        p_AML_CTF.SENDAUTH_DT = DateTime.Now;
                        p_AML_CTF.AMLO_AUTH = "Y";
                        p_AML_CTF.AUTH_DT = DateTime.Now;
                        p_AML_CTF.FSYSTEM_DT = DateTime.Now;

                        AddP_AML_CTF(ref p_AML_CTF);
                    }


                    #endregion

                    #region :: CUST_FLG = BENEFIT(B) ::
                    if (amlocddInfo.BENEFIT_INFO != null && amlocddInfo.BENEFIT_INFO.Any())
                    {
                        int seq = 1;
                        foreach (var benefitInfo in amlocddInfo.BENEFIT_INFO)
                        {
                            P_AML_CTF p_AML_CTF = new P_AML_CTF();
                            foreach (var item in benefitInfo.RISK_AMLOs)
                            {
                                if (item.RISK_CODE == ENUM_INFO_SOURCE.FREEZE.Code)
                                {
                                    p_AML_CTF.FREEZE_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.HR_02.Code)
                                {
                                    p_AML_CTF.HR02_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.HR_08.Code)
                                {
                                    p_AML_CTF.HR08_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.PEP_IN.Code)
                                {
                                    p_AML_CTF.PEP_IN_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.PEP_OUT.Code)
                                {
                                    p_AML_CTF.PEP_OUT_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.RCA_IN.Code)
                                {
                                    p_AML_CTF.RCA_IN_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.RCA_OUT.Code)
                                {
                                    p_AML_CTF.RCA_OUT_FLG = "Y";
                                }
                                else if (item.RISK_CODE == ENUM_INFO_SOURCE.STR.Code)
                                {
                                    p_AML_CTF.STR_FLG = "Y";
                                }
                            }

                          
                            p_AML_CTF.ENTRY_TIME = "";
                            p_AML_CTF.ENTRY_DT = DateTime.Now;
                            p_AML_CTF.SYSTEM_DT = DateTime.Now;
                            p_AML_CTF.POLICY = amlocddInfo.POLICY;
                            p_AML_CTF.POLICY_ID = amlocddInfo.POLICY_ID;
                            p_AML_CTF.APP_NO = amlocddInfo.APP_NO;
                            p_AML_CTF.SEQ = seq;
                            p_AML_CTF.TMN = "Y";
                            p_AML_CTF.TMN_DT = DateTime.Now;
                            p_AML_CTF.CUST_FLG = amlocddInfo.CUSTOMER_INFO.PERSON_STATUS;
                            p_AML_CTF.PRENAME = amlocddInfo.CUSTOMER_INFO.PRE_NAME;
                            p_AML_CTF.NAME = amlocddInfo.CUSTOMER_INFO.NAME;
                            p_AML_CTF.SURNAME = amlocddInfo.CUSTOMER_INFO.SURE_NAME;
                            p_AML_CTF.IDCARD_NO = amlocddInfo.CUSTOMER_INFO.ID_CARD;
                            p_AML_CTF.BIRTH_DT = DateTime.Now;
                            p_AML_CTF.NATIONALITY = amlocddInfo.CUSTOMER_INFO.NATIONALITY;
                            p_AML_CTF.RISK_CLASS = "Y";
                            p_AML_CTF.SENDAUTH_DT = DateTime.Now;
                            p_AML_CTF.AMLO_AUTH = "Y";
                            p_AML_CTF.AUTH_DT = DateTime.Now;
                            p_AML_CTF.FSYSTEM_DT = DateTime.Now;

                            AddP_AML_CTF(ref p_AML_CTF);
                            seq = seq + 1;
                        }

                    }




                    #endregion

                    if (amlocddInfo.UNDERWRITING_CONSIDERATIONS.EDD_STATUS =="Y")
                    {
                        foreach (var item in amlocddInfo.UNDERWRITING_CONSIDERATIONS.EDD_FORMs)
                        {
                            P_AML_CTF_EDD row = new P_AML_CTF_EDD();
                            row.APP_NO = amlocddInfo.APP_NO;
                            row.EFF_DT = Repo.SysDate;
                            row.POLICY_ID = null;
                            row.TMN = "N";
                            row.TMN_DT = Repo.SysDate;

                            AddP_AML_CTF_EDD(ref row);
                        }
                        
                    }

                }
                else
                {
                    throw new Exception($"ไม่พบข้อมูลในการบันทึก");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"ไม่สามารถบันทึกข้อมูล  Error : {ex.Message}");
            }
            finally
            {
                Repo.CloseConnection();
            }
            return saveFlg;
        }



        public async Task<P_APPL_ID_INFO> GetDetailAppno(string appNo, string policy)
        {
            var Repo = new Repository(_dbSetting.ConnectionString);
            Repo.OpenConnection();
            P_APPL_ID_INFO p_Appl_Info = null;
            List<RISK_INFO> lstRiskInfo = new List<RISK_INFO>();
            try
            {
                if (!string.IsNullOrEmpty(policy)) 
                {

                    var dataPolicy = await Repo.GetDetailAppno(appNo, policy);
                    if (dataPolicy != null)
                    {
                        p_Appl_Info = new P_APPL_ID_INFO();
                        p_Appl_Info.POLICY = string.IsNullOrEmpty(dataPolicy.POLICY) ? policy : dataPolicy.POLICY;
                        p_Appl_Info.POLICY_ID = dataPolicy.POLICY_ID;
                        p_Appl_Info.APP_NO = string.IsNullOrEmpty(dataPolicy.APP_NO) ? appNo : dataPolicy.APP_NO;
                        p_Appl_Info.PLANCODE = dataPolicy.PLAN_CODE;
                        p_Appl_Info.PLAN_DESC = dataPolicy.PLAN_TITLE;
                        p_Appl_Info.CHANNEL_TYPE = dataPolicy.CHANNEL_TYPE;
                        p_Appl_Info.APP_DT = dataPolicy.APP_DT;

                        var pPolName = await Repo.GetP_POL_NAME(dataPolicy.POLICY_ID);
                        var pPolParent = await Repo.GetP_POL_PARENT(dataPolicy.POLICY_ID);
                        var pPolBenefit = await Repo.GetP_POL_BENEFIT(dataPolicy.POLICY_ID);
                        if (pPolName != null && pPolName.Any())
                        {
                            foreach (var item in pPolName)
                            {
                                RISK_INFO rowRisk = new RISK_INFO();
                                rowRisk.PRE_NAME = item.PRENAME;
                                rowRisk.NAME = item.NAME;
                                rowRisk.SURE_NAME = item.SURNAME;
                                rowRisk.ID_CARD = string.IsNullOrEmpty(item.IDCARD_NO) ? item.PASSPORT : item.IDCARD_NO;
                                rowRisk.NATIONALITY = item.NATIONALITY;
                                rowRisk.PERSON_STATUS = "C";
                                lstRiskInfo.Add(rowRisk);
                            }
                        }
                        if (pPolParent != null && pPolParent.Any())
                        {
                            foreach (var item in pPolParent)
                            {
                                RISK_INFO rowRisk = new RISK_INFO();
                                rowRisk.PRE_NAME = item.PRENAME;
                                rowRisk.NAME = item.NAME;
                                rowRisk.SURE_NAME = item.SURNAME;
                                rowRisk.ID_CARD = string.IsNullOrEmpty(item.IDCARD_NO) ? item.PASSPORT : item.IDCARD_NO; ;
                                rowRisk.NATIONALITY = item.NATIONALITY;
                                rowRisk.PERSON_STATUS = "P";
                                lstRiskInfo.Add(rowRisk);
                            }
                        }
                        if (pPolBenefit != null && pPolBenefit.Any())
                        {
                            foreach (var item in pPolBenefit)
                            {
                                RISK_INFO rowRisk = new RISK_INFO();
                                rowRisk.PRE_NAME = item.PRENAME;
                                rowRisk.NAME = item.NAME;
                                rowRisk.SURE_NAME = item.SURNAME;
                                rowRisk.PERSON_STATUS = "B";
                                lstRiskInfo.Add(rowRisk);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("ไม่พบข้อมูล");
                    }
                }
                else if (!string.IsNullOrEmpty(appNo))
                {
                    var uAppInfo = await Repo.GetU_APPLICATION(appNo);
                    if (uAppInfo != null)
                    {
                        p_Appl_Info = new P_APPL_ID_INFO();
                        p_Appl_Info.APP_NO = string.IsNullOrEmpty(uAppInfo.APP_NO)? appNo : uAppInfo.APP_NO;
                        p_Appl_Info.PLANCODE = uAppInfo.PLAN_CODE;
                        p_Appl_Info.PLAN_DESC = uAppInfo.PLAN_TITLE;
                        p_Appl_Info.CHANNEL_TYPE = uAppInfo.CHANNEL_TYPE;
                        p_Appl_Info.APP_DT = uAppInfo.APP_DT;

                        if (uAppInfo.UAPP_ID.HasValue)
                        {
                            var uNameInfo = await Repo.GetU_NAME_ID(uAppInfo.UAPP_ID);
                            if (uNameInfo != null && uNameInfo.Any())
                            {

                                foreach (var item in uNameInfo)
                                {
                                    RISK_INFO rowRisk = new RISK_INFO();
                                    rowRisk.PRE_NAME = item.PRENAME;
                                    rowRisk.NAME = item.NAME;
                                    rowRisk.SURE_NAME = item.SURNAME;
                                    rowRisk.ID_CARD = item.IDCARD_NO;
                                    rowRisk.NATIONALITY = item.NATIONALITY;
                                    rowRisk.PERSON_STATUS = item.CUSTOMER_TYPE;
                                    lstRiskInfo.Add(rowRisk);
                                }
                            }

                            var uBenefit = await Repo.GetU_APP_BENEFIT(uAppInfo.UAPP_ID);
                            if (uBenefit != null && uBenefit.Any())
                            {
                                foreach (var item in uBenefit)
                                {
                                    RISK_INFO rowRisk = new RISK_INFO();
                                    rowRisk.PRE_NAME = item.PRENAME;
                                    rowRisk.NAME = item.NAME;
                                    rowRisk.SURE_NAME = item.SURNAME;
                                    rowRisk.PERSON_STATUS = "B";
                                    lstRiskInfo.Add(rowRisk);
                                }
                            }
                            
                        }

                    }
                    else
                    {
                        throw new Exception("ไม่พบข้อมูล");
                    }
                }
                else
                {
                    throw new Exception("ระบุเลขกรมธรรม์หรือเลขใบคำขอ");
                }
                if (lstRiskInfo.Any())
                {
                    p_Appl_Info.RISK_INFOs = lstRiskInfo.ToArray();
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"ไม่สามารถค้นหาข้อมูล  Error : {ex.Message}");
            }
            finally
            {
                Repo.CloseConnection();
            }



            return p_Appl_Info;

        }

        
        public bool CheckStatusAmlo(string Code)
        {
            bool stsAmlo = false;
            try
            {
                if (!string.IsNullOrEmpty(Code))
                {
                    if (Code == ENUM_INFO_SOURCE.FREEZE.Code)
                    {
                        stsAmlo = true;
                    }
                    else if (Code == ENUM_INFO_SOURCE.HR_02.Code)
                    {
                        stsAmlo = true;

                    }
                    else if (Code == ENUM_INFO_SOURCE.HR_08.Code)
                    {
                        stsAmlo = true;
                    }
                    else if (Code == ENUM_INFO_SOURCE.PEP_IN.Code)
                    {
                        stsAmlo = true;
                    }
                    else if (Code == ENUM_INFO_SOURCE.PEP_OUT.Code)
                    {
                        stsAmlo = true;
                    }
                    else if (Code == ENUM_INFO_SOURCE.RCA_IN.Code)
                    {
                        stsAmlo = true;
                    }
                    else if (Code == ENUM_INFO_SOURCE.RCA_OUT.Code)
                    {
                        stsAmlo = true;
                    }
                    else if (Code == ENUM_INFO_SOURCE.STR.Code)
                    {
                        stsAmlo = true;
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Erorr Status Amlo :{ex.Message}");
            }
            return stsAmlo;
        }

        #region :: Inset AML CTF ::
        public void AddP_AML_CTF_REMARK(ref P_AML_CTF_REMARK addObject)
        {
            AddP_AML_CTF_REMARK(ref addObject, (Repository)null);
        }
        public void AddP_AML_CTF_REMARK(ref P_AML_CTF_REMARK addObject, Repository repository)
        {
            bool internalConnection = false;
            if (repository == null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }
            try
            {
                repository.AddP_AML_CTF_REMARK(ref addObject);
                if (internalConnection)
                    repository.commitTransaction();
            }
            catch (Exception ex)
            {
                if (internalConnection)
                    repository.rollbackTransaction();
                throw ex;
            }
            finally
            {
                if (internalConnection)
                    repository.CloseConnection();
            }
        }
        public void AddP_AML_CTF_EDD(ref P_AML_CTF_EDD addObject)
        {
            AddP_AML_CTF_EDD(ref addObject, (Repository)null);
        }
        public void AddP_AML_CTF_EDD(ref P_AML_CTF_EDD addObject, Repository repository)
        {
            bool internalConnection = false;
            if (repository == null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }
            try
            {
                repository.AddP_AML_CTF_EDD(ref addObject);
                if (internalConnection)
                    repository.commitTransaction();
            }
            catch (Exception ex)
            {
                if (internalConnection)
                    repository.rollbackTransaction();
                throw ex;
            }
            finally
            {
                if (internalConnection)
                    repository.CloseConnection();
            }
        }
        public void  AddP_AML_CTF(ref P_AML_CTF addObject)
        {
            AddP_AML_CTF(ref addObject, (Repository)null);
        }
        public void AddP_AML_CTF(ref P_AML_CTF addObject, Repository repository)
        {
            bool internalConnection = false;
            if (repository == null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }
            try
            {
                repository.AddP_AML_CTF(ref addObject);
                if (internalConnection)
                    repository.commitTransaction();
            }
            catch (Exception ex)
            {
                if (internalConnection)
                    repository.rollbackTransaction();
                throw ex;
            }
            finally
            {
                if (internalConnection)
                    repository.CloseConnection();
            }
        }

        public void AddP_AML_CTF_CHANNEL(ref P_AML_CTF_CHANNEL addObject)
        {
            AddP_AML_CTF_CHANNEL(ref addObject, (Repository)null);
        }
        public void AddP_AML_CTF_CHANNEL(ref P_AML_CTF_CHANNEL addObject, Repository repository)
        {
            bool internalConnection = false;
            if (repository == null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                repository.beginTransaction();
                internalConnection = true;
            }
            try
            {
                repository.AddP_AML_CTF_CHANNEL(ref addObject);
                if (internalConnection)
                    repository.commitTransaction();
            }
            catch (Exception ex)
            {
                if (internalConnection)
                    repository.rollbackTransaction();
                throw ex;
            }
            finally
            {
                if (internalConnection)
                    repository.CloseConnection();
            }
        }

        #endregion
    }
}
