using AmloNewbis.BusinessLogic;
using AmloNewbis.DataContract;
using AmloNewbisAPI.Library.Helper;
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
using System.Net;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class EddController : APIControllerBase
    {
        public EddController(IConfiguration config, IHostEnvironment hosting, IOptions<AppSettingsModel> settings) : base(config, hosting, settings)
        {


        }

        [HttpPost, Route("GetAUTB_CHANNEL")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AmlocddInfo_Response), (int)HttpStatusCode.OK)]

        public IActionResult GetAUTB_CHANNEL()
        {
            var action = new ServiceAction(_appSettings.DBSettingModel);
            try
            {
              
                var data =  action.GetAUTB_CHANNEL();
                if (data == null)
                {
                    return StatusCode((int)HttpStatusCode.OK , null);
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError , ex.Message);
            }
         
        }

        [HttpPost, Route("GetPolicyInChannel")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Model.ZTB_POLICYOWNER_PLAN), (int)HttpStatusCode.OK)]

        public IActionResult GetPolicyInChannel([FromBody]EddRequest request)
        {
            var action = new ServiceAction(_appSettings.DBSettingModel);
            try
            {
                PolicyWithCertSvcClient client = InitializeWebServicePolicyWithCert();
                ProcessResult pr = new ProcessResult();
                var data = client.GetPolicyInChannel(request.ChannelType , out pr).ToListDataZtbPolicyOwnerPlan();


                if (data.Count == 0 && !data.Any())
                {
                    return Ok();
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [HttpPost, Route("GetAmloData")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AmlocddInfo_Response), (int)HttpStatusCode.OK)]

        public IActionResult GetAmloData([FromBody]EddRequest request)
        {
            var action = new ServiceAction(_appSettings.DBSettingModel);
            try
            {
                var data = action.GetAmloData(request);
                if (data == null)
                {
                    return StatusCode((int)HttpStatusCode.OK, null);
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
