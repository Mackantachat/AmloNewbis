using AmloNewbis.DataAccess;
using AmloNewbis.DataContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmloNewbis.BusinessLogic
{
    public partial class ServiceAction
    {
        public EddReport[] GetEddReport(EddReportRequest request) => GetEddReport(request, null);
        private EddReport[] GetEddReport(EddReportRequest request, Repository repository)
        {
            EddReport[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetEddReportTypeP(request.APP_NO);
                if (data == null)
                {
                    data = repository.GetEddReportTypeC(request.APP_NO);
                }
                var factRecorder = repository.GetFactRecorder(request.N_USER_ID);
                var ekyc = repository.GetEKYC_MAIN( request.POLICY_NO, request.APP_NO);
                var address = repository.GetAddress(request.APP_NO);
                var riskBene = repository.GetRiskBenefit(request.APP_NO);
                foreach (var rep in data)
                {
                    rep.FACT_RECORDER = factRecorder.FULLNAME;
                    rep.SUM_PRM = (Convert.ToInt32(rep.BSC_PRM) +  Convert.ToInt32(rep.RDR_PRM)).ToString();
                    rep.RESULT_FLG = ekyc == null ? null : ekyc.RESULT_FLG;
                    rep.RISK_BENEFIT = riskBene == null ? null : riskBene.APP_NO;
                    foreach (var ad in address)
                    {
                        if (ad.ADDRESS_TYPE == "0")
                        {
                            rep.CURRENT_ADDRESS = ad.ADDRESS_TYPE;
                        }
                        else if(ad.ADDRESS_TYPE == "1")
                        {
                            rep.WORK_ADDRESS = ad.ADDRESS_TYPE;
                        }
                        else if (ad.ADDRESS_TYPE == "2")
                        {
                            rep.REGISTRATION_ADDRESS = ad.ADDRESS_TYPE;
                        }
                    }
                }

            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }


        public AmloReport[] GetAmloReport(AmloReportRequest request) => GetAmloReport(request, null);
        private AmloReport[] GetAmloReport(AmloReportRequest request, Repository repository)
        {
            AmloReport[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {

                data = repository.GetAmloReport(request.APP_NO);
                if (data != null)
                {
                    foreach (var d in data)
                    {
                        var iden = repository.GetP_POLICY_IDENTITY(d.POLICY);
                        var assesor = repository.GetZTB_USER(d.UPD_ID);
                        if (iden != null)
                        {
                            d.POLICY_NUMBER = iden.POLICY_NUMBER;
                            d.CERT_NUMBER = iden.CERT_NUMBER;
                        }
                        if (assesor != null)
                        {
                            d.ASSESSOR = assesor.PRENAME + ' ' + assesor.NAME + ' ' + assesor.SURNAME;
                        }
                    }
                }
               
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }


        public AUTB_CHANNEL[] GetAUTB_CHANNEL() => GetAUTB_CHANNEL(null);
        private AUTB_CHANNEL[] GetAUTB_CHANNEL(Repository repository)
        {
            AUTB_CHANNEL[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {

                data = repository.GetAUTB_CHANNEL();
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }

        public AmloReport[] GetAmloData(EddRequest request) => GetAmloData(request,null);
        private AmloReport[] GetAmloData(EddRequest request , Repository repository)
        {
            AmloReport[] data = null;
            bool internalConnection = false;
            if (repository is null)
            {
                repository = new Repository(_dbSetting.ConnectionString);
                repository.OpenConnection();
                internalConnection = true;
            }

            try
            {
                data = repository.GetAmloData(request);
            }
            finally
            {
                if (internalConnection)
                {
                    repository.CloseConnection();
                }
            }
            return data;
        }
    }
}
