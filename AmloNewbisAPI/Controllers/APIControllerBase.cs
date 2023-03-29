using AmloNewbis.BusinessLogic;
using AmloNewbisAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PolicyWithCertServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;


namespace AmloNewbisAPI.Controllers
{
    [ApiController]
    public class APIControllerBase : ControllerBase
    {
        protected readonly AppSettingsModel _appSettings;
        protected readonly EndpointServiceURLs _endpointServiceURL;
        protected readonly IHostEnvironment _hostingEnvironment;
        public IConfiguration _configuration { get; private set; }
        public APIControllerBase(IConfiguration config, IHostEnvironment hosting, IOptions<AppSettingsModel> settings)
        {
            
            _configuration = config;
            _hostingEnvironment = hosting;
            _appSettings = settings.Value;
            _endpointServiceURL = _appSettings.EndpointServiceURLs;
            

        }

        [HttpGet("Environment")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Environment()
        {
            if (_hostingEnvironment.IsProduction())
            {
                return Ok(new
                {
                    Environment = "This Server is Production Environment",
                    CurrentDateTime = $"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}"
                });
            }
            else
            {
                return Ok(new
                {
                    Environment = "This Server is Development Environment",
                    CurrentDateTime = $"{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}"
                });
            }
        }

        protected OkObjectResult SuccessResponse(object result = null)
        {
            return Ok(result);
        }

        protected ActionResult SuccessNoContent(object result = null)
        {
            return StatusCode(StatusCodes.Status204NoContent, result);
        }

        protected ObjectResult ErrorResponse(Exception ex, Guid guid)
        {
            if (_hostingEnvironment.IsProduction())
            {
                if (ex is Oracle.ManagedDataAccess.Client.OracleException || ex.Message.Contains("ORA-"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { ErrorMessage = "InternalException : BLAER-" + guid.ToString("D") });
                }
                else
                {
                     return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse { ErrorMessage = $"{ex.Message}" });
                }
               
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace });
            }
        }

        protected ObjectResult NotFondResponse(string message = "")
        {
            return StatusCode(StatusCodes.Status404NotFound, new ErrorResponse { ErrorMessage = message });
        }

        protected ObjectResult BadRequestResponse(string message = "")
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponse { ErrorMessage = message });
        }

        protected async Task WriteLogFile(string SystemName, string[] LogMessages)
        {
            try
            {
                if (SystemName != null && LogMessages != null && LogMessages.Any())
                {
                    LogFileWcfSvcRef.Service1Client client = InitializeWebServiceLogFile();
                    Task<LogFileWcfSvcRef.ProcessResult> pr = client.WriteLogSingleFileAsync(SystemName, LogMessages);
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
        }

        private LogFileWcfSvcRef.Service1Client InitializeWebServiceLogFile()
        {
            try
            {
                LogFileWcfSvcRef.Service1Client DataInitializeWebService = null;
                System.ServiceModel.BasicHttpBinding binding = SetHttpBindingLogFilee();
                System.ServiceModel.EndpointAddress endpoint = new System.ServiceModel.EndpointAddress(_endpointServiceURL.LogFileWCF_URL);
                DataInitializeWebService = new LogFileWcfSvcRef.Service1Client(binding, endpoint);
                return DataInitializeWebService;
            }
            catch
            {
                throw;
            }
        }

        protected PolicyWithCertSvcClient InitializeWebServicePolicyWithCert()
        {
            try
            {
                PolicyWithCertSvcClient DataInitializeWebService = null;
                System.ServiceModel.BasicHttpBinding binding = SetHttpBindingPolicyWithCert();
                System.ServiceModel.EndpointAddress endpoint = new System.ServiceModel.EndpointAddress(_endpointServiceURL.PolicyWithCerSvc);
                DataInitializeWebService = new PolicyWithCertSvcClient(binding, endpoint);
                return DataInitializeWebService;
            }
            catch
            {
                throw;
            }
        }

        private System.ServiceModel.BasicHttpBinding SetHttpBindingLogFilee()
        {
            try
            {
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                return binding;
            }
            catch
            {
                throw;
            }
        }

        private System.ServiceModel.BasicHttpBinding SetHttpBindingPolicyWithCert()
        {
            try
            {
                System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding();
                binding.CloseTimeout = TimeSpan.FromMinutes(10);
                binding.OpenTimeout = TimeSpan.FromMinutes(10);
                binding.SendTimeout = TimeSpan.FromMinutes(10);
                binding.MaxBufferPoolSize = Int32.MaxValue;
                binding.MaxBufferSize = Int32.MaxValue;
                binding.MaxReceivedMessageSize = Int32.MaxValue;
                binding.TransferMode = TransferMode.Streamed;
                binding.ReaderQuotas.MaxDepth = Int32.MaxValue;
                binding.ReaderQuotas.MaxStringContentLength = Int32.MaxValue;
                binding.ReaderQuotas.MaxArrayLength = Int32.MaxValue;
                binding.ReaderQuotas.MaxBytesPerRead = Int32.MaxValue;
                binding.ReaderQuotas.MaxNameTableCharCount = Int32.MaxValue;
                return binding;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
