using AmloNewbis.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmloNewbisAPI.Model
{
    public class AppSettingsModel
    {
        public DBSettingModel DBSettingModel { get; set; }
        public EndpointServiceURLs EndpointServiceURLs { get; set; }
        public string RedirectURL { get; set; }

    }
    public class EndpointServiceURLs
    {
        public string LogFileWCF_URL { get; set; }
        public string PolicyWithCerSvc { get; set; }

    }

}
