using AmloNewbis.BusinessLogic;
using AmloNewbis.DataContract;
using AmloNewbisAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class ReportController : APIControllerBase
    {
        public ReportController(IConfiguration config, IHostEnvironment hosting, IOptions<Model.AppSettingsModel> settings) : base(config, hosting, settings)
        {


        }


        [HttpPost]
        [Route("GenerateEDDReport")]
        public IActionResult GenerateEDDReport([FromBody] EddReportRequest request)
        {
            try
            {
                //request.APP_NO = "D055375";
                //request.N_USER_ID = "003726";
                //request.POLICY_NO = "";
                var action = new ServiceAction(_appSettings.DBSettingModel);
                EddReport[] dataReport = action.GetEddReport(request);

                byte[] filereport = null;
                var dateNow = DateTime.Now;
                using (MemoryStream ms = new MemoryStream())
                {
                    var rpt = new Report.EDDReport(dataReport);
                    rpt.CreateDocument();
                    rpt.ExportToPdf(ms);
                    filereport = ms.ToArray();
                }
                var fileName = "EDD_Report" + dateNow.ToString() + ".pdf";
                var file = File(filereport, "application/pdf", fileName);

                return file;
                //return Ok(dataReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }

        [HttpPost]
        [Route("GenerateAmloReport")]
        public IActionResult GenerateAmloReport([FromBody] AmloReportRequest request)
        {
            try
            {
                //request.APP_NO = "D055375";
                //request.N_USER_ID = "003726";
                //request.POLICY_NO = "";
                var action = new ServiceAction(_appSettings.DBSettingModel);
                AmloReport[] dataReport = action.GetAmloReport(request);

                byte[] filereport = null;
                var dateNow = DateTime.Now;
                using (MemoryStream ms = new MemoryStream())
                {
                    var rpt = new Report.AmloFormReport(dataReport);
                    rpt.CreateDocument();
                    rpt.ExportToPdf(ms);
                    filereport = ms.ToArray();
                }
                var fileName = "EDD_Report" + dateNow.ToString() + ".pdf";
                var file = File(filereport, "application/pdf", fileName);

                return file;
                //return Ok(dataReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }
        }
    }
}
