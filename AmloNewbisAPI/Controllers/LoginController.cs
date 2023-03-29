using AmloNewbis.DataContract;
using AmloNewbisAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
    public class LoginController : APIControllerBase 
    {
        public LoginController(IConfiguration config, IHostEnvironment hosting, IOptions<AppSettingsModel> settings) : base(config, hosting, settings)
        {


        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public IActionResult Login([FromForm]Login user)
        {
            try
            {
                if (user == null && user.userID == null)
                {
                    return BadRequest("Invalid client request");
                }
                string redirectUrl = _appSettings.RedirectURL;
                var datenow = DateTime.Now;
                var time = datenow.TimeOfDay.Ticks + 1000 * 7200;
                var gotoPage = redirectUrl + "?userId=" + user.userID + "&fullname=" + user.fullname + "&timeStamp=" + time;
                return Redirect(gotoPage);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.ToString());
            }
        }


    }
}
