using AmloNewbis.BusinessLogic;
using AmloNewbisAPI.Library.Helper;
using AmloNewbisAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Controllers
{
    [Route("api/v1/[controller]")]

    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class AmloNewbisController : APIControllerBase
    {
 
      
        public AmloNewbisController(IConfiguration config, IHostEnvironment hosting, IOptions<AppSettingsModel> settings) : base(config, hosting, settings)
        {
      

        }
        [HttpPost, Route("GetInfoForAmloNewbis")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AmlocddInfo_Response), (int)HttpStatusCode.OK)]

        public async ValueTask<IActionResult> GetInfoForAmloNewbis([FromBody] MoneyLaunderingRiskInfo_Request dataRequest)
        {
            var action = new ServiceAction(_appSettings.DBSettingModel);
            var logs = new List<string>();
            Guid guid = Guid.NewGuid();
            try
            {
                var model = dataRequest.ToAppMoneyLaunderingRiskInfo();

                var info = await action.GetInfoForAmloNewbis(model);
                if (info == null)
                {
                    return SuccessNoContent();
                }
                else
                {
                    
                    return SuccessResponse(info.ToAmlocddDataResponse());
                }
            }
            catch (Exception ex)
            {
                logs.Add($"GetAppDocumentMessageInfo Error [{guid.ToString("D")}]: {ex.Message}!");
                return ErrorResponse(ex, guid); 
            }
            finally
            {
                Task Log = WriteLogFile("AmloNewbisAPI", logs.ToArray());
            }
        }
        [HttpPost, Route("SaveInfoAmloNewbis")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]

        public async ValueTask<IActionResult> SaveInfoAmloNewbis([FromBody] AmlocddInfo_Request dataRequest)
        {
            var action = new ServiceAction(_appSettings.DBSettingModel);
            var logs = new List<string>();
            Guid guid = Guid.NewGuid();
            try
            {
                var model = dataRequest.ToAmlocdDataInfo();

                var info = await action.SaveDataAMLO(model);
                if (info)
                {
                    return SuccessNoContent("บันทึกข้อมูลสำเร็จ");
                }
                else
                {

                    return BadRequestResponse("ไม่สามารถบันทึกข้อมูลได้ กรุณาติดต่อผู้ดูแลระบบ");
                }
            }
            catch (Exception ex)
            {
                logs.Add($"GetAppDocumentMessageInfo Error [{guid.ToString("D")}]: {ex.Message}!");
                return ErrorResponse(ex, guid);
            }
            finally
            {
                Task Log = WriteLogFile("AmloNewbisAPI", logs.ToArray());
            }
        }

        [HttpGet, Route("GetInfoAppNo")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AppNoInfo), (int)HttpStatusCode.OK)]

        public async ValueTask<IActionResult> GetInfoAppNo(string appNo,string policy )
        {
            var action = new ServiceAction(_appSettings.DBSettingModel);
            var logs = new List<string>();
            Guid guid = Guid.NewGuid();
            try
            {
                if (string.IsNullOrEmpty(policy) && string.IsNullOrEmpty(appNo))
                {
                    throw new Exception("ระบุบเลขใบคำขอหรือเลขกรมธรรม์ใหม่");
                }
            }
            catch (Exception ex)
            {

                return BadRequestResponse(ex.Message);
            }
            try
            {

                var info = await action.GetDetailAppno(appNo, policy);
                if (info == null)
                {
                    return SuccessResponse("ไม่พบข้อมูล");
                }
                else
                {

                    return SuccessResponse(info.ToDataAppNoInfo());
                }
            }
            catch (Exception ex)
            {
                logs.Add($"GetAppDocumentMessageInfo Error [{guid.ToString("D")}]: {ex.Message}!");
                return ErrorResponse(ex, guid);
            }
            finally
            {
                Task Log = WriteLogFile("AmloNewbisAPI", logs.ToArray());
            }
        }
    }
}
